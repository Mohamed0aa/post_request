using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace BezaatSignup
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/129.0.0.0 Safari/537.36");
            client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
            client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9,ar;q=0.8");
            client.DefaultRequestHeaders.Add("Referer", "https://www.bezaat.com/egypt/cairo/user/create");
            client.DefaultRequestHeaders.Add("Origin", "https://www.bezaat.com");
            client.DefaultRequestHeaders.Add("Connection", "keep-alive");
            client.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "document");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "navigate");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");
            client.DefaultRequestHeaders.Add("Sec-Fetch-User", "?1");
            client.DefaultRequestHeaders.Add("Priority", "u=0, i");
            // Get initial cookies
            var response = await client.GetAsync("https://www.bezaat.com/egypt/cairo");
            //var cookies = response.Headers.GetValues("Set-Cookie");

            // Get the signup page to extract the CSRF token
            response = await client.GetAsync("https://www.bezaat.com/egypt/cairo/user/create");
            var responseString = await response.Content.ReadAsStringAsync();
            var doc = new HtmlDocument();
            doc.LoadHtml(responseString);
            var tokenNode = doc.DocumentNode.SelectSingleNode("//input[@name='_token']");
            string _token = tokenNode?.GetAttributeValue("value", "");

            Console.WriteLine(_token);
            // Prepare data for POST request

            var data = new Dictionary<string, string>
            {
                { "_token", _token },
                { "firstname", "mohamed" },
                { "lastname", "ali" },
                { "password", "555555" },
                { "password_confirmation", "555555" },
                { "submit", "" },
                { "email", "evtrgbhyjkmuilounbvuil@gmail.com" }
            };
            var content = new FormUrlEncodedContent(data);
            var postResponse = await client.PostAsync("https://www.bezaat.com/egypt/cairo/user/signup", content);
            string finalResponse = await postResponse.Content.ReadAsStringAsync();

            Console.WriteLine(finalResponse);
            // File.Create("C:\\Users\\Kimo Store\\Desktop\\test.html");
            File.AppendAllText("C:\\Users\\Kimo Store\\Desktop\\test.html", finalResponse);
        }
    }
}
