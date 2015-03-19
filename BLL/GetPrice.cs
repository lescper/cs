using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using SeasideResearch.LibCurlNet;

namespace FxProductMonitor.BLL
{
    public class GetPrice
    {
        public int ProdId { get; set; }
        public int TongchengProdId { get; set; }
        public string ProductName { get; set; }

        private string Html { get; set; }
        private Model.Tickets _model = new Model.Tickets();
        private readonly Tickets _bll = new Tickets();
        public void CompareProduct()
        {
            if (ProdId <= 0 || TongchengProdId <= 0) return;
            _model = _bll.GetModelByTicketId(TongchengProdId);
            if (_model == null)
            {
                //产品已下架。
                var log = new Model.logs();
                log.log_date = DateTime.Now;
                log.log_text = "【同程】已下架。";
                log.related_product = "【产品编号】：" + ProdId.ToString(CultureInfo.InvariantCulture) + "，【产品名称】：" + ProductName;
                var logBll = new logs();
                logBll.Add(log);
                return;
            }
            Html = "";
            Curl.GlobalInit((int)CURLinitFlag.CURL_GLOBAL_ALL);
            var easyOpt = new Easy();
            easyOpt.SetOpt(CURLoption.CURLOPT_URL, "http://fx.henghengw.net/prod/list_ajax.jsp?action=setticketrole&currency_type=0&year=" + DateTime.Now.Year + "&month=" + DateTime.Now.Month + "&info_id=" + ProdId.ToString());
            easyOpt.SetOpt(CURLoption.CURLOPT_COOKIEFILE, "cookie.txt");
            var wf = new Easy.WriteFunction(GetFunc);
            easyOpt.SetOpt(CURLoption.CURLOPT_WRITEFUNCTION, wf);
            easyOpt.Perform();
            easyOpt.Cleanup();
        }

        private int GetFunc(byte[] buf, int size, int memb, object extraData)
        {
            var msg = System.Text.Encoding.UTF8.GetString(buf);
            string week = "";
            var logBll = new logs();
            var logsModel = new Model.logs();

            if (!string.IsNullOrEmpty(Html) || msg.IndexOf("start_date") >= 0)
                Html += msg;
            if (Html.IndexOf("start_date") >= 0 && Html.IndexOf("/>天</td>") >= 0)
            {
                string startDate = Html.Substring(Html.IndexOf("start_date") + 19);
                startDate = startDate.Substring(0, startDate.IndexOf("\""));

                string endDate = Html.Substring(Html.IndexOf("end_date") + 17);
                endDate = endDate.Substring(0, endDate.IndexOf("\""));

                string conferPriceRmb = Html.Substring(Html.IndexOf("confer_price_rmb") + 25);
                conferPriceRmb = conferPriceRmb.Substring(0, conferPriceRmb.IndexOf("\""));

                string priceRmb = Html.Substring(Html.IndexOf("\"price_rmb") + 19);
                priceRmb = priceRmb.Substring(0, priceRmb.IndexOf("\""));

                string startNum = Html.Substring(Html.IndexOf("start_num") + 18);
                startNum = startNum.Substring(0, startNum.IndexOf("\""));

                string startDay = Html.Substring(Html.IndexOf("start_day") + 18);
                startDay = startDay.Substring(0, startDay.IndexOf("\""));

                var startDt = Convert.ToDateTime(startDate);
                var endDt = Convert.ToDateTime(endDate);
                if (DateTime.Compare(startDt, Convert.ToDateTime(_model.startSellDate)) < 0)
                {
                    if (DateTime.Compare(startDt, DateTime.Now) >= 0)
                    {
                        logsModel.log_text += "开始日期设置错误；";
                        logsModel.log_text += "同程开始日期：" + _model.startSellDate + "。";
                    }

                }

                if (DateTime.Compare(endDt, Convert.ToDateTime(_model.stopSellDate)) > 0)
                {
                    if (DateTime.Compare(endDt, DateTime.Now) >= 0)
                    {
                        logsModel.log_text += "结束日期设置错误；";
                        logsModel.log_text += "同程结束日期：" + _model.stopSellDate + "。";
                    }
                }




                if (!string.IsNullOrEmpty(_model.exceptDate.Replace("[", "").Replace("]", "")))
                {
                    var dt = _model.exceptDate.Replace("\"", "").Replace("[", "").Replace("]", "");
                    foreach (var s in from s in dt.Split(',') let exceptDt = Convert.ToDateTime(s) where exceptDt.Month == DateTime.Now.Month where exceptDt.Day >= DateTime.Now.Day where DateTime.Compare(exceptDt, Convert.ToDateTime(_model.startSellDate)) >= 0 && DateTime.Compare(exceptDt, Convert.ToDateTime(_model.stopSellDate)) <= 0 select s)
                    {
                        logsModel.log_text += "应排除日期：" + s + "；";
                    }
                }

                var dPrice = Convert.ToDouble(priceRmb);//分销价
                var dConferPrice = Convert.ToDouble(conferPriceRmb);//采购价
                var priceLog = new Model.logs();
                if (dPrice < dConferPrice)
                {
                    logsModel.log_text += "分销价小于采购价；";
                }

                if (dPrice < Convert.ToDouble(_model.agentPrice))
                {
                    logsModel.log_text += "分销价小于同程价；";
                    logsModel.log_text += "同程价：" + _model.agentPrice + "。";

                }

                if (Convert.ToInt32(startDay) < _model.reserveBeforeDays)
                {
                    logsModel.log_text += "提前天数设置错误；";
                    logsModel.log_text += string.Format("应提前天数：{0}。", _model.reserveBeforeDays.ToString());
                }

                _model.updated = 1;
                _bll.Update(_model);
                if (!string.IsNullOrEmpty(logsModel.log_text))
                {
                    logsModel.log_date = DateTime.Now;
                    logsModel.related_product = "【产品编号】：" + ProdId.ToString(CultureInfo.InvariantCulture) + "，【产品名称】：" + ProductName;
                    logsModel.log_text = logsModel.log_text;
                    logBll.Add(logsModel);
                }
                Application.ExitThread();
                return 0;
            }
            return size * memb;
        }

