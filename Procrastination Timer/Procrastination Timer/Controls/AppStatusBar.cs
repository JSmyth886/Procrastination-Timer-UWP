using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Procrastination_Timer.Common;

namespace Procrastination_Timer.Controls
{
  public static class AppStatusBar
  {
    private static readonly ApplicationViewTitleBar _titleBar = ApplicationView.GetForCurrentView().TitleBar;
    private static StatusBar _statusBar;


    public static readonly DependencyProperty ForegroundColorProperty =
      DependencyProperty.RegisterAttached("ForegroundColor", typeof(SolidColorBrush),
        typeof(AppStatusBar),
        new PropertyMetadata(null, OnForegroundColorPropertyChanged));

    public static SolidColorBrush GetForegroundColor(DependencyObject d)
    {
      return (SolidColorBrush)d.GetValue(ForegroundColorProperty);
    }

    public static void SetForegroundColor(DependencyObject d, SolidColorBrush value)
    {
      d.SetValue(ForegroundColorProperty, value);
    }

    private static void OnForegroundColorPropertyChanged(DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      var color = (SolidColorBrush)e.NewValue;
      _titleBar.ForegroundColor = color.Color;
      if (SystemInfo.IsMobile)
      {
        _statusBar.ForegroundColor = color.Color;
      }
    }

    public static readonly DependencyProperty BackgroundColorProperty =
      DependencyProperty.RegisterAttached("BackgroundColor", typeof(SolidColorBrush),
        typeof(AppStatusBar),
        new PropertyMetadata(null, OnBackgroundColorPropertyChanged));

    public static SolidColorBrush GetBackgroundColor(DependencyObject d)
    {
      return (SolidColorBrush)d.GetValue(BackgroundColorProperty);
    }

    public static void SetBackgroundColor(DependencyObject d, SolidColorBrush value)
    {
      d.SetValue(BackgroundColorProperty, value);
    }

    private static void OnBackgroundColorPropertyChanged(DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      var color = (SolidColorBrush)e.NewValue;
      _titleBar.BackgroundColor = color.Color;
      if (SystemInfo.IsMobile)
      {
        _statusBar = StatusBar.GetForCurrentView();
        _statusBar.BackgroundOpacity = 1;
        _statusBar.BackgroundColor = color.Color;
      }
    }

    public static readonly DependencyProperty ButtonForegroundColorProperty =
      DependencyProperty.RegisterAttached("ButtonForegroundColor", typeof(SolidColorBrush),
        typeof(AppStatusBar),
        new PropertyMetadata(null, OnButtonForegroundColorPropertyChanged));

    public static SolidColorBrush GetButtonForegroundColor(DependencyObject d)
    {
      return (SolidColorBrush)d.GetValue(ButtonForegroundColorProperty);
    }

    public static void SetButtonForegroundColor(DependencyObject d, SolidColorBrush value)
    {
      d.SetValue(ButtonForegroundColorProperty, value);
    }

    private static void OnButtonForegroundColorPropertyChanged(DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      var color = (SolidColorBrush)e.NewValue;
      _titleBar.ButtonForegroundColor = color.Color;
    }

    public static readonly DependencyProperty ButtonBackgroundColorProperty =
      DependencyProperty.RegisterAttached("ButtonBackgroundColor", typeof(SolidColorBrush),
        typeof(AppStatusBar),
        new PropertyMetadata(null, OnButtonBackgroundColorPropertyChanged));

    public static SolidColorBrush GetButtonBackgroundColor(DependencyObject d)
    {
      return (SolidColorBrush)d.GetValue(ButtonBackgroundColorProperty);
    }

    public static void SetButtonBackgroundColor(DependencyObject d, SolidColorBrush value)
    {
      d.SetValue(ButtonBackgroundColorProperty, value);
    }

    private static void OnButtonBackgroundColorPropertyChanged(DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      var color = (SolidColorBrush)e.NewValue;
      _titleBar.ButtonBackgroundColor = color.Color;
    }

    public static readonly DependencyProperty ButtonHoverBackgroundColorProperty =
      DependencyProperty.RegisterAttached("ButtonHoverBackgroundColor", typeof(SolidColorBrush),
        typeof(AppStatusBar),
        new PropertyMetadata(null, OnButtonHoverBackgroundColorPropertyChanged));

    public static SolidColorBrush GetButtonHoverBackgroundColor(DependencyObject d)
    {
      return (SolidColorBrush)d.GetValue(ButtonHoverBackgroundColorProperty);
    }

    public static void SetButtonHoverBackgroundColor(DependencyObject d, SolidColorBrush value)
    {
      d.SetValue(ButtonHoverBackgroundColorProperty, value);
    }

    private static void OnButtonHoverBackgroundColorPropertyChanged(DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      var color = (SolidColorBrush)e.NewValue;
      _titleBar.ButtonHoverBackgroundColor = color.Color;
    }

    public static readonly DependencyProperty ButtonHoverForegroundColorProperty =
      DependencyProperty.RegisterAttached("ButtonHoverForegroundColor", typeof(SolidColorBrush),
        typeof(AppStatusBar),
        new PropertyMetadata(null, OnButtonHoverForegroundColorPropertyChanged));

    public static SolidColorBrush GetButtonHoverForegroundColor(DependencyObject d)
    {
      return (SolidColorBrush)d.GetValue(ButtonHoverForegroundColorProperty);
    }

    public static void SetButtonHoverForegroundColor(DependencyObject d, SolidColorBrush value)
    {
      d.SetValue(ButtonHoverForegroundColorProperty, value);
    }

