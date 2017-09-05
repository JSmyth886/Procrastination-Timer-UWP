using Procrastination_Timer.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Procrastination_Timer.Views
{
  public sealed partial class HomeView
  {
    public HomeViewModel Vm => (HomeViewModel)DataContext;

    public HomeView()
    {
      InitializeComponent();

      SplitView.RegisterPropertyChangedCallback(SplitView.IsPaneOpenProperty, IsPaneOpenPropertyChanged);
    }

    private void IsPaneOpenPropertyChanged(DependencyObject sender, DependencyProperty dp)
    {
      var splitView = sender as SplitView;
      if (splitView != null && splitView.IsPaneOpen)
      {
        FindName("PaneContent");
      }
    }

    public void OnLaunchedEvent(string arguments)
    {
      if (Vm == null) return;
      if (Vm.PlayCommand.CanExecute(null))
        Vm.PlayCommand.Execute(null);
    }
  }
}
