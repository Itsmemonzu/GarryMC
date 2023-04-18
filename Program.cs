/*
MIT License
Copyright (c) 2023 Monzu77
Copyright (c) 2023 HitBlast
Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System.Text.Json;
using Spectre.Console;
using Fclp;

namespace GrassMC
{
    public class Server
    {
        public bool online { get; set; }
        public string? ip { get; set; }
        public long? port { get; set; }
        public string? version { get; set ;}
        public string? hostname { get; set;}
        public string? gamemode { get; set;}
        public long? protocol { get; set; }
        public string? software { get; set; }
        public string? map { get; set; }
        public Debug? debug { get; set; }
        public Players? players { get; set; }

    }
    public class Debug
    {
        public bool? ping { get; set; }
        public bool? query { get; set; }
        public bool? srv { get; set; }
        public bool? querymismatch { get; set; }
        public bool? ipinsrv { get; set; }
        public bool? cnameinsrv { get; set; }
        public bool? animatedmotd { get; set; }
        public long? cachetime { get; set; }
        public long? cacheexpire { get; set; }
        public int? apiversion { get; set; }
    }
    public class Players
    {
        public long? online { get; set; }
        public long? max { get; set; } 
    }

    public class Data
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<Server?> getData(string? InputIP, bool Bedrock) {
            string url;

            if (Bedrock)
            {
                url = "https://api.mcsrvstat.us/bedrock/2/"+InputIP;
            }
            else {
                url = "https://api.mcsrvstat.us/2/"+InputIP;
            }

            var response = await client.GetStringAsync(url);
            var server = JsonSerializer.Deserialize<Server>(response);

            return server;
        }
    }

    public class ApplicationArguments
    {
        public string? Address { get; set; }
        public bool Bedrock { get; set; }
    }


    public static class MainRenderer

    {
        public static async Task Main(string[] args) 
        {
            var p = new FluentCommandLineParser<ApplicationArguments>();

            p.Setup(arg => arg.Address)
                .As('a', "address")
                .Required();

            p.Setup(arg => arg.Bedrock)
                .As('b', "bedrock")
                .SetDefault(false);

            var result = p.Parse(args);

            if (result.HasErrors == false)
            {
                Data? data = new Data();
                Server? server = await data.getData(InputIP: p.Object.Address, Bedrock: p.Object.Bedrock);

                Panel? panel = new Panel($"Online: [green]{server?.online}[/]\nIP: {server?.ip}\nPort: {server?.port}");
                panel.RoundedBorder();
                panel.Padding = new Padding(2, 1, 15, 1);

                string bools = $"\n   Ping: [red]{server?.debug?.ping}[/]\n   Query: [red]{server?.debug?.query}[/]\n   Srv: [red]{server?.debug?.srv}[/]\n   QueryMismatch: [red]{server?.debug?.querymismatch}[/]\n   IPinSrv: [red]{server?.debug?.ipinsrv}[/]\n   CNameinSrv: [red]{server?.debug?.cnameinsrv}[/]\n   AnimatedMotd: [red]{server?.debug?.animatedmotd}[/]";
                
                AnsiConsole.Write(panel);
                AnsiConsole.Markup($"   Version: {server?.version}");
                Console.WriteLine("");

                if(server?.gamemode != null)
                {
                    AnsiConsole.Markup($"   Gamemode: {server?.gamemode}");
                }
                if(server?.map != null)
                {
                    AnsiConsole.Markup($"   Map: {server?.map}");
                }
                if(server?.software != null)
                {
                    AnsiConsole.Markup($"   Software: {server?.software}");
                }
                AnsiConsole.Markup($"   Protocol: {server?.protocol}\n\n   Players online: {server?.players.online} / {server?.players.max}\n\n   Cache Time: [paleturquoise1]{server?.debug?.cachetime}[/]\n   Cache Expire: [paleturquoise1]{server?.debug?.cacheexpire}[/]\n   API Version: [paleturquoise1]{server?.debug?.apiversion}[/]\n\n   Hostname: {server?.hostname}");
                AnsiConsole.Markup("\n" + bools);
                Console.Read();
            }   
        }
    }
}
