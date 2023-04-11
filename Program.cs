/*
MIT License
Copyright (c) 2023 Monzu77
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

                Panel? panel = new Panel($"[green]Online: {server?.online}[/]\n[green]IP: {server?.ip}[/]\n[green]Port: {server?.port}[/]\n[wheat1]Version: {server?.version}[/]\n[wheat1]Gamemode: {server?.gamemode}[/]\n[wheat1]Map: {server?.map}[/]\n[wheat1]Software: {server?.software}[/]\n[wheat1]Protocol: {server?.protocol}[/]\n[wheat1]Hostname: {server?.hostname}[/]\n\n[deeppink2]  Debug  [/]\n[red]    Ping: {server?.debug?.ping}[/]\n[red]    Query: {server?.debug?.query}[/]\n[red]    Srv: {server?.debug?.srv}[/]\n[red]    QueryMismatch: {server?.debug?.querymismatch}[/]\n[red]    IPinSrv: {server?.debug?.ipinsrv}[/]\n[red]    CNameinSrv: {server?.debug?.cnameinsrv}[/]\n[red]    AnimatedMotd: {server?.debug?.animatedmotd}[/]\n[red]    CacheTime: {server?.debug?.cachetime}[/]\n[red]    CacheExpire: {server?.debug?.cacheexpire}[/]\n[red]    ApiVersion: {server?.debug?.apiversion}[/]");
                panel.RoundedBorder();

                AnsiConsole.Write(panel);
                Console.Read();
            }   
        }
    }
}
