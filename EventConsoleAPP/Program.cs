using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using EventWebService.Models;

namespace EventConsoleAPP
{
    class Program
    {
        static void Main(string[] args)
        {
            const string ServerUrl = "http://localhost:58420";
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var response = client.GetAsync("api/Events").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var EventData = response.Content.ReadAsAsync<IEnumerable<Event>>().Result;
                        foreach (var Event in EventData)
                        {
                            Console.WriteLine(Event);
                        }

                    }

                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
        }
    }
}
