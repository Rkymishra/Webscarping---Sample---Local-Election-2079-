// See https://aka.ms/new-console-template for more information
using HtmlAgilityPack;
using MayorElectionWebScraping_Kathmandu_Kathmandu;
using System.Net;
using System.Text;

try
{
    string electionUrl = "https://election.ekantipur.com/pradesh-3/district-kathmandu/kathmandu?lng=eng";
    string htmlFileLocation = @"D:\Private\.NET Projects\MayorElectionWebScraping_Kathmandu_Kathmandu\Htmls\index.html";
    string csvFileLocation = @"D:\Private\.NET Projects\MayorElectionWebScraping_Kathmandu_Kathmandu\Htmls\nameVoteList.csv";
    HttpClient client = new HttpClient();
    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
    client.DefaultRequestHeaders.Accept.Clear();
    string response = await client.GetStringAsync(electionUrl);
    using (var stream = File.Open(htmlFileLocation, FileMode.OpenOrCreate))
    {
        stream.Write(Encoding.UTF8.GetBytes(response));
    }
    var nameVoteList = Parser.ParseHtml(response);
    using (var stream = File.Open(csvFileLocation, FileMode.OpenOrCreate))
    {
        foreach (var item in nameVoteList)
        {
            stream.Write(Encoding.UTF8.GetBytes($"\n{item.Item1}, {item.Item2}"));
        }
    }
    Console.WriteLine($"{nameVoteList.Count()} data found!");
}
catch (Exception ex)
{
    throw;
}