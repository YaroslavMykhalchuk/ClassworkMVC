using DemoClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Threading;

namespace WebClientDemo
{
    internal class Program
    {
        static Uri _host = new Uri("http://localhost:5246");
        static IEnumerable<Organisation> GetOrganisations(int skip = 0, int take = 50)
        {
            var url = new Uri(_host, $"api/organisations?take={take}&skip={skip}");
            var webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            webClient.Headers.Add("Accept", "*/*");
            try
            {
                string s = webClient.DownloadString(url);
                var result = JsonConvert.DeserializeObject<QueryResult<Organisation>>(s);
                return result.Items;
            }
            catch (Exception)
            {

                throw new Exception();
            }
        }
        static Organisation PostOrganisation(Organisation organisation)
        {
            var url = new Uri(_host, $"api/organisations");
            var webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            webClient.Headers.Add("accept", "*/*");
            webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");

            try
            {
                string data = JsonConvert.SerializeObject(organisation);
                string s = webClient.UploadString(url, data);
                var result = JsonConvert.DeserializeObject<Organisation>(s);
                return result;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public static void DeleteOranisation(long id)
        {
            var url = new Uri(_host, $"api/organisations/{id}");
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "DELETE";
            request.Accept = "*/*";
            try
            {
                var response = request.GetResponse();
            }
            catch (Exception ex)
            {

                throw new Exception($"Error :{ex.ToString()}");
            }
        }
        static void Main(string[] args)
        {
            Thread.Sleep(6000);
            var postResult = PostOrganisation(new Organisation { Name = "WebClient Org", FullName = "WebClient FullName" });
            var r = GetOrganisations();
            Console.WriteLine(r.Count());
            Console.WriteLine(GetGoogleHomePage());
            Console.ReadKey();
        }

        static string GetGoogleHomePage()
        {
            var url = new Uri("http://www.google.com");
            var webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            webClient.Headers.Add("Accept", "*/*");

            try
            {
                string html = webClient.DownloadString(url);
                return html;
            }
            catch (Exception)
            {
                throw new Exception("Помилка при завантаженні сторінки Google.");
            }
        }
    }
}