    private static void OnButtonHoverForegroundColorPropertyChanged(DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      var color = (SolidColorBrush)e.NewValue;
      _titleBar.ButtonHoverForegroundColor = color.Color;
    }

    public static readonly DependencyProperty ButtonPressedBackgroundColorProperty =
      DependencyProperty.RegisterAttached("ButtonPressedBackgroundColor", typeof(SolidColorBrush),
        typeof(AppStatusBar),
        new PropertyMetadata(null, OnButtonPressedBackgroundColorPropertyChanged));

    public static SolidColorBrush GetButtonPressedBackgroundColor(DependencyObject d)
    {
      return (SolidColorBrush)d.GetValue(ButtonPressedBackgroundColorProperty);
    }

    public static void SetButtonPressedBackgroundColor(DependencyObject d, SolidColorBrush value)
    {
      d.SetValue(ButtonPressedBackgroundColorProperty, value);
    }

    private static void OnButtonPressedBackgroundColorPropertyChanged(DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      var color = (SolidColorBrush)e.NewValue;
      _titleBar.ButtonPressedBackgroundColor = color.Color;
    }

    public static readonly DependencyProperty ButtonPressedForegroundColorProperty =
      DependencyProperty.RegisterAttached("ButtonPressedForegroundColor", typeof(SolidColorBrush),
        typeof(AppStatusBar),
        new PropertyMetadata(null, OnButtonPressedForegroundColorPropertyChanged));

    public static SolidColorBrush GetButtonPressedForegroundColor(DependencyObject d)
    {
      return (SolidColorBrush)d.GetValue(ButtonPressedForegroundColorProperty);
    }

    public static void SetButtonPressedForegroundColor(DependencyObject d, SolidColorBrush value)
    {
      d.SetValue(ButtonPressedForegroundColorProperty, value);
    }

    private static void OnButtonPressedForegroundColorPropertyChanged(DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      var color = (SolidColorBrush)e.NewValue;
      _titleBar.ButtonPressedForegroundColor = color.Color;
    }

    public static readonly DependencyProperty InactiveBackgroundColorProperty =
      DependencyProperty.RegisterAttached("InactiveBackgroundColor", typeof(SolidColorBrush),
        typeof(AppStatusBar),
        new PropertyMetadata(null, OnInactiveBackgroundColorPropertyChanged));

    public static SolidColorBrush GetInactiveBackgroundColor(DependencyObject d)
    {
      return (SolidColorBrush)d.GetValue(InactiveBackgroundColorProperty);
    }

    public static void SetInactiveBackgroundColor(DependencyObject d, SolidColorBrush value)
    {
      d.SetValue(InactiveBackgroundColorProperty, value);
    }

    private static void OnInactiveBackgroundColorPropertyChanged(DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      var color = (SolidColorBrush)e.NewValue;
      _titleBar.InactiveBackgroundColor = color.Color;
    }

    public static readonly DependencyProperty InactiveForegroundColorProperty =
      DependencyProperty.RegisterAttached("InactiveForegroundColor", typeof(SolidColorBrush),
        typeof(AppStatusBar),
        new PropertyMetadata(null, OnInactiveForegroundColorPropertyChanged));

    public static SolidColorBrush GetInactiveForegroundColor(DependencyObject d)
    {
      return (SolidColorBrush)d.GetValue(InactiveForegroundColorProperty);
    }

    public static void SetInactiveForegroundColor(DependencyObject d, SolidColorBrush value)
    {
      d.SetValue(InactiveForegroundColorProperty, value);
    }

    private static void OnInactiveForegroundColorPropertyChanged(DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      var color = (SolidColorBrush)e.NewValue;
      _titleBar.InactiveForegroundColor = color.Color;
    }

    public static readonly DependencyProperty ButtonInactiveBackgroundColorProperty =
      DependencyProperty.RegisterAttached("ButtonInactiveBackgroundColor", typeof(SolidColorBrush),
        typeof(AppStatusBar),
        new PropertyMetadata(null, OnButtonInactiveBackgroundColorPropertyChanged));

    public static SolidColorBrush GetButtonInactiveBackgroundColor(DependencyObject d)
    {
      return (SolidColorBrush)d.GetValue(ButtonInactiveBackgroundColorProperty);
    }

    public static void SetButtonInactiveBackgroundColor(DependencyObject d, SolidColorBrush value)
    {
      d.SetValue(ButtonInactiveBackgroundColorProperty, value);
    }

    private static void OnButtonInactiveBackgroundColorPropertyChanged(DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      var color = (SolidColorBrush)e.NewValue;
      _titleBar.ButtonInactiveBackgroundColor = color.Color;
    }

    public static readonly DependencyProperty ButtonInactiveForegroundColorProperty =
      DependencyProperty.RegisterAttached("ButtonInactiveForegroundColor", typeof(SolidColorBrush),
        typeof(AppStatusBar),
        new PropertyMetadata(null, OnButtonInactiveForegroundColorPropertyChanged));

    public static SolidColorBrush GetButtonInactiveForegroundColor(DependencyObject d)
    {
      return (SolidColorBrush)d.GetValue(ButtonInactiveForegroundColorProperty);
    }

    public static void SetButtonInactiveForegroundColor(DependencyObject d, SolidColorBrush value)
    {
      d.SetValue(ButtonInactiveForegroundColorProperty, value);
    }

    private static void OnButtonInactiveForegroundColorPropertyChanged(DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      var color = (SolidColorBrush)e.NewValue;
      _titleBar.ButtonInactiveForegroundColor = color.Color;
    }
  }
}
