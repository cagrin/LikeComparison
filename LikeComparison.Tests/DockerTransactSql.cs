using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LikeComparison.Tests
{
    /*
        // docker run -e 'ACCEPT_EULA=1' -e 'MSSQL_SA_PASSWORD=StrongP@ssw0rd!' -p 1433:1433 --name azuresqledge -d mcr.microsoft.com/azure-sql-edge
    */
    public static class DockerTransactSql
    {
        const string _containerName = "azuresqledge";
        const string _password = "StrongP@ssw0rd!";

        public static string ConnectionString
        {
            get
            {
                return $"Data Source=localhost,1433;Initial Catalog=master;User Id=sa;Password={_password}";
            }
        }

        public static void InitContainer(TestContext context)
        {
            DockerTransactSql.RunContainer(_containerName, _password).Wait();
            Thread.Sleep(2000);
        }

        public static async Task RunContainer(string containerName, string password)
        {
            using var configuration = new DockerClientConfiguration();
            using var client = configuration.CreateClient();

            var containers = await client.Containers.ListContainersAsync(new ContainersListParameters() { All = true });
            var container = containers.FirstOrDefault(c => c.Names.Contains("/" + containerName));
            if (container == null)
            {
                var response = await client.Containers.CreateContainerAsync(
                    new CreateContainerParameters
                    {
                        Name = containerName,
                        Image = "mcr.microsoft.com/azure-sql-edge",
                        Env = new string[]{ "ACCEPT_EULA=y", "MSSQL_SA_PASSWORD=" + password },
                        HostConfig = new HostConfig
                        {
                            PortBindings = new Dictionary<string, IList<PortBinding>>
                            {
                                {
                                    "1433/tcp",
                                    new PortBinding[]
                                    {
                                        new PortBinding
                                        {
                                            HostPort = "1433"
                                        }
                                    }
                                }
                            }
                        }
                    });

                await client.Containers.StartContainerAsync(response.ID, new ContainerStartParameters());
            }
        }
    }
}