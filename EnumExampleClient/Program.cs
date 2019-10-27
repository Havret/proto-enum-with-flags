using System;
using EnumExample;
using Grpc.Core;

namespace EnumExampleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Channel channel = new Channel("127.0.0.1:50051", ChannelCredentials.Insecure);

            var client = new DeploymentCalendar.DeploymentCalendarClient(channel);

            var reply = client.GetDeploymentDays(new DeploymentDaysRequest() { NumberOfDeployments = 3 });
            var deploymentDays = (Weekend) reply.DeploymentDays;

            Console.WriteLine("Deployment will be in:");
            if ((deploymentDays & Weekend.Friday) == Weekend.Friday || (deploymentDays & Weekend.Saturday) == Weekend.Saturday || (deploymentDays & Weekend.Sunday) == Weekend.Sunday)
            {
                Console.WriteLine("There will be deployment during the weekend. :(");                
            }
            else
            {
                Console.WriteLine("There won't be any deployment over the weekend.");
            }

            channel.ShutdownAsync().Wait();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}