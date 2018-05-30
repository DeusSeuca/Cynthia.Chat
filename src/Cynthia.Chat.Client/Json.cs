using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Cynthia.Chat.Client
{
    public class Json
    {
        public static async Task<T> GetJson<T>(string url)
        {
            var request = HttpWebRequest.Create(url);
            using (var response = await request.GetResponseAsync())
            using (var stream = response.GetResponseStream())
            {
                return GetJson<T>(stream);
            }
        }

        public static T GetJson<T>(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            using (var jsonreader = new JsonTextReader(reader))
            {
                return new JsonSerializer().Deserialize<T>(jsonreader);
            }
        }

        public static async Task<IEnumerable<string>> PostJson<T>(string url, T data)
        {
            using (var client = new HttpClient())
            using (var content = new StringContent(JsonConvert.SerializeObject(data)))
            {
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                using (var response = await client.PostAsync(url, content))
                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                using (var jsonreader = new JsonTextReader(reader))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return new JsonSerializer().Deserialize<IEnumerable<string>>(jsonreader);
                    }
                    else
                    {
                        return new[] { "连接失败" };
                    }
                }
            }
        }
    }
}