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
        public bool Playing = false;

        public MainPage()
        {
            this.InitializeComponent();
            MediaPlayer player = new MediaPlayer();
            MediaPlaybackList list = new MediaPlaybackList();

            this.playButton.Click += (object sender, RoutedEventArgs e) =>
            {
                if (!Playing && list.Items.Count > 0)
                {
                    player.Play();
                    Playing = !Playing;
                }
                else
                {
                    player.Pause();
                    Playing = !Playing;
                }
            };
 
            this.openButton.Click += async (object sender, RoutedEventArgs e) =>
            {
                var picker = new Windows.Storage.Pickers.FileOpenPicker();
                picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
                picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
                picker.FileTypeFilter.Add(".mp3");
                var files = await picker.PickMultipleFilesAsync();
                foreach (var file in files)
                {
                    list.Items.Add(new MediaPlaybackItem(MediaSource.CreateFromStorageFile(file)));
                    this.listView.Items.Add(file.Name);
                }
                player.Source = list;
            };

            this.preButton.Click += (object sender, RoutedEventArgs e) =>
            {
                list.MovePrevious();
            };

            this.nextButton.Click += (object sender, RoutedEventArgs e) =>
            {
                list.MoveNext();
            };
            this.listView.IsItemClickEnabled = true;

            this.listView.ItemClick += (object sender, ItemClickEventArgs e) =>
            {
                int index = listView.Items.IndexOf((string)e.ClickedItem);
                list.MoveTo((uint)index);
                if (player.PlaybackSession.PlaybackState == MediaPlaybackState.Paused)
                {
                    player.Play();
                }
            };
        }

        private void EventListenerRegister()
        {

        }
    }
}
