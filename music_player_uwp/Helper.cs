using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Storage;

namespace music_player_uwp
{
    class Helper
    {
        public static async Task<Dictionary<string, IList<PlayItemModel>>> ReadAllListAsync()
        {
            Dictionary<string, IList<PlayItemModel>> AllSongList = new Dictionary<string, IList<PlayItemModel>>();

            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.GetFileAsync(Constant.SONG_LIST_FILE);

            XmlDocument document = new XmlDocument();
            var xmlStr = await FileIO.ReadTextAsync(file, Windows.Storage.Streams.UnicodeEncoding.Utf8);
            document.LoadXml(xmlStr);
            var AllSongListNode = document.GetElementsByTagName("SongList");
            foreach (var songList in AllSongListNode)
            {
                IList<PlayItemModel> OneSongList = new List<PlayItemModel>(songList.ChildNodes.Count);
                foreach (var item in songList.ChildNodes)
                {
                    if (!item.GetXml().StartsWith("\r\n"))
                    {
                        PlayItemModel model = new PlayItemModel();
                        model.Name = item.Attributes.GetNamedItem("Name").NodeValue.ToString();
                        model.Author = item.Attributes.GetNamedItem("Author").NodeValue.ToString();
                        model.TotalTime = TimeSpan.FromSeconds(int.Parse(item.Attributes.GetNamedItem("TotalTime").NodeValue.ToString()));
                        model.Ablum = item.Attributes.GetNamedItem("Ablum").NodeValue.ToString();
                        model.ResourceLocation = item.Attributes.GetNamedItem("ResourceLocation").NodeValue.ToString();
                        model.ResourceType = int.Parse(item.Attributes.GetNamedItem("ResourceType").NodeValue.ToString());
                        OneSongList.Add(model);
                    }
                }
                AllSongList.Add(songList.Attributes.GetNamedItem("name").NodeValue.ToString(), OneSongList);
            }

            //XmlDocument xmlDocument = new XmlDocument();
            //xmlDocument.
            //xmlDocument.LoadXml(await FileIO.ReadTextAsync(file, Windows.Storage.Streams.UnicodeEncoding.Utf8));
            //var AllSongListNode = xmlDocument.GetElementsByTagName

            return AllSongList;
        }
    }
}
