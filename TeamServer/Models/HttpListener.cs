

namespace TeamServer.Models
{
    public class HttpListener : Listener
    {
        public override string Name { get;  }

        public int BindPort { get; } 

        public HttpListener(string name, int bindPort) 
        {
            Name = name;
            BindPort = bindPort;
        }

        public override Task Start()
        {
            var hostBuilder = new HostBuilder()
                 .ConfigureWebHostDefaults(host =>
                 {
                     host.UseUrls($"http://0.0.0.0:{BindPort}");
                     host.Configure(ConfigureApp);
                     host.ConfigureServices(ConfigureServices);
                 });
        }


        //9:45 
        private void ConfigureServices(IServiceCollection collection)
        {
            throw new NotImplementedException();
        }

        private void ConfigureApp(IApplicationBuilder builder)
        {
            throw new NotImplementedException();
        }

        public override void Stop()
        {
            throw new NotImplementedException();
        }

    }
}
