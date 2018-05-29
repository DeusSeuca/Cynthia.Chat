namespace Cynthia.Chat.Client
{
    public class DataProcessing
    {
        public string Url { get; set; } = "http://localhost:5000/data/";
        public async void SendData(string name, string context)
        {
            await Json.GetJson<JsonData>(Url + JsonConvert.SerializeObject(new JsonData(name, context)));
        }
        public async Task<IEnumerable<JsonData>> GetData(int count)
        {
            return await Task<IEnumerable<JsonData>>.Run(() => Json.GetJson<IEnumerable<JsonData>>(Url + "getdata/" + count));
        }
    }
}