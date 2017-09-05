using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.StartScreen;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Caliburn.Micro;
using Procrastination_Timer.Messages;
using Procrastination_Timer.ViewModels;
using Procrastination_Timer.Views;

namespace Procrastination_Timer
{
  public sealed partial class App
  {
    private WinRTContainer _container;
    private IEventAggregator _eventAggregator;

    public App()
    {
      InitializeComponent();

      //TODO: Convert all constant strings to resource values
      var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
      var workingText = (string)localSettings.Values["WorkingText"];
      var procrastinationText = (string)localSettings.Values["ProcrastinationText"];
      var pomodoroIsOn = (bool?)localSettings.Values["PomodoroIsOn"];
      var fiftySevenIsOn = (bool?)localSettings.Values["FiftySevenIsOn"];
      var customIsOn = (bool?)localSettings.Values["CustomIsOn"];

      if (string.IsNullOrEmpty(workingText))
        localSettings.Values["WorkingText"] = "Working";
      if (string.IsNullOrEmpty(procrastinationText))
        localSettings.Values["ProcrastinationText"] = "Procrastinating"; ;
      if (pomodoroIsOn == null)
        localSettings.Values["PomodoroIsOn"] = true;
      if (fiftySevenIsOn == null)
        localSettings.Values["FiftySevenIsOn"] = false;
      if (customIsOn == null)
        localSettings.Values["CustomIsOn"] = false;
    }

    protected override void Configure()
    {
      _container = new WinRTContainer();
      _container.RegisterWinRTServices();

      _container
        .PerRequest<HomeViewModel>();

      _eventAggregator = _container.GetInstance<IEventAggregator>();
    }

    protected override void PrepareViewFirst(Frame rootFrame)
    {
      _container.RegisterNavigationService(rootFrame);
    }

    protected async override void OnLaunched(LaunchActivatedEventArgs args)
    {
      if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
      {
        _eventAggregator.PublishOnUIThread(new ResumeStateMessage());
      }

      await SetupJumpList();
      //DebugSettings.IsOverdrawHeatMapEnabled = true;
      //DebugSettings.EnableFrameRateCounter = true;
      //DebugSettings.IsTextPerformanceVisualizationEnabled = true;

      DisplayRootView<HomeView>(args.Arguments);
    }

    private new void DisplayRootView<T>(object parameter = null)
    {
      DisplayRootView(typeof(T), parameter);
    }

    private new void DisplayRootView(Type viewType, object parameter = null)
    {
      Initialize();

      PrepareViewFirst();

      RootFrame.Navigate(viewType, parameter);
      // Seems stupid but observed weird behaviour when resetting the Content
      if (Window.Current.Content == null)
        Window.Current.Content = RootFrame;

      var param = parameter as String;

      if (!string.IsNullOrEmpty(param))
      {
        var frame = Window.Current.Content as Frame;
        var page = frame?.Content as HomeView;
        page?.OnLaunchedEvent((string)parameter);
      }

      Window.Current.Activate();
    }

    protected override void OnSuspending(object sender, SuspendingEventArgs e)
    {
      _eventAggregator.PublishOnUIThread(new SuspendStateMessage(e.SuspendingOperation));
    }

    protected override object GetInstance(Type service, string key)
    {
      return _container.GetInstance(service, key);
    }

    protected override IEnumerable<object> GetAllInstances(Type service)
    {
      return _container.GetAllInstances(service);
    }

    protected override void BuildUp(object instance)
    {
      _container.BuildUp(instance);
    }

    private static async Task SetupJumpList()
    {
      JumpList jumpList = await JumpList.LoadCurrentAsync();
      jumpList.Items.Clear();

      JumpListItem startItem = JumpListItem.CreateWithArguments("start", "Start timer");
      startItem.Logo = new Uri("ms-appx:///Assets/start_image.png");

      jumpList.Items.Add(startItem);

      await jumpList.SaveAsync();
    }
  }
}
