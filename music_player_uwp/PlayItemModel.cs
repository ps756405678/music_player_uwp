using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace music_player_uwp
{
    class PlayItemModel
    {
        public string Name { set; get; }
        public string Author { set; get; }
        public TimeSpan TotalTime { set; get; }
        public string ResourceLocation { set; get; }
        public int ResourceType { set; get; }
    }
}
