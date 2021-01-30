namespace Calendar.UserInterface
{
    using System;
    using System.Windows;
    using System.Windows.Threading;

    public partial class MainWindow : Window, IMainWindow
    {
        private readonly DispatcherTimer dispatcherTimer;
        private readonly MainWindowViewModel mainWindowViewModel;

        public MainWindow()
        {
            this.DataContext = this.mainWindowViewModel = new MainWindowViewModel(this);
            this.InitializeComponent();

            this.Topmost = true;
            this.ResizeMode = ResizeMode.NoResize;
            this.WindowState = WindowState.Minimized;

            this.dispatcherTimer = new DispatcherTimer();
            this.dispatcherTimer.Tick += new EventHandler(this.OnTimerTick);

            this.mainWindowViewModel.OnMainWindowInitialized();
        }

        public void Minimize()
        {
            this.WindowState = WindowState.Minimized;
        }

        public void StartTimer(DateTime targetTime)
        {
            this.dispatcherTimer.Interval = targetTime - DateTime.Now;
            this.dispatcherTimer.IsEnabled = true;
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            this.mainWindowViewModel.OnTimerTick();
            this.WindowState = WindowState.Normal;
        }
    }
}
