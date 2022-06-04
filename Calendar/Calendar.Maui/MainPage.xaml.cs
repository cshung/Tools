namespace Calendar.Maui
{
    using Microsoft.Maui.Dispatching;
    using System;

    public partial class MainPage : ContentPage, IMainWindow
    {
        private readonly IDispatcherTimer dispatcherTimer;
        private readonly MainWindowViewModel mainWindowViewModel;

        public MainPage()
        {
            this.BindingContext = this.mainWindowViewModel = new MainWindowViewModel(this);
            this.InitializeComponent();

            // this.Topmost = true;
            // this.ResizeMode = ResizeMode.NoResize;
            // this.WindowState = WindowState.Minimized;

            this.dispatcherTimer = Dispatcher.CreateTimer();
            this.dispatcherTimer.Tick += new EventHandler(this.OnTimerTick);

            this.mainWindowViewModel.OnMainWindowInitialized();
        }

        public void Minimize()
        {
            // this.WindowState = WindowState.Minimized;
            var a = this;
            var b = a.GetParentWindow();
            var c = b?.Handler;
            var d = c?.PlatformView;
            Console.WriteLine(d?.GetType()?.FullName);
        }

        public void StartTimer(TimeSpan fromNow)
        {
            this.dispatcherTimer.Interval = fromNow;
            this.dispatcherTimer.Start();
        }

        private void OnTimerTick(object? sender, EventArgs e)
        {
            this.mainWindowViewModel.OnTimerTick();
            // this.WindowState = WindowState.Normal;
        }
    }
}
