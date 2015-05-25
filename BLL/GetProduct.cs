using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Xml.Serialization;
using System.Threading;

namespace FxProductMonitor.BLL
{
    public class GetProduct
    {
        public int CurrentPage { get; set; }

        public void Get()
        {
            var url = "http://fx.henghengw.net/api/list_hh.jsp";
            url += "?custId=234200&apikey=05908E8BF370E0BFB58F36F15774E499";
            url += "&treeId=0&pageNum=100&pageNo=";
            url += CurrentPage.ToString();
            var request = WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();
            var dataStream = response.GetResponseStream();
            var sr = new StreamReader(dataStream);
            var data = sr.ReadToEnd();

            //FxProductMonitor.BLL.EasyApi easy = new EasyApi();
            //string easyResult = easy.Perform("http://fx.henghengw.net/api/list_hh.jsp", "JSESSIONID=e1tFW2opYihf; dc4e01dbca1cd374ffb9068b31380fc2=2csFVZj9GauFmbsVTPmEHb0l2XklSPjZXd0N2XklTPzITMzcyNpZ1c39GaslTZw0mJ1N3cfRHdwlTZz0mJfd3YzVFdp9DZy0zM3EzMmcXdlNlcu9WYl1ePfe+suWut6WOh6SOq6SOq9eek7eOnneekKaOgcaeiZmOkFWOrPWCupZ1cn9mc19Dcw0mJzl2XpR3c9ASMkZlYs9War5XPhNXYwMCOyZ2blx2XklTPmMXdlNlcp9DZi13bldjb1ATMmM", "fx.henghengw.net", url.Replace("http://fx.henghengw.net", ""), "http://fx.henghengw.net");
            //var data = easyResult;
            data = data.Substring(data.IndexOf("</status><products>", StringComparison.Ordinal) + "</status><products>".Length);
            data = data.Replace("<product>", "<ProductList>");
            data = data.Replace("</product>", "</ProductList>");
            data = data.Replace("</products></result>", "</ArrayOfProductList>");
            data = "<?xml version=\"1.0\"?><ArrayOfProductList xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">" + data;

            while (data.IndexOf("<orderDesc><![CDATA") >= 0)
            {
                var repStr = data.Substring(data.IndexOf("<orderDesc><![CDATA") + "<orderDesc>".Length, data.IndexOf("]]></orderDesc>") - data.IndexOf("<orderDesc><![CDATA") - "<orderDesc>".Length + 3);
                data = data.Replace(repStr, Convert.ToBase64String(Encoding.UTF8.GetBytes(repStr)));
            }
            data = data.Replace("<![CDATA[]]>", "<![CDATA[2014-01-01]]>");
            var stringReader = new StringReader(data);
            var list = new List<Model.ProductList>();
            var ProductBll = new ProductList();
            var xmldes = new XmlSerializer(typeof(List<Model.ProductList>));
            list = xmldes.Deserialize(stringReader) as List<Model.ProductList>;
            sr.Close();
            response.Close();
            dataStream.Close();
            var bll = new ProductList();
            foreach (var model in list)
            {
                if (model.priceEndDate == null) continue;
                if (model.img.IndexOf("u.liuliuka.com") >= 0) continue;
                var recordCount = bll.GetRecordCount("productNo=" + model.productNo.ToString());
                if (model.interfaceId == "2014-01-01")
                    model.interfaceId = string.Empty;
                if (model.interfaceProdId == "2014-01-01")
                    model.interfaceProdId = string.Empty;

                if (model.interfaceId == "960" || string.IsNullOrEmpty(model.interfaceId))
                    continue;
                if (recordCount > 0)
                {
                    model.id = bll.GetProductId(Convert.ToInt32(model.productNo));
                    model.ProductState = 1;
                    model.updateDate = DateTime.Now;
                    bll.Update(model); continue;
                }

                if (Convert.ToDateTime(model.priceEndDate).ToString("yyyy-MM-dd") != "2014-01-01")
                {
                    model.ProductState = 1;
                    model.updateDate = DateTime.Now;
                    ProductBll.Add(model);
                }
            }

        }
    }
}
