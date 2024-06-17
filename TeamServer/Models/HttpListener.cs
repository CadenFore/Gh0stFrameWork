using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;




namespace TeamServer.Models
{
    // HttpListener class inherits from Listener class
    public class HttpListener : Listener
    {

        // Name property is overridden from the base class Listener and is read-only
        public override string Name { get; }


        // BindPort property is read-only and stores the port number on which the listener will bind
        public int BindPort { get; }

        // CancellationTokenSource to manage the cancellation of the running host
        private CancellationTokenSource _tokenSource;


        // Constructor for HttpListener that takes a name and a bind port as parameters
        public HttpListener(string name, int bindPort)
        {
            // Initialize the Name property with the provided name
            Name = name;

            // Initialize the BindPort property with the provided port number
            BindPort = bindPort;
        }

        // Override the Start method from the Listener base class
        public override async Task Start()
        {

            // Create a new HostBuilder for configuring and running the web host
            var hostBuilder = new HostBuilder()
                .ConfigureWebHostDefaults(host =>
                {

                    // Set the URL to bind to all network interfaces on the specified port
                    host.UseUrls($"http://0.0.0.0:{BindPort}");


                    // Configure the application using the ConfigureApp method
                    host.Configure(ConfigureApp);


                    // Configure services using the ConfigureServices method
                    host.ConfigureServices(ConfigureServices);
                });

            var host = hostBuilder.Build();

            _tokenSource = new CancellationTokenSource();
              host.RunAsync(_tokenSource.Token);
        }


        // Method to configure services for dependency injection
        private void ConfigureServices(IServiceCollection services)
        {

            // Add controller services to the dependency injection container
            services.AddControllers();

        }

        // Method to configure the application pipeline
        private void ConfigureApp(IApplicationBuilder app)
        {

            // Enable routing in the application
            app.UseRouting();


            // Define endpoint routing
            app.UseEndpoints(e =>
            {

                // Map the root URL to the HttpListener controller and HandleImplant action
                e.MapControllerRoute("/", "/", new { controller = "HttpListener", action = "HandleImplant" });

            });
        }

        // Override the Stop method from the Listener base class
        public override void Stop()
        {
            _tokenSource.Cancel();
        }
    }
}
