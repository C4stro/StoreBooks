using Microsoft.Extensions.Configuration;
using storeBooks.repository.entities;
using storeBooks.repository.interfaces;
using System;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Collections.Generic;

namespace storeBooks.repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ExchangeApiConfs _exchangeApiConfs = new ExchangeApiConfs();

        public Repository()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            _exchangeApiConfs.UrlBase = config.GetSection("ExchangeApiConfs:UrlBase").Value;
            _exchangeApiConfs.ExchagenToken = config.GetSection("ExchangeApiConfs:ExchageToken").Value;

        }

        public ExchangeValues ExchangeLatest()
        {
            try
            {
                var requisition = (HttpWebRequest)WebRequest.Create($"{_exchangeApiConfs.UrlBase}/latest?access_key={_exchangeApiConfs.ExchagenToken}");
                requisition.ContentType = "application/json";
                requisition.Method = "GET";
                
                return RequisitionHttp(requisition);
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception: {ex.Message}");
            }
        }

        private ExchangeValues RequisitionHttp(HttpWebRequest requisition)
        {
            var respostaHttp = (HttpWebResponse)requisition.GetResponse();

            using var streamReader = new StreamReader(respostaHttp.GetResponseStream());
            var jsonReturn = streamReader.ReadToEnd();

            return JsonConvert.DeserializeObject<ExchangeValues>(jsonReturn);
        }
    }
}
