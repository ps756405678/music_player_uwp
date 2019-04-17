using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace music_player_uwp.Pages
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class PlayerControlPage : Page
    {
        public TimeSpan TotalTime { set; get; }
        public TimeSpan CurrentTime { set; get; }
        public int CurrentSongIndex { set; get; }

        private bool Running = false;

        private DispatcherTimer timer;
        private IList<PlayItemModel> SongList;

        public event TimeChangeHandler TimeChange;
        public delegate void TimeChangeHandler(object sender, TimeChangeEventArgs e);

        public event PlayEndHandler PlayEnd;
        public delegate void PlayEndHandler(object sender, PlayEndEventArgs e);

        public PlayerControlPage()
        {
            this.InitializeComponent();

            // Object init
            Init();
            
            // Event handler registe
            EventRegister();
        }

        public void Start()
        {
            this.PlayButton.FontFamily = new FontFamily("Segoe MDL2 Assets"); ;
            this.PlayButton.Content = Constant.PAUSE_ICON;
            this.TimeText.Text = "00:00/" + this.TotalTime.ToString(Constant.TIME_FORMMAT);
            this.timer.Start();
        }

        public void Stop()
        {
            this.timer.Stop();
        }

        public void Pause()
        {
            this.PlayButton.FontFamily = new FontFamily("Segoe MDL2 Assets");
            this.PlayButton.Content = Constant.PLAY_ICON;
            this.timer.Stop();
        }

        private void Init()
        {
            this.CurrentSongIndex = 0;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
        }

        private void EventRegister()
        {
            // When time change.
            this.ProcessBar.Tapped += (object sender, TappedRoutedEventArgs e) =>
            {
                Point pos = e.GetPosition(this.ProcessBar);
                this.ProcessBar.Value = 100 * pos.X / this.ProcessBar.ActualWidth;
                OnTimeChange(this.ProcessBar.Value);
            };

            // default time changer event handler
            this.TimeChange += (object sender, TimeChangeEventArgs e) =>
            {
                
            };

            // Timer update
            this.timer.Tick += (object sender, object e) =>
            {
                this.CurrentTime = this.CurrentTime.Add(TimeSpan.FromSeconds(1));
                this.ProcessBar.Value = 100 * this.CurrentTime.TotalMilliseconds / this.TotalTime.TotalMilliseconds;
                this.TimeText.Text = this.CurrentTime.ToString(Constant.TIME_FORMMAT) + "/" + this.TotalTime.ToString(Constant.TIME_FORMMAT);
                if (CurrentTime == TotalTime)
                {
                    this.CurrentSongIndex += 1;
                    OnPlayEnd(this.CurrentSongIndex);
                }
            };

            this.PlayEnd += (object sender, PlayEndEventArgs e) =>
            {
                this.timer.Stop();
            };

            this.PlayButton.Click += (object sender, RoutedEventArgs e) =>
            {
                if (!Running)
                {
                    this.Start();
                    Running = !Running;
                }
                else
                {
                    this.Pause();
                    Running = !Running;
                }
            };
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.SongList = (IList<PlayItemModel>)e.Parameter;
            this.TotalTime = SongList[0].TotalTime;
            base.OnNavigatedTo(e);
        }

        private void OnTimeChange(double time)
        {
            TimeChange(this, new TimeChangeEventArgs(time));
        }

        private void OnPlayEnd(int nextIndex)
        {
            PlayEnd(this, new PlayEndEventArgs(nextIndex));
        }
    }

    public class TimeChangeEventArgs : EventArgs
    {
        public double CurrentTime { set; get; }
        
        public TimeChangeEventArgs(double time)
        {
            this.CurrentTime = time;
        }
    }

    public class PlayEndEventArgs : EventArgs
    {
        public int NextSong { set; get; }

        public PlayEndEventArgs(int next)
        {
            this.NextSong = next;
        }
    }
}
