using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;

namespace IconSDK.RPC
{
    public class RPC<TRPCRequestMessage, TRPCResponseMessage>
        where TRPCRequestMessage : RPCRequestMessage
        where TRPCResponseMessage : RPCResponseMessage
    {
        public readonly string URL;

        public RPC(string url)
        {
            URL = url;
        }

        public async Task<TRPCResponseMessage> Invoke(TRPCRequestMessage requestMessage)
        {
            using (var httpClient = new HttpClient())
            {
                string message = JsonConvert.SerializeObject(requestMessage);
                Debug.Log($"Message {message}");
                using (var result = await httpClient.PostAsync(
                    URL,
                    new StringContent(
                        message,
                        Encoding.UTF8,
                        "application/json"
                    )
                ))
                {
                    string resultContent = await result.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<TRPCResponseMessage>(resultContent);
                }
            }
        }
    }
}