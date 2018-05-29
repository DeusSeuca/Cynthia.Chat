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

        public static void PostJson<T>(T data)
        {
            JsonConvert.SerializeObject(data);
            using (var client = new HttpClient())
            using(var content )
        }
    }
}