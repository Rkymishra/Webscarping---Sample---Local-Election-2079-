using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MayorElectionWebScraping_Kathmandu_Kathmandu
{
    public static class Parser
    {
        public static List<Tuple<string, string>> ParseHtml(string html)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            List<HtmlNode>? candidateNameNodes = htmlDoc.DocumentNode.Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "candidate-name").Contains("candidate-name")).ToList();

            List<HtmlNode>? candidateVoteCountNodes = htmlDoc.DocumentNode.Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "vote-numbers").Contains("vote-numbers")).ToList();

            List<Tuple<string, string>> nameVoteList = new List<Tuple<string, string>>();
            var nameAndVotes = candidateNameNodes.Zip(candidateVoteCountNodes, (n, v) => new { Name = n.InnerHtml.Trim(), Votes = v.InnerHtml.Trim() });

            foreach (var item in nameAndVotes)
            {
                if (!string.IsNullOrEmpty(item.Name) && !item.Name.Contains("<"))
                {
                    string voteCount = string.IsNullOrEmpty(item.Votes) ? "Not Counted" : item.Votes;
                    nameVoteList.Add(new Tuple<string, string>(item.Name, voteCount));
                }
            }
            return nameVoteList;
        }
    }
}
