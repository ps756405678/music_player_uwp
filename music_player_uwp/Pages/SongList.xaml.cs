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
    public sealed partial class SongList : Page
    {

        public event SongSelectEventHandler SongSelect;
        public delegate void SongSelectEventHandler(object sender, SongSelectEventArgs e);

        public SongList()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var AllSongList = e.Parameter as Dictionary<string, IList<PlayItemModel>>;
            foreach(var songList in AllSongList)
            {
                TreeViewNode node = new TreeViewNode()
                {
                    Content = songList.Key,
                };
                foreach(var song in songList.Value)
                {
                    TreeViewNode child = new TreeViewNode()
                    {
                        Content = song.Author + " - " + song.Name + " - " + song.TotalTime.ToString(@"mm\:ss"),
                    };
                    node.Children.Add(child);
                }
                this.treeView.RootNodes.Add(node);
            }
            base.OnNavigatedTo(e);
        }

        private void EventRegister()
        {
            this.treeView.ItemInvoked += (TreeView sender, TreeViewItemInvokedEventArgs e) =>
            {
                var node = e.InvokedItem as TreeViewNode;
                string songList = node.Parent.Content.ToString();
                int index = node.Parent.Children.IndexOf(node);
                OnSongSelect(songList, index);
            };
        }

        private void OnSongSelect(string songListName, int index)
        {
            this.SongSelect(this, new SongSelectEventArgs(songListName, index));
        }
    }

    public class SongSelectEventArgs : EventArgs
    {
        public string SongListName { set; get; }
        public int Index { set; get; }
        public SongSelectEventArgs(string songListName, int index)
        {
            this.SongListName = songListName;
            this.Index = index;
        }
    }
}
