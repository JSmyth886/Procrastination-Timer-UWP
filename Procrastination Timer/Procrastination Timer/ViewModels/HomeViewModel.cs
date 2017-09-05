using System;
using System.Diagnostics;
using System.Windows.Input;
using Windows.UI.Xaml;
using Procrastination_Timer.Common;
using Caliburn.Micro;
using Windows.UI.Notifications;
using Microsoft.Toolkit.Uwp.Notifications;

namespace Procrastination_Timer.ViewModels
{
  public class HomeViewModel : Screen
  {
    private readonly Windows.Storage.ApplicationDataContainer _localSettings;
    private readonly Stopwatch _timer1;
    private readonly Stopwatch _timer2;
    private readonly ToastNotifier _toastNotifier;
    private const string IsPomodoro = "isPomodoro";
    private const string IsFiftySeven = "isFiftySeven";
    private const string IsCustom = "isCustom";
    private const string WorkingNotificationTitle = "Time for a break!";
    private const string ProcrastinationNotificationTitle = "Time to study!";
    private const int PomodoroWorkingTime = 25;
    private const int PomodoroProcrastinationTime = 5;
    private const int FiftySevenWorkingTime = 52;
    private const int FiftySevenProcrastinationTime = 17;

    private bool _isTimerOneEnabled;
    private bool _isPaneOpen;
    private bool _playing;
    private bool _pomodoroIsOn;
    private bool _fiftySevenIsOn;
    private bool _customIsOn;
    private string _workingText;
    private string _procrastinatingText;
    private int _workingTime;
    private int _procrastinationTime;
    private string _toastTitle;
    private string _toastContent;
    private string _technique;
    private ScheduledToastNotification _previousSceduledToastNotification;
    private DateTime _dueTime;
    private int _pomodoroCount = 0;

    private const string ToastLogo = "ms-appx:///Assets/Square150x150Logo.png";

    public HomeViewModel()
    {
      _isTimerOneEnabled = true;
      _localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
      _toastNotifier = ToastNotificationManager.CreateToastNotifier();

      _timer1 = new Stopwatch();
      _timer2 = new Stopwatch();

      _pomodoroIsOn = _localSettings.Values["PomodoroIsOn"] as bool? ?? true;
      if (_pomodoroIsOn)
        _technique = IsPomodoro;
      _fiftySevenIsOn = _localSettings.Values["FiftySevenIsOn"] as bool? ?? false;
      if (_fiftySevenIsOn)
        _technique = IsFiftySeven;
      _customIsOn = _localSettings.Values["CustomIsOn"] as bool? ?? false;
      if (_customIsOn)
        _technique = IsCustom;

      _procrastinatingText = _localSettings.Values["ProcrastinationText"] as string;
      _workingText = _localSettings.Values["WorkingText"] as string;

      var dispatcher = new DispatcherTimer();
      dispatcher.Tick += DispatcherOnTick;
      dispatcher.Start();

      ShowSettingsCommand = new CommandHandler(p =>
      {
        IsPaneOpen = !IsPaneOpen;
      }, true);

      RefreshCommand = new CommandHandler(p =>
      {
        _timer1.Reset();
        _timer2.Reset();
        NotifyOfPropertyChange(nameof(Time));
        NotifyOfPropertyChange(nameof(Time2));
      }, true);

      PlayCommand = new CommandHandler(p =>
      {
        if (Playing)
        {
          dispatcher.Stop();
          StopTimer(IsTimerOneEnabled ? _timer1 : _timer2);
        }
        else
        {
          dispatcher.Start();
          StartTimer(IsTimerOneEnabled ? _timer1 : _timer2);
        }
        Playing = !Playing;
      }, true);
    }

    private void DispatcherOnTick(object sender, object o)
    {
      if (IsTimerOneEnabled)
      {
        NotifyOfPropertyChange(nameof(Time));
        return;
      }

      NotifyOfPropertyChange(nameof(Time2));
    }

    public ICommand ShowSettingsCommand { get; }
    public ICommand RefreshCommand { get; }
    public ICommand PlayCommand { get; }

    public bool Playing
    {
      get => _playing;
      set
      {
        if (value == _playing) return;
        _playing = value;
        NotifyOfPropertyChange(nameof(Playing));
      }
    }

    public bool IsPaneOpen
    {
      get => _isPaneOpen;
      set
      {
        if (value == _isPaneOpen) return;
        _isPaneOpen = value;
        NotifyOfPropertyChange(nameof(IsPaneOpen));
      }
    }

    public bool PomodoroIsOn
    {
      get => _pomodoroIsOn;
      set
      {
        if (value == _pomodoroIsOn) return;
        _pomodoroIsOn = value;
        NotifyOfPropertyChange(nameof(PomodoroIsOn));
        SetTimerTechniquesSettings(IsPomodoro, value);
        if (value)
          _technique = IsPomodoro;
        else
          ResetPomodoroCount();
      }
    }

    public bool FiftySevenIsOn
    {
      get => _fiftySevenIsOn;
      set
      {
        if (value == _fiftySevenIsOn) return;
        _fiftySevenIsOn = value;
        NotifyOfPropertyChange(nameof(FiftySevenIsOn));
        SetTimerTechniquesSettings(IsFiftySeven, value);
        if (value)
          _technique = IsFiftySeven;
      }
    }

    public bool CustomIsOn
    {
      get => _customIsOn;
      set
      {
        if (value == _customIsOn) return;
        _customIsOn = value;
        NotifyOfPropertyChange(nameof(CustomIsOn));
        SetTimerTechniquesSettings(IsCustom, value);
        if (value)
          _technique = IsCustom;
      }
    }

