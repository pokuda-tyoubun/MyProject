using FxCommonLib.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FxCommonLib.Utils {
    public struct TagInfo {
        public string ContentId;
        public string Title;
        public string Performers;
        public string Genres;
        public string Maker;
        public string Director;
        public string Comment;
        public string ImageUrl;
        public string PageUrl;
    }
    public class DMMWebAPIUtil {
        public List<TagInfo> GetItemList(string site, string keyword) {
            List<TagInfo> ret = new List<TagInfo>();
            WebResponse response = null;
            try {
                const string apiId = "zx5gaKL4fwyKDeBzUZ1q";
                const string affiliateId = "PokudaEng-990";

                //DVD通販
                const string service = "mono";
                const string floorCd = "dvd";
                //動画ビデオ
                //const string service = "digital";
                //const string floorCd = "videoa";
                UTF8Encoding utf8 = new UTF8Encoding();
                string url = @"https://api.dmm.com/affiliate/v3/ItemList?api_id=" + apiId + @"&affiliate_id=" + affiliateId +
                    //@"&site=FANZA&service=" + service + @"&floor=" + floorCd + @"&hits=10&sort=date&keyword=" + keyword + "&output=json";
                    @"&site=" + site + @"&service=" + service + @"&floor=" + floorCd + @"&hits=10&sort=date&keyword=" + keyword + "&output=json";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader sReader = new StreamReader(dataStream);
                string jsonString = sReader.ReadToEnd();
                Debug.Write(jsonString);

                var jo = JObject.Parse(jsonString);
                var tokens = jo.SelectTokens("$.result.items");
                foreach (var tmp in tokens.Children()) {
                    var contentId = tmp.SelectToken("$.content_id");
                    if (contentId != null) {
                        var tag = new TagInfo();
                        tag.ContentId = contentId.ToString();

                        var title = tmp.SelectToken("$.title");
                        tag.Title = title.ToString();

                        var actressTokens = tmp.SelectTokens("$.iteminfo.actress");
                        string performers = "";
                        foreach (var val in actressTokens.Children()) {
                            performers += val.SelectToken("$.name").ToString() + " ";
                        }
                        tag.Performers = performers;

                        var genreTokens = tmp.SelectTokens("$.iteminfo.genre");
                        string genre = "";
                        foreach (var val in genreTokens.Children()) {
                            genre += val.SelectToken("$.name").ToString() + " ";
                        }
                        tag.Genres = genre;

                        var directorTokens = tmp.SelectTokens("$.iteminfo.director");
                        string directors = "";
                        foreach (var val in directorTokens.Children()) {
                            directors += val.SelectToken("$.name").ToString() + " ";
                        }
                        tag.Director = directors;

                        var makerTokens = tmp.SelectTokens("$.iteminfo.maker");
                        string makers = "";
                        foreach (var val in makerTokens.Children()) {
                            makers += val.SelectToken("$.name").ToString() + " ";
                        }
                        tag.Maker = makers;

                        var image = tmp.SelectToken("$.imageURL.small");
                        tag.ImageUrl = image.ToString();

                        var page = tmp.SelectToken("$.URL");
                        tag.PageUrl = page.ToString();

                        ret.Add(tag);
                    }
                }
                return ret;
            } catch (System.Net.WebException we) {
                throw we;
            } finally {
                if (response != null) {
                    response.Close();
                }
            }
        }
    }
}
