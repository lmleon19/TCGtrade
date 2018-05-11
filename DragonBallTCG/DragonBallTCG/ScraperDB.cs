using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DragonBallTCG
{
    public class ScraperDB
    {
        public void Init()
        {
            string baseURL = "http://www.dbs-cardgame.com/us-en/cardlist/index.php?search=true";
            List<string> ranks = new List<string>() { "LEADER", "BATTLE", "EXTRA" };
            foreach(string rank in ranks)
            {
                CreateRequestByRank(baseURL, rank);
            }
        }

        private HtmlDocument CreateRequestByRank(string baseURL, string rank)
        {
            HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlDocument();

            var request = (HttpWebRequest)WebRequest.Create(baseURL);
            var postData = string.Format("rank={0}", rank);
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            htmlDocument.LoadHtml(responseString);

            var cards = htmlDocument.DocumentNode.SelectNodes("//div[@id='listCol']/ul/li").ToList();

            return htmlDocument;
        }

        private void ExtracData(HtmlNode htmlNode)
        {
            var cardNumber = htmlNode.SelectSingleNode("//dl/dt[contains(@class,'cardNumber')]");
            var cardName = htmlNode.SelectSingleNode("//dl/dd[contains(@class,'cardName')]");

            var leftCol = htmlNode.SelectSingleNode("//dl/dd[contains(@class,'leftCol clearfix')]");
            var cardImage = leftCol.SelectSingleNode("//div[contains(@class,'cardimg')/img]");
            var series = leftCol.SelectSingleNode("//dl[contains(@class,'seriesCol')/dd]");
            var rarity = leftCol.SelectSingleNode("//dl[contains(@class,'rarityCol')/dd]");

            var rightCol = htmlNode.SelectSingleNode("//dl/dd[contains(@class,'rightCol clearfix')]");
            var typeCol = rightCol.SelectSingleNode("//dl[contains(@class,'typeCol')]/dd");
            var colorCol = rightCol.SelectSingleNode("//dl[contains(@class,'colorCol')]/dd");
            var powerCol = rightCol.SelectSingleNode("//dl[contains(@class,'powerCol')]/dd");
            var characterCol = rightCol.SelectSingleNode("//dl[contains(@class,'characterCol')]/dd");
            var specialTraitCol = rightCol.SelectSingleNode("//dl[contains(@class,'specialTraitCol')]/dd");
            var eraCol = rightCol.SelectSingleNode("//dl[contains(@class,'eraCol')]/dd");
            var skillCol = rightCol.SelectSingleNode("//dl[contains(@class,'skillCol')]/dd");

            var bottomCol = htmlNode.SelectSingleNode("//dl/dd[contains(@class,'bottomCol')]");
            var availableDateCol = htmlNode.SelectSingleNode("//dl[contains(@class,'availableDateCol')]");
        }
    }
}
