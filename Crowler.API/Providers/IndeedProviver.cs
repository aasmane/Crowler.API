using Crowler.API.Modele;
using Crowler.API.Modele.Indeed;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Crowler.API.Providers
{
    /// <summary>
    /// The Indeed Provider Class
    /// </summary>
    public class IndeedProvider : ICrowlingProvider
    {
        IConfiguration _configuration;

        public IndeedProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Launch Indeed Crowling
        /// </summary>
        /// <param name="request">the request</param>
        /// <returns>the Message represent result of crowling</returns>
        public async Task<CrowlingMessage> LaunchCrowling(IndeedCrowlingRequest request)
        {
            CrowlingMessage message = new CrowlingMessage() { OperationSucces = false };
            try
            {
                await StartCrowling(request);
                return message;
            }
            catch (Exception ex)
            {
                message.ErrorMessage = ex.StackTrace;
                message.OperationSucces = false;
                return message;
            }
        }

        private async Task StartCrowling(IndeedCrowlingRequest request)
        {
            //testGenerationEntities context = new testGenerationEntities();

            string host = _configuration["IndeedHost"].ToString();

            string url = string.Format(_configuration["IndeedSearchQuery"].ToString(), request.What, request.Where);

            HtmlWeb web = new HtmlWeb();
            try
            {
                while (!string.IsNullOrEmpty(url))
                {
                    HtmlAgilityPack.HtmlDocument htmlDocument = web.Load(host + url);

                    var jobList = htmlDocument?.DocumentNode.Descendants("div")?.Where(node => node.GetAttributeValue("data-tn-component", "").Equals("organicJob")).ToList();

                    HtmlAgilityPack.HtmlDocument detailHtmlDocument;
                    foreach (var item in jobList)
                    {
                        Jobs job = new Jobs();
                        job.Title = item.Descendants("h2")?.FirstOrDefault()?.InnerText;
                        var detailUrl = item.Descendants("h2")?.FirstOrDefault()?.Descendants("a")?.FirstOrDefault()?.GetAttributeValue("href", "");
                        detailHtmlDocument = web.Load(host + detailUrl);
                        job.Content = detailHtmlDocument.DocumentNode.Descendants("div")?.Where(x => x.GetAttributeValue("class", "").Contains("jobsearch-JobComponent icl-u-xs-mt"))?.FirstOrDefault()?.InnerText;
                        job.Company = item.Descendants("span")?.Where(node => node.GetAttributeValue("class", "").Equals("company"))?.FirstOrDefault()?.InnerText;
                        var date = item.Descendants("span")?.Where(node => node.GetAttributeValue("class", "").Equals("date"))?.FirstOrDefault()?.InnerText;
                        string number = Regex.Match(date, @"\d+").Value;
                        if (!string.IsNullOrEmpty(number))
                        {
                            job.Date = DateTime.Now.AddDays(int.Parse(number) * -1).ToShortDateString();
                        }

                        job.City = item.Descendants("span")?.Where(node => node.GetAttributeValue("class", "").Equals("location"))?.FirstOrDefault()?.InnerText;
                        //if (!string.IsNullOrEmpty(job.Title) && !context.Jobs.Any(x => x.Title == job.Title && x.Date == job.Date))
                        //{
                        //    context.Jobs.Add(job);
                        //    context.SaveChanges();
                        //}
                    }
                    var pagination = htmlDocument.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("class", "").Equals("pagination")).FirstOrDefault();

                    url = pagination.Descendants("a").LastOrDefault().GetAttributeValue("href", "");
                }
            }
            catch (Exception ex)
            {

            }
        }


    }
}
