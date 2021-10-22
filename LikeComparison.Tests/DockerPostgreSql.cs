using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LikeComparison.Tests
{
    /*
        // docker run -e POSTGRES_PASSWORD=StrongP@ssw0rd! -p 5432:5432 --name postgres -d postgres
    */
    public static class DockerPostgreSql
    {
        const string _containerName = "postgres";
        const string _password = "StrongP@ssw0rd!";

        public static string ConnectionString
        {
            get
            {
                return $"User ID=postgres;Password={_password};Host=localhost;Port=5432;";
            }
        }

        public static void InitContainer(TestContext context)
        {
            DockerPostgreSql.RunContainer(_containerName, _password).Wait();
            Thread.Sleep(2000);
        }

        private static async Task RunContainer(string containerName, string password)
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
                        Image = "postgres",
                        Env = new string[]{ "POSTGRES_PASSWORD=" + password },
                        HostConfig = new HostConfig
                        {
                            PortBindings = new Dictionary<string, IList<PortBinding>>
                            {
                                {
                                    "5432/tcp",
                                    new PortBinding[]
                                    {
                                        new PortBinding
                                        {
                                            HostPort = "5432"
                                        }
                                    }
                                }
                            }
                        }
                    });

                await client.Containers.StartContainerAsync(response.ID, new ContainerStartParameters());
            }
        }

        public static async Task StartSqlServerDocker(string containerName, string password)
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