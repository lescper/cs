using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using FxProductMonitor.BLL;
using FxProductMonitor.Model;
using Newtonsoft.Json;
using logs = FxProductMonitor.Model.logs;
using MJLDProducts = FxProductMonitor.BLL.MJLDProducts;
using ProductList = FxProductMonitor.Model.ProductList;
using Products = FxProductMonitor.BLL.Products;
using ThreadState = System.Threading.ThreadState;
using Tickets = FxProductMonitor.BLL.Tickets;

namespace FxProductMonitor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        readonly Tickets bll = new Tickets();
        Products prod_bll = new Products();

        public bool isRunning = false;
        public bool Started = false;
        private void button1_Click(object sender, EventArgs e)
        {
            notifyIcon1.Dispose();
            Environment.Exit(0);
            Application.Exit();
        }

        private void TongchengProducts()
        {
            try
            {
                SetTextValue("开始更新同程产品。");
                var newProductCount = 0;
                var totalCount = 0;
                var pageIndex = 1;
                var msg = "";
                if (bll.SetProductState(0) <= 0)
                {
                    msg += "设置产品状态出现错误。";
                    SetTextValue("设置产品状态出现错误。");
                }
                while (true)
                {
                    var request = WebRequest.Create("http://www.lvcang.cn/jingqu/Services/GetTicketDetailService.ashx");
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    var postData = "{\"requestHead\": {\"digitalSign\": \"2455aab0f2f31cb9d93641a410b23df4\",\"agentAccount\": \"aedf7256-d867-4850-aa78-698c86527a74\"},\"requestBody\": {\"pageSize\": 20,\"pageIndex\":" + pageIndex + "}}";
                    var byteArray = Encoding.UTF8.GetBytes(postData);
                    request.ContentLength = byteArray.Length;
                    var dataStream = request.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    var response = request.GetResponse();
                    dataStream = response.GetResponseStream();
                    var sr = new StreamReader(dataStream);
                    var result = sr.ReadToEnd();
                    if (result.IndexOf("未找到符合条件的数据", StringComparison.Ordinal) >= 0)
                    {
                        break;
                    }
                    pageIndex++;
                    result = result.Substring(result.IndexOf("ticketPriceList", StringComparison.Ordinal) + 17);
                    result = result.Substring(0, result.Length - 1);
                    while (result.IndexOf("exceptDate\":[", StringComparison.Ordinal) > 0)
                    {
                        var exceptDate = result.Substring(result.IndexOf("exceptDate\":[", StringComparison.Ordinal) + 12, result.IndexOf("],\"sceneryId", StringComparison.Ordinal) - result.IndexOf("exceptDate\":[", StringComparison.Ordinal) - 11);
                        result = result.Replace(exceptDate, "\"" + Convert.ToBase64String(Encoding.UTF8.GetBytes(exceptDate)) + "\"");
                    }
                    var tcList = JsonConvert.DeserializeObject<List<Model.Tickets>>(result);

                    foreach (var model in tcList)
                    {
                        var id = bll.GetTicketId(model.ticketPriceId.ToString());
                        model.updated = 1;
                        totalCount++;
                        model.exceptDate = Encoding.UTF8.GetString(Convert.FromBase64String(model.exceptDate));
                        if (id <= 0)
                        {
                            bll.Add(model);
                            //richTextBox1.Text += "发现新票，产品编号：" + model.ticketPriceId.ToString() + "，产品名称：" + model.ticketName + "，景区名称：" + model.sceneryName + "。\r\n";
                            newProductCount++;
                        }
                        else
                        {
                            model.id = id;
                            bll.Update(model);
                        }
                    }
                }
                SetTextValue("更新完毕，统计并清理产品。");
                var deletedProduct = bll.GetRecordCount("updated <= 0");
                msg += "更新产品：" + totalCount + "。已经下架的产品：" + deletedProduct + "。";
                SetTextValue("同程产品更新结束，更新产品数量：" + totalCount + "，已下架的产品数量：" + deletedProduct);
                var deletedList = bll.GetModelList("updated <= 0");
                if (deletedList.Count != 0)
                {
                    foreach (var m in deletedList)
                    {
                        msg += m.ticketName;
                        bll.Delete(m.id);
                        SetTextValue(m.ticketName);
                    }
                }
                var log = new logs { related_product = "", log_text = msg, log_date = DateTime.Now };
                var logsBll = new BLL.logs();
                logsBll.Add(log);
                SetTextValue("同程产品操作完毕。");
            }
            catch (Exception ex)
            {
                SetTextValue("出现异常：原因为：" + ex.Message);
            }
        }


        private delegate void SetTextBoxValue(string value);

        private void SetTextValue(string value)
        {
            if (richTextBox1.InvokeRequired)
            {
                var objSetTextValue = new SetTextBoxValue(SetTextValue);
                var result = richTextBox1.BeginInvoke(objSetTextValue, new object[] { value });
                try
                {
                    objSetTextValue.EndInvoke(result);
                }
                catch
                {

                }
            }
            else
            {
                value = "【" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "】" + value;
                richTextBox1.Text += value += Environment.NewLine;
                richTextBox1.SelectionStart = richTextBox1.TextLength;
                richTextBox1.ScrollToCaret();
            }
        }
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int CreateThread(
                   IntPtr lpThreadAttributes,
                   UInt32 dwStackSize,
                   ThreadStart lpStartAddress,
                   IntPtr lpParameter,
                   UInt32 dwCreationFlags,
                   UInt32 lpThreadId);

        [DllImport("kernel32.dll")]
        static extern bool GetExitCodeThread(IntPtr hThread, out UInt32 lpExitCode);

        private void button2_Click_1(object sender, EventArgs e)
        {
            GetYuanFanProducts(); return;
            var handle = CreateThread(IntPtr.Zero, 0, TongchengProducts, IntPtr.Zero, 0, 0);
            UInt32 exitCode = 9999;
            GetExitCodeThread((IntPtr)handle, out exitCode);
            while (exitCode == (UInt32)259)
            {
                Application.DoEvents();
                GetExitCodeThread((IntPtr)handle, out exitCode);
            }

            SetTextValue("开始更新美景联动数据。");
            var getUtsThread = new Thread(GetUtsProduct);
            getUtsThread.Start();
            while (getUtsThread.ThreadState != ThreadState.Stopped)
            {
                Application.DoEvents();
            }
            SetTextValue("美景联动数据更新结束。");

            GetProductApi();
            SetTextValue("获取产品结束。");

            GetProductInfo();
            SetTextValue("获取价格日期结束。");

            var compareThread = new Thread(CompareProduct);
            compareThread.Start();
            while (compareThread.ThreadState != ThreadState.Stopped)
            {
                Application.DoEvents();
            }
            SetTextValue("产品对比已完成。");


            SendMail();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer2.Stop();
            button3.Enabled = false;
            button4.Enabled = true;
            timer4.Stop();
        }

        string _html = "";
        [DllImport("KERNEL32.DLL", EntryPoint = "GetCurrentProcess", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        static extern IntPtr GetCurrentProcess();
        [DllImport("KERNEL32.DLL", EntryPoint = "SetProcessWorkingSetSize", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        static extern bool SetProcessWorkingSetSize(IntPtr hProcess, int
          dwMinimumWorkingSetSize, int dwMaximumWorkingSetSize);

        [DllImport("wininet.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern IntPtr InternetOpen(
           string lpszAgent, int dwAccessType, string lpszProxyName,
           string lpszProxyBypass, int dwFlags);

        private void button4_Click(object sender, EventArgs e)
        {
            button4.Enabled = false;
            button3.Enabled = true;

            timer4.Interval = 1000;
            timer4.Start();
            //timer2.Interval = 1000;
            //timer2.Start();

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            notifyIcon1.Dispose();
            Environment.Exit(0);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button3.Enabled = false;
        }





        private void timer2_Tick(object sender, EventArgs e)
        {
            //IntPtr pHandle = GetCurrentProcess();
            //SetProcessWorkingSetSize(pHandle, -1, -1);
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            try
            {
                ThreadPool.SetMaxThreads(200, 200);
                var dt1 = comboBox2.SelectedItem + ":" + comboBox1.SelectedItem + ":00";
                var dt2 = comboBox3.SelectedItem + ":" + comboBox4.SelectedItem + ":00";
                var now = DateTime.Now.ToString("HH:mm:ss");


                if (now == dt1 || now == dt2)
                {
                    timer4.Stop();

                    var t = new Thread(TongchengProducts);
                    t.Start();
                    while (t.ThreadState != ThreadState.Stopped)
                    {
                        Application.DoEvents();
                    }


                    GetProductApi();

                    GetPriceAndDate();



                    SetTextValue("门票对比已结束，发送邮件。");
                    var logs_bll = new BLL.logs();
                    var log_list = new List<logs>();
                    log_list = logs_bll.GetModelList("log_state is null");

                    var emailText = "";
                    emailText += "【同程】已下架的产品：\r\n";
                    emailText = log_list.Where(x => x.log_text.Contains("已下架")).Aggregate(emailText, (current, xModel) => current + (xModel.related_product + "\r\n"));
                    emailText += "\r\n";
                    emailText += "【同程】分销价小于同程价：\r\n";
                    emailText = log_list.Where(x => x.log_text.Contains("分销价小于同程价")).Aggregate(emailText, (current, model) => current + (model.related_product + "。" + model.log_text.Split('；')[1] + "\r\n"));
                    emailText += "\r\n\r\n";
                    emailText += "【同程】提前天数设置错误：\r\n";
                    emailText = log_list.Where(x => x.log_text.Contains("提前天数设置错误")).Aggregate(emailText, (current, xModel) => current + (xModel.related_product + "。" + xModel.log_text.Split('；')[1] + "\r\n"));
                    emailText += "\r\n\r\n";
                    emailText += "【同程】开始或结束日期设置错误：\r\n";
                    emailText = log_list.Where(x => x.log_text.Contains("日期设置错误")).Aggregate(emailText, (current, xModel) => current + (xModel.related_product + "," + xModel.log_text + "\r\n"));

                    emailText += "\r\n\r\n";
                    emailText += "【同程】应排除日期：\r\n";
                    emailText = log_list.Where(x => x.log_text.Contains("应排除日期")).Aggregate(emailText, (current, xModel) => current + (xModel.related_product + xModel.log_text.Replace("应排除日期：", "") + "\r\n"));

                    logs_bll.SetLogState().ToString();
                    var mail = new SendMail();
                    mail.Send(emailText);


                    //for (int i = 0; i < 100000; i++)
                    //{
                    //    Application.DoEvents();
                    //}
                    //timer2.Start();
                }
            }
            catch (Exception ex)
            {
                SetTextValue(ex.Message);
                var mail = new SendMail();
                mail.Send(ex.Message);
            }
            timer4.Start();

        }

        public void GetProductApi()
        {
            var productListBll = new BLL.ProductList();
            if (productListBll.SetProductState() <= 0)
            {
                SetTextValue("设置产品状态失败。");
            }
            var url = "http://fx.henghengw.net/api/list_hh.jsp";
            url += "?custId=234200&apikey=05908E8BF370E0BFB58F36F15774E499";
            url += "&treeId=0&pageNum=1&pageNo=1";
            var totalPage = 0;
            var request = WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();
            var dataStream = response.GetResponseStream();
            var sr = new StreamReader(dataStream);

            var data = sr.ReadToEnd();
            var totalNum = data.Substring(0, data.IndexOf("</totalNum>", StringComparison.Ordinal));
            totalNum = totalNum.Substring(totalNum.IndexOf("<totalNum>", StringComparison.Ordinal) + 10);
            totalNum = totalNum.Replace("<![CDATA[", "");
            totalNum = totalNum.Replace("]]>", "");
            var pageCount = totalPage;
            totalPage = Convert.ToInt32(totalNum);
            pageCount = totalPage / 100;
            if (totalPage % 100 > 0)
                pageCount++;

            var bll = new GetProduct();
            var threadList = new List<Thread>();
            for (var i = 0; i < pageCount; i++)
            {
                bll = new GetProduct();
                bll.CurrentPage = i + 1;
                threadList.Add(new Thread(bll.Get));
                threadList[i].Start();
            }
            foreach (var t in threadList)
            {

                while (t.ThreadState != ThreadState.Stopped)
                {
                    Application.DoEvents();
                }
            }
            var recordCount = productListBll.GetRecordCount("ProductState=0");
            SetTextValue("产品下架数量：" + recordCount.ToString(CultureInfo.InvariantCulture));
            var prodList = productListBll.GetModelList("ProductState=0");
            var logBll = new BLL.logs();
            foreach (var m in prodList)
            {
                var logs = new logs
                {
                    log_date = DateTime.Now,
                    log_text = "【产品下架】",
                    related_product = "【产品编号】：" + m.productNo + "，【产品名称】：" + m.productName,
                };
                logBll.Add(logs);
                productListBll.Delete(m.id);
            }
        }

        public void GetInterface()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var productBll = new BLL.ProductList();
            productBll.SetProductState();
            List<ProductList> prodList = productBll.GetModelList("");

            foreach (var model in prodList)
            {
                var htmlBll = new GetHtml
                {
                    prod_id = Convert.ToInt32(model.productNo),
                    ProductId = model.id,
                    Completed = false
                };
                var t = new Thread(htmlBll.Html);
                t.Start();
                while (t.ThreadState != ThreadState.Stopped)
                {
                    Application.DoEvents();
                }
                SetTextValue(model.id.ToString(CultureInfo.InvariantCulture));

            }

            var unCount = productBll.GetRecordCount("ProductState=0");

            var unList = new List<Thread>();
            var unNum = 0;
            while (unCount > 0)
            {
                prodList = productBll.GetModelList("ProductState=0");
                foreach (var prodModel in prodList)
                {
                    var htmlBll = new GetHtml
                    {
                        prod_id = Convert.ToInt32(prodModel.productNo),
                        ProductId = prodModel.id,
                        Completed = false
                    };
                    var t = new Thread(htmlBll.Html);
                    t.Start();
                    while (t.ThreadState != ThreadState.Stopped)
                    {
                        Application.DoEvents();
                    }
                    SetTextValue("重复的线程：" + prodModel.id.ToString(CultureInfo.InvariantCulture));
                    unNum++;
                }

                unCount = productBll.GetRecordCount("ProductState=0");
            }
            stopWatch.Stop();
            SetTextValue("更新接口信息结束。");
            SetTextValue(stopWatch.Elapsed.TotalSeconds.ToString(CultureInfo.InvariantCulture));
        }

        private void GetPriceAndDate()
        {
            try
            {
                var bll = new BLL.ProductList();
                var list = new List<ProductList>();
                list = bll.GetModelList("interfaceid=10000218");
                foreach (var m in list)
                {
                    if (!string.IsNullOrEmpty(m.interfaceProdId.Trim()))
                    {
                        var price_bll = new GetPrice();
                        price_bll.ProdId = Convert.ToInt32(m.productNo);
                        price_bll.ProductName = m.productName;
                        price_bll.TongchengProdId = Convert.ToInt32(m.interfaceProdId);
                        var pThread = new Thread(price_bll.Get);
                        pThread.Start();
                        while (pThread.ThreadState != ThreadState.Stopped)
                        {
                            Application.DoEvents();
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            WindowState = FormWindowState.Normal;
            Show();
        }

        private void Form1_MinimumSizeChanged(object sender, EventArgs e)
        {
            Hide();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            //if (WindowState == FormWindowState.Minimized)
            //{
            //    this.Hide();
            //}
        }


        public void CompareProduct()
        {
            var list = new List<ProductList>();
            var productBll = new BLL.ProductList();

            int pageIndex = 1, pageSize = 100;
            var recordCount = productBll.GetRecordCount("");
            var pageCount = recordCount / pageSize;
            var totalCount = 0;
            if (recordCount % pageSize > 0)
                pageCount++;
            for (var i = pageIndex; i <= pageCount; i++)
            {
                pageIndex = i;
                var dt = productBll.GetListByPage("", "id", (pageIndex - 1) * pageSize + 1, pageIndex * pageSize).Tables[0];
                list = productBll.DataTableToList(dt);
                var errLogBll = new BLL.logs();
                foreach (var model in list)
                {
                    switch (model.interfaceId)
                    {
                        case "10000218":
                            {
                                var ticketsBll = new Tickets();
                                var ticketsModel = ticketsBll.GetModelByTicketId(Convert.ToInt32(model.interfaceProdId));
                                if (ticketsModel == null)
                                {
                                    var errLog = new logs();
                                    errLog.log_category = 0;
                                    errLog.log_date = DateTime.Now;
                                    errLog.log_text = "【同程】已下架。";
                                    errLog.related_product = "【产品编号】：" + model.productNo.ToString() + "，【产品名称】：" + model.productName;
                                    errLogBll.Add(errLog);
                                    break;
                                }
                                Compare(ticketsModel.agentPrice, Convert.ToInt32(ticketsModel.reserveBeforeDays), ticketsModel.startSellDate, ticketsModel.effectiveEndDate, ticketsModel.isRealName, model, "同程", ticketsModel.exceptDate, ticketsModel.stopSellDate);
                                totalCount++;
                                break;
                            }

                        case "10000210":
                            {
                                var uts = new MJLDProducts();
                                var mjldModel = new Model.MJLDProducts();
                                mjldModel = uts.GetModelById(Convert.ToInt32(model.interfaceProdId));
                                if (mjldModel == null)
                                {
                                    var errLog = new logs();
                                    errLog.log_category = 0;
                                    errLog.log_date = DateTime.Now;
                                    errLog.log_text = "【美景联动】已下架。";
                                    errLog.related_product = "【产品编号】：" + model.productNo.ToString() + "，【产品名称】：" + model.productName;
                                    errLogBll.Add(errLog);
                                    break;
                                }
                                Compare(mjldModel.SettlementPrice, 0, mjldModel.ActDate, mjldModel.RequireDate, "", model, "美景联动", "", "");
                                break;
                            }
                    }
                }
            }
        }

        public void Compare(string settlementPrice, int startNum, string startDate, string endDate, string isSingle, ProductList model, string name, string excptDate, string stopSellDate)
        {

            var logBll = new BLL.logs();
            var logsModel = new logs();

            var startDt = Convert.ToDateTime(startDate);
            var endDt = Convert.ToDateTime(endDate);

            if (DateTime.Compare(startDt, DateTime.Now) > 0)
            {
                if (DateTime.Compare(startDt, Convert.ToDateTime(model.priceStartDate)) > 0)
                {
                    logsModel.log_category = 1;
                    logsModel.log_date = DateTime.Now;
                    logsModel.log_text = "【" + name + "】开始日期过早；";
                    logsModel.log_text += name + "开始日期：" + startDate;
                    logsModel.related_product = "【产品编号】：" + model.productNo.ToString();
                    logsModel.related_product += "，【产品名称】：" + model.productName;
                    logBll.Add(logsModel);
                }
            }

            if (DateTime.Compare(endDt, DateTime.Now) > 0)
            {
                if (DateTime.Compare(endDt, Convert.ToDateTime(model.priceEndDate)) < 0)
                {
                    logsModel = new logs();
                    logsModel.log_category = 2;
                    logsModel.log_date = DateTime.Now;
                    logsModel.log_text = "【" + name + "】结束日期过晚；";
                    logsModel.log_text += name + "结束日期：" + endDate;
                    logsModel.related_product = "【产品编号】：" + model.productNo.ToString();
                    logsModel.related_product += "，【产品名称】：" + model.productName;
                    logBll.Add(logsModel);
                }
            }

            if (!string.IsNullOrEmpty(stopSellDate))
            {
                if (Convert.ToDateTime(stopSellDate).AddDays(-1).ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))
                {
                    logsModel = new logs();
                    logsModel.log_category = 6;
                    logsModel.log_date = DateTime.Now;
                    logsModel.log_text = "【" + name + "】产品即将下架，下架日期：" + stopSellDate;
                    logsModel.related_product = "【产品编号】：" + model.productNo.ToString();
                    logsModel.related_product += "，【产品名称】：" + model.productName;
                    logBll.Add(logsModel);
                }
            }

            var price = Convert.ToDouble(settlementPrice);
            var priceRmb = Convert.ToDouble(model.SettlementPrice);

            if (price > priceRmb)
            {
                logsModel = new logs();
                logsModel.log_category = 3;
                logsModel.log_date = DateTime.Now;
                logsModel.log_text = "【" + name + "】分销价小于" + name + "价。";
                logsModel.log_text += name + "价：" + settlementPrice;
                logsModel.related_product = "【产品编号】：" + model.productNo.ToString();
                logsModel.related_product += "，【产品名称】：" + model.productName;
                logBll.Add(logsModel);
            }

            if (Convert.ToInt32(model.StartNum) < Convert.ToInt32(startNum))
            {
                logsModel = new logs();
                logsModel.log_category = 4;
                logsModel.log_date = DateTime.Now;
                logsModel.log_text = "【" + name + "】提前天数设置错误。";
                logsModel.log_text += name + "提前天数：" + startNum;
                logsModel.related_product = "【产品编号】：" + model.productNo.ToString();
                logsModel.related_product += "，【产品名称】：" + model.productName;
                logBll.Add(logsModel);
            }

            if (!string.IsNullOrEmpty(excptDate))
            {
                var url = "http://fx.henghengw.net/api/price.jsp?productNo=" + model.productNo.ToString();
                url += "&custId=234200&apikey=05908E8BF370E0BFB58F36F15774E499";
                var request = WebRequest.Create(url);
                var response = (HttpWebResponse)request.GetResponse();
                var stream = response.GetResponseStream();
                var dtReader = new StreamReader(stream);
                var dtData = dtReader.ReadToEnd();
                stream.Close();
                response.Close();
                dtReader.Close();
                logsModel = new logs();
                excptDate = excptDate.Replace("[", "").Replace("]", "").Replace("\"", "");
                foreach (var str in excptDate.Split(','))
                {
                    if (string.IsNullOrEmpty(str))
                        continue;
                    var dt = Convert.ToDateTime(str);
                    if (DateTime.Compare(dt, DateTime.Now) > 0)
                    {
                        var lastDay = Convert.ToDateTime(DateTime.Now.Year.ToString() + "-" + (DateTime.Now.AddMonths(1).Month).ToString() + "-01");
                        if (DateTime.Compare(dt, lastDay) < 0)
                        {
                            if (dtData.IndexOf(str) >= 0)
                            {
                                logsModel.log_text += str + ",";
                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(logsModel.log_text))
                {
                    logsModel.log_text = "【" + name + "】" + "应排除日期：" + logsModel.log_text;
                    logsModel.log_category = 5;
                    logsModel.log_date = DateTime.Now;
                    logsModel.related_product = "【产品编号】：" + model.productNo.ToString() + "，【产品名称】：" + model.productName;
                    logBll.Add(logsModel);
                }
            }



        }

        public void GetProductInfo()
        {
            var list = new List<ProductList>();
            var bll = new BLL.ProductList();
            list = bll.GetModelList("");
            var threads = new List<Thread>();
            var n = 0;
            foreach (var prodModel in list)
            {
                var getBll = new GetProductInfo();
                getBll.ProdId = Convert.ToInt32(prodModel.productNo);
                threads.Add(new Thread(getBll.Get));
                threads[n].Start();
                n++;
            }
            foreach (var one in threads)
            {
                while (one.ThreadState != ThreadState.Stopped)
                {
                    Application.DoEvents();
                }
            }
        }

        public void GetUtsProduct()
        {
            var model = new MJLDProductRequest
            {
                AreaId = 0,
                AreaName = "",
                goodsName = "",
                PageIndex = 1,
                PageSize = 1,
                password = "a1s2d3",
                SaleType = 1,
                ThemeId = 0,
                timeStamp =
                    Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds).ToString(CultureInfo.InvariantCulture),
                user = "liuyang"
            };
            var request = (HttpWebRequest)WebRequest.Create("http://outer.mjld.com.cn/Outer/Interface/SelectProductList");
            request.Method = "POST";
            var byteData = TCodeServiceCrypt.GetPostData(model);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteData.Length;
            var dataStream = request.GetRequestStream();
            dataStream.Write(byteData, 0, byteData.Length);
            dataStream.Close();
            var response = request.GetResponse();
            dataStream = response.GetResponseStream();
            var sr = new StreamReader(dataStream);
            var data = sr.ReadToEnd();
            sr.Close();
            response.Close();
            dataStream.Close();
            data = TCodeServiceCrypt.Decrypt3DESFromBase64(data, TCodeServiceCrypt.keyStr);
            var itemCount = data.Substring(data.IndexOf("<ItemCount>", StringComparison.Ordinal) + 11);
            itemCount = itemCount.Substring(0, itemCount.IndexOf("<", StringComparison.Ordinal));
            SetTextValue(itemCount);
            var totalCount = Convert.ToInt32(itemCount);
            var pageSize = 255;
            var pageCount = totalCount / pageSize;
            if (totalCount % pageSize > 0)
                totalCount++;
            var mjldBll = new MJLDProducts();
            mjldBll.SetProductState();
            var getUts = new List<Thread>();
            for (var i = 1; i <= totalCount; i++)
            {
                var uts = new Uts();

                uts.PageIndex = i;
                uts.PageSize = 255;
                getUts.Add(new Thread(uts.Get));
                getUts[i - 1].Start();
            }

            foreach (var utsThread in getUts)
            {
                while (utsThread.ThreadState != ThreadState.Stopped)
                {
                    Application.DoEvents();
                }
            }
            var mjProducts = new List<Model.MJLDProducts>();
            SetTextValue("已下架的产品：" + mjProducts.Count.ToString());
            foreach (var mjld in mjProducts)
            {
                mjldBll.Delete(mjld.mj_id);
            }
            SetTextValue("美景联动产品更新完毕。");
        }

        private void SendMail()
        {
            var strTxt = "";
            var log = new List<logs>();
            var logBll = new BLL.logs();
            log = logBll.GetModelList("log_state is null");

            if ((log.Any(x => x.log_category == 0)))
            {
                strTxt += "【已下架的产品】：\r\n";
                strTxt = log.Where(x => x.log_category == 0).Aggregate(strTxt, (current, logModel) => current + (logModel.related_product + "\r\n"));
                strTxt += "\r\n";
            }

            if (log.Any(x => x.log_category == 1))
            {
                strTxt += "【开始日期错误】：\r\n";
                strTxt = log.Where(x => x.log_category == 1).Aggregate(strTxt, (current, logModel) => current + (logModel.related_product + "，" + logModel.log_text.Replace("开始日期过早", "") + "\r\n"));

                strTxt += "\r\n";
            }

            if (log.Any(x => x.log_category == 2))
            {
                strTxt += "【结束日期错误】：\r\n";
                strTxt = log.Where(x => x.log_category == 2).Aggregate(strTxt, (current, logModel) => current + (logModel.related_product + "，" + logModel.log_text.Replace("结束日期过晚", "") + "\r\n"));
                strTxt += "\r\n";
            }

            if (log.Any(x => x.log_category == 3))
            {
                strTxt += "【分销价设置错误】：\r\n";
                strTxt = log.Where(x => x.log_category == 3).Aggregate(strTxt, (current, logModel) => current + (logModel.related_product + "，" + logModel.log_text + "\r\n"));
                strTxt += "\r\n";
            }

            if (log.Any(x => x.log_category == 4))
            {
                strTxt += "【提前天数设置错误】：\r\n";
                strTxt = log.Where(x => x.log_category == 4).Aggregate(strTxt, (current, logModel) => current + (logModel.related_product + "，" + logModel.log_text.Replace("提前天数设置错误", "") + "\r\n"));
                strTxt += "\r\n";
            }

            if (log.Any(x => x.log_category == 5))
            {
                strTxt += "【应排除日期】：\r\n";
                strTxt = log.Where(x => x.log_category == 5).Aggregate(strTxt, (current, logModel) => current + (logModel.related_product += "，" + logModel.log_text + "\r\n"));
                strTxt += "\r\n";
            }

            if (log.Any(x => x.log_category == 6))
            {
                strTxt += "【产品即将下架】：\r\n";
                strTxt = log.Where(x => x.log_category == 6).Aggregate(strTxt, (current, logModel) => current + (logModel.related_product += "，" + logModel.log_text + "\r\n"));
                strTxt += "\r\n";
            }
            logBll.SetLogState();
            var mail = new SendMail();
            mail.Send(strTxt);
        }
        public string GetMd5(string str)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] md5bytes = System.Text.UTF8Encoding.Default.GetBytes(str);
            byte[] output = md5.ComputeHash(md5bytes);
            str = BitConverter.ToString(output, 4, 8);
            str = str.Replace("-", "").ToLower();
            return str;
        }

        public void GetYuanFanProducts()
        {

            string requestData = "";
            requestData = "{\"Head\":{\"account\":\"亨亨票务\",\"SequenceId\":\"sid\",\"sign\":\"md5sign\",\"TimeStamp\":\"时间戳\",\"organization\":\"323\",\"method\":\"GetProduct\"},\"Body\":{";
            requestData += "\"Data\": \"ALL\"}";
            requestData = requestData.Replace("时间戳", DateTime.Now.ToString("yyyyMMddHHmmss"));
            Random rnd = new Random();
            string sid = rnd.Next(10000000, 99999999).ToString();
            requestData = requestData.Replace("sid", sid);
            requestData = requestData.Replace("md5sign", GetMd5("323" + sid + DateTime.Now.ToString("yyyyMMddHHmmss") + "D32B6CDBA001D645A6A346DA"));
            requestData = FxProductMonitor.BLL.TCodeServiceCrypt.Encrypt3DESToBase64(requestData, "D70A690DD21ADD12DA1101A2");
            string url = "http://pw.yfpw.cn/yfinterface/yfInterface.ashx";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ProtocolVersion = HttpVersion.Version11;

            request.KeepAlive = true;
            request.Headers.Add("Accept-Language", "zh-cn");
            request.Accept = "*/*";
            request.Method = "POST";
            byte[] b = Encoding.UTF8.GetBytes(requestData);
            request.ContentLength = b.Length;
            request.ContentType = "application/x-www-form-urlencoded";
            using (Stream sw = request.GetRequestStream())
            {
                sw.Write(b, 0, b.Length);
            }

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("gb2312")))
                {
                    string data = sr.ReadToEnd();
                    SetTextValue(BLL.TCodeServiceCrypt.Decrypt3DESFromBase64(data, "D70A690DD21ADD12DA1101A2"));
                    JsonConvert.DeserializeObject
                }
            }
        }
    }
}
