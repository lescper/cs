using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using SeasideResearch.LibCurlNet;
using System.Windows.Forms;
using System.Net;
using System.IO;


namespace FxProductMonitor.BLL
{
    public class GetHtml
    {
        public int prod_id { get; set; }
        public string html { get; set; }
        public string data { get; set; }
        public bool Completed { get; set; }
        public int ProductId { get; set; }

        public int GetFunc(byte[] buf, int size, int memb, object extraData)
        {
            var Msg = System.Text.Encoding.UTF8.GetString(buf);
            string interface_id = "", interface_prod_id = "";
            if (Msg.IndexOf("选择接口对象") >= 0 || !string.IsNullOrEmpty(html))
            {
                html += Msg;

            }
            try
            {
                if (!string.IsNullOrEmpty(html) && html.IndexOf("选择接口对象") >= 0 && html.IndexOf("请认真填写") >= 0)
                {
                    Msg = html;
                    var inter_id = Msg.Substring(Msg.IndexOf("选择接口对象"));
                    inter_id = inter_id.Substring(0, inter_id.IndexOf("请认真填写"));
                    var model = new Model.ProductList();
                    var bll = new ProductList();
                    model = bll.GetModel(ProductId);
                    if (inter_id.IndexOf("selected") >= 0)
                    {
                        var id = inter_id.Substring(0, inter_id.IndexOf("selected"));
                        id = id.Substring(id.LastIndexOf("value") + 7);
                        id = id.Substring(0, id.IndexOf("\""));

                        inter_id = inter_id.Substring(inter_id.IndexOf("selected"));
                        inter_id = inter_id.Substring(inter_id.IndexOf(">") + 1);
                        interface_id = id;
                        inter_id = inter_id.Substring(inter_id.IndexOf("interface_prod_id"));
                        inter_id = inter_id.Substring(inter_id.IndexOf("value") + 7);
                        interface_prod_id = inter_id.Substring(0, inter_id.IndexOf("\""));
                        if (model != null)
                        {
                            model.interfaceId = interface_id;
                            model.interfaceProdId = interface_prod_id;
                            model.updateDate = DateTime.Now;
                            model.ProductState = 1;

                            if (bll.Update(model))
                            {
                                Completed = true;
                            }
                        }
                        else
                        {

                        }

                    }
                    else
                    {
                        model.ProductState = 1;
                        model.updateDate = DateTime.Now;
                        if (bll.Update(model))
                        {
                            Completed = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var logs = new Model.logs();
                logs.log_text = "获取接口信息异常，" + ex.Message;
                logs.log_date = DateTime.Now;
                logs.related_product = "错误";
                var bll = new logs();
                bll.Add(logs);
            }
            return size * memb;
        }

        public void Html()
        {
            //are = (System.Threading.AutoResetEvent)o;
            html = "";
            if (prod_id <= 0) return;
            Curl.GlobalInit((int)CURLinitFlag.CURL_GLOBAL_ALL);
            var easy_opt = new Easy();
            easy_opt.SetOpt(CURLoption.CURLOPT_URL, "http://fx.henghengw.net/prod/ticket_edit.jsp?info_id=" + prod_id.ToString());
            easy_opt.SetOpt(CURLoption.CURLOPT_COOKIEFILE, "cookie.txt");
            easy_opt.SetOpt(CURLoption.CURLOPT_TIMEOUT, 5);
            easy_opt.SetOpt(CURLoption.CURLOPT_CONNECTTIMEOUT, 3);
            var wf = new Easy.WriteFunction(GetFunc);
            easy_opt.SetOpt(CURLoption.CURLOPT_WRITEFUNCTION, wf);
            easy_opt.Perform();
            easy_opt.Cleanup();
        }

        public void WebGetData()
        {
            try
            {
                var url = "http://fx.henghengw.net/prod/ticket_edit.jsp?info_id=" + prod_id.ToString();
                var request = (HttpWebRequest)WebRequest.Create(url);
                var cc = new Cookie("dc4e01dbca1cd374ffb9068b31380fc2", "2csFVZj9GauFmbsVTPmEHb0l2XklSPjZXd0N2XklTPzITMzcyNpZ1c39GaslTZw0mJ1N3cfRHdwlTZz0mJfd3YzVFdp9DZy0zM3EzMmcXdlNlcu9WYl1ePfe+suWut6WOh6SOq6SOq9eek7eOnneekKaOgcaeiZmOkFWOrPWCupZ1cn9mc19Dcw0mJzl2XpR3c9ASMkZlYs9War5XPhNXYwMCOyZ2blx2XklTPmMXdlNlcp9DZi13bldjb1ATMmM");
                var cookieContainer = new CookieContainer();
                cc.Domain = "fx.henghengw.net";
                cookieContainer.Add(cc);
                cc = new Cookie("JSESSIONID", "fvxGTQQKWZrg");
                cc.Domain = "fx.henghengw.net";
                cookieContainer.Add(cc);
                request.CookieContainer = cookieContainer;
                var response = (HttpWebResponse)request.GetResponse();
                var cookie = new Cookie();
                var sr = new StreamReader(response.GetResponseStream());
                var data = sr.ReadToEnd();
                response.Close();
                request.Abort();
                sr.Close();

                var Msg = data;
                string interface_id = "", interface_prod_id = "";
                var inter_id = Msg.Substring(Msg.IndexOf("选择接口对象"));
                inter_id = inter_id.Substring(0, inter_id.IndexOf("请认真填写"));
                var model = new Model.ProductList();
                var bll = new ProductList();
                model = bll.GetModel(ProductId);
                if (inter_id.IndexOf("selected") >= 0)
                {
                    var id = inter_id.Substring(0, inter_id.IndexOf("selected"));
                    id = id.Substring(id.LastIndexOf("value") + 7);
                    id = id.Substring(0, id.IndexOf("\""));

                    inter_id = inter_id.Substring(inter_id.IndexOf("selected"));
                    inter_id = inter_id.Substring(inter_id.IndexOf(">") + 1);
                    interface_id = id;
                    inter_id = inter_id.Substring(inter_id.IndexOf("interface_prod_id"));
                    inter_id = inter_id.Substring(inter_id.IndexOf("value") + 7);
                    interface_prod_id = inter_id.Substring(0, inter_id.IndexOf("\""));

                    if (model != null)
                    {
                        model.interfaceId = interface_id;
                        model.interfaceProdId = interface_prod_id;
                        model.updateDate = DateTime.Now;
                        model.ProductState = 1;

                        if (bll.Update(model))
                        {
                            Completed = true;
                        }
                    }
                    else
                    {

                    }
                }

                else
                {
                    model.updateDate = DateTime.Now;
                    model.ProductState = 1;
                    if (bll.Update(model))
                    {
                        Completed = true;
                    }
                }
            }
            catch
            {

            }
        }
    }
}