        public void Get()
        {
            if (ProdId <= 0 || TongchengProdId <= 0) return;
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


                    _model = _bll.GetModelByTicketId(TongchengProdId);
                    if (_model == null)
                    {
                        //产品已下架。
                        var log = new Model.logs
                        {
                            log_date = DateTime.Now,
                            log_text = "【同程】已下架。",
                            related_product = "【产品编号】：" + ProdId.ToString() + "，【产品名称】：" + ProductName
                        };
                        var logBll = new logs();
                        logBll.Add(log);
                        return;
                    }

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

                    var startDt = Convert.ToDateTime(startDate);
                    var endDt = Convert.ToDateTime(endDate);
                    if (DateTime.Compare(startDt, Convert.ToDateTime(_model.startSellDate)) < 0)
                    {
                        if (DateTime.Compare(startDt, DateTime.Now) >= 0)
                        {
                            logsModel.log_text += "开始日期设置错误；";
                            logsModel.log_text += "同程开始日期：" + _model.startSellDate + "。";
                        }

                    }

                    if (DateTime.Compare(endDt, Convert.ToDateTime(_model.stopSellDate)) > 0)
                    {
                        if (DateTime.Compare(endDt, DateTime.Now) >= 0)
                        {
                            logsModel.log_text += "结束日期设置错误；";
                            logsModel.log_text += "同程结束日期：" + _model.stopSellDate + "。";
                        }
                    }




                    if (!string.IsNullOrEmpty(_model.exceptDate.Replace("[", "").Replace("]", "")))
                    {
                        var dt = _model.exceptDate.Replace("\"", "").Replace("[", "").Replace("]", "");
                        foreach (var s in from s in dt.Split(',') let exceptDt = Convert.ToDateTime(s) where exceptDt.Month == DateTime.Now.Month where exceptDt.Day >= DateTime.Now.Day where DateTime.Compare(exceptDt, Convert.ToDateTime(_model.startSellDate)) >= 0 && DateTime.Compare(exceptDt, Convert.ToDateTime(_model.stopSellDate)) <= 0 select s)
                        {
                            logsModel.log_text += "应排除日期：" + s + "；";
                        }
                    }

                    var dPrice = Convert.ToDouble(priceRmb);//分销价
                    var dConferPrice = Convert.ToDouble(conferPriceRmb);//采购价
                    var priceLog = new Model.logs();
                    if (dPrice < dConferPrice)
                    {
                        logsModel.log_text += "分销价小于采购价；";
                    }

                    if (dPrice < Convert.ToDouble(_model.agentPrice))
                    {
                        logsModel.log_text += "分销价小于同程价；";
                        logsModel.log_text += "同程价：" + _model.agentPrice.ToString(CultureInfo.InvariantCulture) + "。";

                    }

                    if (Convert.ToInt32(startDay) < _model.reserveBeforeDays)
                    {
                        logsModel.log_text += "提前天数设置错误；";
                        logsModel.log_text += "应提前天数：" + _model.reserveBeforeDays.ToString() + "。";
                    }

                    _model.updated = 1;
                    _bll.Update(_model);
                    if (!string.IsNullOrEmpty(logsModel.log_text))
                    {
                        logsModel.log_date = DateTime.Now;
                        logsModel.related_product = "【产品编号】：" + ProdId.ToString(CultureInfo.InvariantCulture) + "，【产品名称】：" + ProductName;
                        logsModel.log_text = logsModel.log_text;
                        log_bll.Add(logsModel);
                    }
                    sr.Close();
                }
                dataStream.Close();
            }
            response.Close();

        }

    }
}
