﻿using System;
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

namespace music_player_uwp
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class CloudList : Page
    {
        public CloudList()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
            Console.WriteLine("Cloud Page constrate");
            IList<TreeViewNode> roots = new List<TreeViewNode>()
            {
                new TreeViewNode()
                {
                    Content = "CloudList1"
                },
                new TreeViewNode()
                {
                    Content = "CloudList2"
                }
            };

            this.treeView.ItemsSource = roots;
        }
    }
}
