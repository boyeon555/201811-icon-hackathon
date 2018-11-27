using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Globalization;
using System.Numerics;
using Newtonsoft.Json;

namespace IconSDK.RPC
{
    using Types;
    using Transaction;

    public class SendTransactionRequestMessage : RPCRequestMessage<IDictionary<string, object>>
    {
        public SendTransactionRequestMessage(IDictionary<string, object> param)
        : base("icx_sendTransaction", param)
        {

        }
    }

    public class SendTransactionResponseMessage : RPCResponseMessage<string>
    {

    }

    public class SendTransaction : RPC<SendTransactionRequestMessage, SendTransactionResponseMessage>
    {
        public SendTransaction(string url) : base(url)
        {

        }

        public async Task<Hash32> Invoke(Transaction tx)
        {
            var ts = new TransactionSerializer();
            var request = new SendTransactionRequestMessage(ts.Serialize(tx));
            var response = await Invoke(request);
            if (response.IsSuccess)
                return new Hash32(response.result.Replace("0x", ""));

            throw new Exception(response.Error.Message);
        }
    }
}
