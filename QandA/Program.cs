using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QandA
{
    public class Topic
    {
        public string name;

        public List<Question> questions;
    }

    public class Question
    {
        public string question, answer;
        public bool visible = false;
    }

    public class Program
    {
        static private string siteUrl = @"https://www.gov.pl/web/koronawirus/pytania-i-odpowiedzi?fbclid=IwAR1UEkrx66ib1Q6zDL4FbmVwDPnt-qP7aNQOfmP5VRYAj_EJ6o5w6jAES4c";

        static async Task Main(string[] args)
        {
            List<Topic> Topics = await Scrap();

            foreach (Topic t in Topics)
            {
                Console.WriteLine(t.name);
                foreach (var v in t.questions)
                {
                    Console.WriteLine("\t" + v.question + "\n");
                    Console.WriteLine("\t\t" + v.answer + "\n\n");
                }
            }
            Console.ReadKey();
        }

        public static async Task<List<Topic>> Scrap()
        {
            ScrapingBrowser browser = new ScrapingBrowser();

            var page = await browser.NavigateToPageAsync(new Uri(siteUrl));

            var main = page.Html.CssSelect("main");

            var divlist = main.CssSelect("div");

            //Console.WriteLine(main.First().InnerHtml);

            var maindivs = divlist.Where(s => s.OuterHtml.Contains("class=\"editor-content\""));
            //Console.WriteLine(maindivs.ToList()[1].InnerText);

            //var maindiv = maindivs.ToList()[1];

            var h3s = maindivs.CssSelect("h3");
            var h3ss = maindivs.CssSelect("details");

            //foreach(var v in h3ss) Console.WriteLine(v.InnerText);

            //Console.WriteLine(h3s.ToList()[0].NextSibling.OuterHtml);
            //Console.WriteLine(h3s.ToList()[0].NextSibling.InnerHtml);
            //Console.WriteLine(h3s.ToList()[0].NextSibling.InnerText);

            List<Topic> Topics = new List<Topic>();

            ListForTopic(h3s.First(), maindivs.ToList()[1]);

            foreach (var v in h3s)
            {
                Topic tmp = new Topic();

                tmp.name = v.InnerText;

                List<Question> questions = ListForTopic(v, maindivs.ToList()[1]);
                //Console.WriteLine(v.InnerText);
                //Console.WriteLine(v.GetNextSibling());
                tmp.questions = questions;

                Topics.Add(tmp);
            }

            return Topics;
            //Console.WriteLine(maindiv.Count());
        }


        private static List<Question> ListForTopic(HtmlNode element, HtmlNode content)
        {
            List<Question> Q = new List<Question>();

            string s = element.OuterHtml;

            int index = content.InnerHtml.IndexOf(s);
            index += element.OuterHtml.Length;
            int index2 = content.InnerHtml.IndexOf("</div>");
            index2 -= element.OuterHtml.Length-5;
            string html = content.InnerHtml.Substring(index, index2);

            HtmlNode n = new HtmlNode(HtmlNodeType.Element, new HtmlDocument(), 0);

            n.InnerHtml = html;

            var qlistfull = n.CssSelect("details").ToList();
            var qlistQuestion = qlistfull.CssSelect("summary").ToList();

            for (int i = 0; i < qlistfull.Count(); i++)
            {
                Question q = new Question();
                q.question = qlistQuestion[i].InnerText;
                q.answer = (qlistfull[i].InnerText.Replace(q.question, ""));

                //Console.WriteLine(q.question);
                //Console.WriteLine(q.answer);

                Q.Add(q);
            }

            //Console.WriteLine(qlistQuestion.Last().InnerText);

            return Q;
        }
    }
}
