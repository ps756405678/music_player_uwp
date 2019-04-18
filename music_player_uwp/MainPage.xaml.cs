using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
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
            ("SongList", typeof(Pages.SongList)),
            ("CloudList", typeof(CloudList)),
            ("LocationList", typeof(LocationList)),
        };
        
        private Pages.PlayerControlPage playerControlPage;

        private Dictionary<string, IList<PlayItemModel>> AllSongList;

        public MainPage()
        {
            this.InitializeComponent();
            Init();
            EventListenerRegister();
        }

        private void Init()
        {

        }

        private void EventListenerRegister()
        {
            // The main navigator onloaded event handler.
            this.navi.Loaded += async (object sender, RoutedEventArgs e) =>
            {
                var songList = await Helper.ReadAllListAsync();
                this.frame.Navigate(typeof(Pages.SongList), songList);
            };

            // The function view navigator onloaded event handler.
            this.ExpandeFrame.Loaded += (object sender, RoutedEventArgs e) =>
            {
                this.ExpandeFrame.Navigate(typeof(Pages.Search));
            };

            // The player control frame onloaded event.
            this.PlayFrame.Loaded += (object sender, RoutedEventArgs e) =>
            {
                this.PlayFrame.Navigate(typeof(Pages.PlayerControlPage));
                playerControlPage = (Pages.PlayerControlPage)PlayFrame.Content;
            };

            // The main navigator onselectionchanger event handler.
            this.navi.ItemInvoked += (NavigationView sender, NavigationViewItemInvokedEventArgs e) =>
            {
                var itemTag = e.InvokedItemContainer.Tag.ToString();
                Type page = _pages.Find((tuple) =>
                {
                    return tuple.tag == itemTag;
                }).page;
                this.frame.Navigate(page);
            };
        }
    }
}
