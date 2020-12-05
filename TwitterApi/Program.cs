using Nancy.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Twitter
{
    public class Program
    {
        public static async Task<List<string>> GetPopularTweetsForAccountAsync(string name, int count)
        {
                 string baseUrl = "https://api.twitter.com/1.1/search/tweets.json?q=from:{0}&result_type=popular&count={1}";
            string userUrl = String.Format(baseUrl, name, count.ToString());
            using var client = new HttpClient
            {
                BaseAddress = new Uri(userUrl)
            };
            client.DefaultRequestHeaders.Add("authorization", "Bearer AAAAAAAAAAAAAAAAAAAAAFZrKQEAAAAAm5UfW%2BvgJxnrGOKKnjZgQ3N9p6c%3DQwFY5gWh7uZxxaXqW4dh01JcyeHpz7R355aQooc17LR5wEEYHu");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(userUrl);
            response.EnsureSuccessStatusCode();
            var resp = await response.Content.ReadAsStringAsync();
            string tweetUrl = "https://twitter.com/{0}/status/{1}";
            var items = JsonConvert.DeserializeObject<TweetRootV1>(resp);
            List<string> result = new List<string>();
            foreach (var item in items.data)
            {
                result.Add(string.Format(tweetUrl, name, item.id.ToString()));
            }
            return result;
        }

        public static async Task<List<string>> GetPopularTweetsForTagAsync(string name, int count)
        {
            string baseUrl = "https://api.twitter.com/1.1/search/tweets.json?q=%23{0}&result_type=popular&count={1}";
            string userUrl = String.Format(baseUrl, name, count.ToString());
            using var client = new HttpClient
            {
                BaseAddress = new Uri(userUrl)
            };
            client.DefaultRequestHeaders.Add("authorization", "Bearer AAAAAAAAAAAAAAAAAAAAAFZrKQEAAAAAm5UfW%2BvgJxnrGOKKnjZgQ3N9p6c%3DQwFY5gWh7uZxxaXqW4dh01JcyeHpz7R355aQooc17LR5wEEYHu");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(userUrl);
            response.EnsureSuccessStatusCode();
            var resp = await response.Content.ReadAsStringAsync();
            string tweetUrl = "https://twitter.com/{0}/status/{1}";
            var items = JsonConvert.DeserializeObject<TweetRootV1>(resp);
            List<string> result = new List<string>();
            foreach (var item in items.data)
            {
                result.Add(string.Format(tweetUrl, name, item.id.ToString()));
            }
            return result;
        }

        public static async Task<List<string>> GetRecentTweetsForAccountAsync(string name, int count)
        {
            string baseUrl = "https://api.twitter.com/2/tweets/search/recent?query=from:{0}&max_results={1}";
            string userUrl = String.Format(baseUrl, name, count.ToString());
            using var client = new HttpClient
            {
                BaseAddress = new Uri(userUrl)
            };
            client.DefaultRequestHeaders.Add("authorization", "Bearer AAAAAAAAAAAAAAAAAAAAAFZrKQEAAAAAm5UfW%2BvgJxnrGOKKnjZgQ3N9p6c%3DQwFY5gWh7uZxxaXqW4dh01JcyeHpz7R355aQooc17LR5wEEYHu");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(userUrl);
            response.EnsureSuccessStatusCode();
            var resp = await response.Content.ReadAsStringAsync();
            string tweetUrl = "https://twitter.com/{0}/status/{1}";
            var items = JsonConvert.DeserializeObject<TweetRoot>(resp);
            List<string> result = new List<string>();
            foreach (var item in items.data)
            {
                result.Add(string.Format(tweetUrl, name, item.id.ToString()));
            }
            return result;
        }

        public static async Task<List<string>> GetRecentTweetsForTagAsync(string name, int count)
        {
            string baseUrl = "https://api.twitter.com/2/tweets/search/recent?query=%23{0}&max_results={1}";
            string userUrl = String.Format(baseUrl, name, count.ToString());
            using var client = new HttpClient
            {
                BaseAddress = new Uri(userUrl)
            };
            client.DefaultRequestHeaders.Add("authorization", "Bearer AAAAAAAAAAAAAAAAAAAAAFZrKQEAAAAAm5UfW%2BvgJxnrGOKKnjZgQ3N9p6c%3DQwFY5gWh7uZxxaXqW4dh01JcyeHpz7R355aQooc17LR5wEEYHu");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(userUrl);
            response.EnsureSuccessStatusCode();
            var resp = await response.Content.ReadAsStringAsync();
            string tweetUrl = "https://twitter.com/{0}/status/{1}";
            var items = JsonConvert.DeserializeObject<TweetRoot>(resp);
            List<string> result = new List<string>();
            foreach (var item in items.data)
            {
                result.Add(string.Format(tweetUrl, name, item.id.ToString()));
            }
            return result;
        }

    }

    public class TweetRootV1
    {
        [JsonProperty("statuses")]
        public List<Tweet> data { get; set; }
    }



    public class TweetRoot
    {
        [JsonProperty("data")]
        public List<Tweet> data { get; set; }
    }

    public class Tweet
    {
        [JsonProperty("id")]
        public string id { get; set; }
    }
  
}
