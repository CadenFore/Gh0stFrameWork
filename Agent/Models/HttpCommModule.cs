using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;

namespace Agent.Models
{
    public class HttpCommModule : CommModule
    {

        public string ConnectAddress { get; set; }
        public int ConnectPort { get; set; }

        private CancellationTokenSource _tokenSource;
        private HttpClient _client;


        public HttpCommModule(string connectAddress, int connectPort)
        {
            ConnectAddress = connectAddress;
            ConnectPort = connectPort;
        }

        public override void Init(AgentMetadata metadata)
        {
            base.Init(metadata);

            _client = new HttpClient();
            _client.BaseAddress = new System.Uri($"{ConnectAddress}:{ConnectPort}");
            _client.DefaultRequestHeaders.Clear();

            var encodedMetadata = Convert.ToBase64String(AgentMetadata.Serialise());

            _client.DefaultRequestHeaders.Add("authorization", $"Bearer {encodedMetadata}");
        }

        public override Task Start()
        {
            _tokenSource = new CancellationTokenSource();

            while (!_tokenSource.IsCancellationRequested)
            {
                //Check in
                //Get tasks
                //Sleep
            }
        }

        private void CheckIn()
        {
            // 27:30
        }


        public override void Stop()
        {
            _tokenSource.Cancel();
        }
    }
}
