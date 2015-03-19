using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Globalization;

namespace FxProductMonitor.BLL
{
    public class GetProductInfo
    {
        public int ProdId { get; set; }
        public int TongchengProdId { get; set; }
        public void Get()
        {
            if (ProdId <= 0) return;
            var cc = new Cookie("dc4e01dbca1cd374ffb9068b31380fc2", "2csFVZj9GauFmbsVTPmEHb0l2XklSPjZXd0N2XklTPzITMzcyNpZ1c39GaslTZw0mJ1N3cfRHdwlTZz0mJfd3YzVFdp9DZy0zM3EzMmcXdlNlcu9WYl1ePfe+suWut6WOh6SOq6SOq9eek7eOnneekKaOgcaeiZmOkFWOrPWCupZ1cn9mc19Dcw0mJzl2XpR3c9ASMkZlYs9War5XPhNXYwMCOyZ2blx2XklTPmMXdlNlcp9DZi13bldjb1ATMmM");
            var cookieContainer = new CookieContainer();
            cc.Domain = "fx.henghengw.net";
            cookieContainer.Add(cc);
            cc = new Cookie("JSESSIONID", "fvxGTQQKWZrg");
            cc.Domain = "fx.henghengw.net";
            cookieContainer.Add(cc);
            var url = "http://fx.henghengw.net/prod/list_ajax.jsp?action=setticketrole&currency_type=0&year=" + DateTime.Now.Year + "&month=" + DateTime.Now.Month + "&info_id=" + ProdId.ToString();
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.CookieContainer = cookieContainer;
            request.ContentType = "application/x-www-form-urlencoded";
            request.KeepAlive = false;
            var response = (HttpWebResponse)request.GetResponse();
            var log_bll = new logs();
            using (var dataStream = response.GetResponseStream())
            {
                using (var sr = new System.IO.StreamReader(dataStream))
                {
                    var data = sr.ReadToEnd();
                    string week = "";
                    var logsModel = new Model.logs();
                    var html = data;
                    string startDate = html.Substring(html.IndexOf("start_date", System.StringComparison.Ordinal) + 19);
                    startDate = startDate.Substring(0, startDate.IndexOf("\"", System.StringComparison.Ordinal));
                    string endDate = html.Substring(html.IndexOf("end_date", System.StringComparison.Ordinal) + 17);
                    endDate = endDate.Substring(0, endDate.IndexOf("\"", System.StringComparison.Ordinal));
                    string conferPriceRmb = html.Substring(html.IndexOf("confer_price_rmb", System.StringComparison.Ordinal) + 25);
                    conferPriceRmb = conferPriceRmb.Substring(0, conferPriceRmb.IndexOf("\"", System.StringComparison.Ordinal));
                    string priceRmb = html.Substring(html.IndexOf("\"price_rmb", System.StringComparison.Ordinal) + 19);
                    priceRmb = priceRmb.Substring(0, priceRmb.IndexOf("\"", System.StringComparison.Ordinal));

                    string startNum = html.Substring(html.IndexOf("start_num", System.StringComparison.Ordinal) + 18);
                    startNum = startNum.Substring(0, startNum.IndexOf("\"", System.StringComparison.Ordinal));

                    string startDay = html.Substring(html.IndexOf("start_day", System.StringComparison.Ordinal) + 18);
                    startDay = startDay.Substring(0, startDay.IndexOf("\"", System.StringComparison.Ordinal));

                    FxProductMonitor.Model.ProductList model = new Model.ProductList();
                    FxProductMonitor.BLL.ProductList bll = new ProductList();
                    model = bll.GetModel(bll.GetProductId(ProdId));
                    if (model != null)
                    {
                        model.priceEndDate = Convert.ToDateTime(endDate);
                        model.priceStartDate = Convert.ToDateTime(startDate);
                        model.SettlementPrice = priceRmb.Trim();
                        model.StartNum = Convert.ToInt32(startDay);
                        model.updateDate = DateTime.Now;
                        bll.Update(model);
                    }

                    sr.Close();
                }
                dataStream.Close();
            }
            response.Close();

        }

    }
}
