using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace FxProductMonitor.BLL
{
    public class GetProductDetail
    {
        public FxProductMonitor.Model.ProductList model { get; set; }

        public void GetCustFiled()
        {
            FxProductMonitor.BLL.ProductList productDetailBll = new FxProductMonitor.BLL.ProductList();
            string url = "http://fx.henghengw.net/api/detail.jsp?custId=233286&apikey=8411B4D5B6A600FCB6FBD217E3DD2E21";
            string tempUrl = url + "&productNo=" + model.productNo.ToString();
            WebRequest request = WebRequest.Create(tempUrl);
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {

                    string productData = sr.ReadToEnd();
                    if (productData.IndexOf("<custField>") < 0)
                    {
                        return;
                    }
                    string custFiled = productData.Substring(productData.IndexOf("<custField>") + 11);
                    custFiled = custFiled.Substring(0, custFiled.IndexOf("</custField>"));

                    custFiled = custFiled.Replace("<![CDATA[", "");
                    custFiled = custFiled.Replace("]]>", "");
                    model.custFiled = custFiled;
                    productDetailBll.Update(model);
                    sr.Close();
                }
                response.Close();
            }
        }
    }
}
