using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace music_player_uwp
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly List<(string tag, Type page)> _pages = new List<(string tag, Type page)>
        {
            ("SongList", typeof(SongList)),
            ("CloudList", typeof(CloudList)),
            ("LocationList", typeof(LocationList)),
        };

        public bool IsExpande = true;

        public MainPage()
        {
            this.InitializeComponent();
            EventListenerRegister();
        }

        private void EventListenerRegister()
        {
            this.navi.SelectionChanged += (NavigationView sender, NavigationViewSelectionChangedEventArgs e) =>
            {
                var itemTag = e.SelectedItemContainer.Tag.ToString();
                Type page = _pages.Find((tuple) =>
                {
                    return tuple.tag == itemTag;
                }).page;
                this.frame.Navigate(page);
            };
        }
    }
}
