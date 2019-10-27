using System;
using System.Threading.Tasks;
using EnumExample;
using EnumExampleClient;
using Grpc.Core;

namespace EnumExampleServer
{
    class GreeterImpl : DeploymentCalendar.DeploymentCalendarBase
    {
        public override Task<DeploymentDaysReply> GetDeploymentDays(DeploymentDaysRequest request, ServerCallContext context)
        {
            Weekend deploymentDays = Weekend.Friday | Weekend.Saturday | Weekend.Sunday;
            return Task.FromResult(new DeploymentDaysReply
            {
                DeploymentDays = (int) deploymentDays
            });
        }
    }

    class Program
    {
        const int Port = 50051;

        public static void Main(string[] args)
        {
            Server server = new Server
            {
                Services = { DeploymentCalendar.BindService(new GreeterImpl()) },
                Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
            };
            server.Start();

            Console.WriteLine("Deployment calendar server listening on port " + Port);
            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();

            server.ShutdownAsync().Wait();
        }
    }
}