    public bool IsTimerOneEnabled
    {
      get => _isTimerOneEnabled;
      set
      {
        if (value == _isTimerOneEnabled) return;
        _isTimerOneEnabled = value;
        NotifyOfPropertyChange(nameof(IsTimerOneEnabled));
        if (!Playing) return;
        StopTimer(value ? _timer2 : _timer1);
        StartTimer(value ? _timer1 : _timer2);
      }
    }

    public string WorkingText
    {
      get => _workingText;
      set
      {
        if (value.Equals(_workingText)) return;
        _workingText = value;
        NotifyOfPropertyChange(nameof(WorkingText));
        _localSettings.Values["WorkingText"] = value;
      }
    }

    public string ProcrastinatingText
    {
      get => _procrastinatingText;
      set
      {
        if (value.Equals(_procrastinatingText)) return;
        _procrastinatingText = value;
        NotifyOfPropertyChange(nameof(ProcrastinatingText));
        _localSettings.Values["ProcrastinationText"] = value;
      }
    }

    public int WorkingTime
    {
      get => _workingTime;
      set
      {
        if (value.Equals(_workingTime)) return;
        _workingTime = value;
        NotifyOfPropertyChange(nameof(WorkingTime));
        _localSettings.Values["WorkingTime"] = value;
      }
    }

    public int ProcrastinationTime
    {
      get => _procrastinationTime;
      set
      {
        if (value.Equals(_procrastinationTime)) return;
        _procrastinationTime = value;
        NotifyOfPropertyChange(nameof(ProcrastinationTime));
        _localSettings.Values["ProcrastinationTime"] = value;
      }
    }

    public int PomodoroCount
    {
      get => _pomodoroCount;
      set
      {
        if (value.Equals(_pomodoroCount)) return;
        _pomodoroCount = value;
        NotifyOfPropertyChange(nameof(PomodoroCount));
      }
    }


    public string Time => _timer1.Elapsed.ToString(@"mm\:ss");
    public string Time2 => _timer2.Elapsed.ToString(@"mm\:ss");

    private void StartTimer(Stopwatch timer)
    {
      timer.Start();
      AddScheduledNotification();
    }

    private void StopTimer(Stopwatch timer)
    {
      timer.Stop();
      RemoveScheduledNotification();
    }

    private void AddScheduledNotification()
    {
      var toast = BuildNotification(_technique);
      _toastNotifier.AddToSchedule(toast);
    }

    private void RemoveScheduledNotification()
    {
      if (_previousSceduledToastNotification == null) return;
      _toastNotifier.RemoveFromSchedule(_previousSceduledToastNotification);
    }

    private void ResetPomodoroCount()
    {
      PomodoroCount = 0;
    }

    private void SetTimerTechniquesSettings(string timerTechnique, bool isOn)
    {
      switch (timerTechnique)
      {
        case IsPomodoro:
          _localSettings.Values["PomodoroIsOn"] = isOn;
          break;
        case IsFiftySeven:
          _localSettings.Values["FiftySevenIsOn"] = isOn;
          break;
        case IsCustom:
          _localSettings.Values["CustomIsOn"] = isOn;
          break;
      }
    }

    private ScheduledToastNotification BuildNotification(string timerTechnique)
    {
      _toastTitle = IsTimerOneEnabled ? WorkingNotificationTitle : ProcrastinationNotificationTitle;
      switch (timerTechnique)
      {
        case IsPomodoro:
          // TODO: Set Pomodoro count and add the count to the message
          string pomodoroNumber;
          switch (PomodoroCount)
          {
            case 1:
              pomodoroNumber = "first";
              break;
            case 2:
              pomodoroNumber = "second";
              break;
            case 3:
              pomodoroNumber = "third";
              break;
            case 4:
              pomodoroNumber = "fourth";
              break;
            default:
              pomodoroNumber = "first";
              break;
          }
          _toastContent = IsTimerOneEnabled ? string.Format("You have finished your {0} pomodoro", pomodoroNumber) : string.Format("Just {0} more pomodoros to go", 4 - PomodoroCount);
          _dueTime = IsTimerOneEnabled ? DateTime.Now + TimeSpan.FromMinutes(PomodoroWorkingTime) : _dueTime = DateTime.Now + TimeSpan.FromMinutes(PomodoroProcrastinationTime);
          break;
        case IsFiftySeven:
          _toastContent = IsTimerOneEnabled ? "You have been working for over 52 minutes" : "You have been procrastinating for over 17 minutes";
          _dueTime = IsTimerOneEnabled ? DateTime.Now + TimeSpan.FromMinutes(FiftySevenWorkingTime) : _dueTime = DateTime.Now + TimeSpan.FromMinutes(FiftySevenProcrastinationTime);
          break;
        case IsCustom:
          _toastContent = "Custom Content";
          _dueTime = IsTimerOneEnabled ? DateTime.Now + TimeSpan.FromMinutes(WorkingTime) : _dueTime = DateTime.Now + TimeSpan.FromMinutes(ProcrastinationTime);
          break;
      }

      var visual = new ToastVisual
      {
        BindingGeneric = new ToastBindingGeneric
        {
          Children =
              {
                        new AdaptiveText
                        {
                            Text = _toastTitle
                        },

                        new AdaptiveText
                        {
                            Text = _toastContent
                        }
                    },

          AppLogoOverride = new ToastGenericAppLogo
          {
            Source = ToastLogo
          }
        }
      };


      //TODO: Notification Actions

      var content = new ToastContent
      {
        Visual = visual,
      };

      // TODO: Implement time correctly on toast depending on the style of timer chosen
      var toast = new ScheduledToastNotification(content.GetXml(), _dueTime);
      _previousSceduledToastNotification = toast;
      return toast;
    }
  }
}
