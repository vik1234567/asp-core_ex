using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        // https://www.stevejgordon.co.uk/httpclient-connection-pooling-in-dotnet-core
        // netstat -ano | findstr 142.251.39.68

        static async Task Main(string[] args)
        {
            {
                /*
                PooledConnectionLifetime , определяет, как долго соединения остаются активными при объединении в пул. 
                                    По истечении этого срока жизни соединение больше не будет объединяться или выдаваться для будущих запросов.

                PooledConnectionIdleTimeout определяет, как долго неактивные соединения остаются в пуле, пока не используются. 
                                    По истечении этого срока жизни бездействующее соединение будет очищено и удалено из пула.

                MaxConnectionsPerServer - определяет максимальное количество исходящих подключений, которые будут установлены для каждой конечной точки. 
                                        Подключения для каждой конечной точки объединяются отдельно. 

                -----
                PooledConnectionLifetime бесконечно, поэтому при регулярном использовании для запросов соединения могут оставаться открытыми бесконечно. 
                            В PooledConnectionIdleTimeout по умолчанию 2 минуты, с соединениями очищаемое, если они сидят неиспользованными 
                            в пуле дольше этого периода времени. 
                MaxConnectionsPerServer по умолчанию имеет значение int.MaxValue, и поэтому соединения практически не ограничены.
                */

            }
            var ips = await Dns.GetHostAddressesAsync("www.google.com");

            var fip = "";

            foreach (var ipAddress in ips)
            {
                fip = ipAddress.MapToIPv4().ToString();
                Console.WriteLine(ipAddress.MapToIPv4().ToString());
            }

            Console.WriteLine($"netstat -ano | findstr {fip} | wc -l");

            await NewMethod2();

            Console.WriteLine("Press a key to exit...");
            Console.ReadKey();
        }

        private static async Task NewMethod()
        {
            var client = new HttpClient();
            for (var i = 0; i < 20; i++)
            {
                _ = await client.GetAsync("https://www.google.com");
                //await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }

        private static async Task NewMethod1()
        {
            //При краткосрочном пуде - много соединений
            var socketsHandler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromSeconds(1),
                PooledConnectionIdleTimeout = TimeSpan.FromSeconds(1),
                MaxConnectionsPerServer = 3
            };

            var client = new HttpClient(socketsHandler);

            for (var i = 0; i < 5; i++)
            {
                _ = await client.GetAsync("https://www.google.com");
                await Task.Delay(TimeSpan.FromSeconds(2));
            }
        }
        private static async Task NewMethod2()
        {
            // максимальное кол-во соединений
            var socketsHandler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromSeconds(60),
                PooledConnectionIdleTimeout = TimeSpan.FromMinutes(4),
                MaxConnectionsPerServer = 2
            };

            var client = new HttpClient(socketsHandler);

            var sw = Stopwatch.StartNew();

            var tasks = Enumerable.Range(0, 200).Select(i => client.GetAsync("https://www.google.com"));

            await Task.WhenAll(tasks);

            sw.Stop();

            Console.WriteLine($"{sw.ElapsedMilliseconds}ms taken for 200 requests");
        }
        private static async Task NewMethod3()
        {
            var socketsHandler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromSeconds(60),
                PooledConnectionIdleTimeout = TimeSpan.FromMinutes(3),
                MaxConnectionsPerServer = 5
            };

            var client = new HttpClient(socketsHandler);

            //var sw = Stopwatch.StartNew();

            for (var i = 0; i < 7; i++)
            {
                _ = await client.GetAsync("https://www.google.com");
                await Task.Delay(TimeSpan.FromSeconds(2));
            }

            //sw.Stop();
            //Console.WriteLine($"{sw.ElapsedMilliseconds}ms taken for 200 requests");
        }
    }
}
