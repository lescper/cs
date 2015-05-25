using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;
using FxProductMonitor.BLL;
using FxProductMonitor.Model;
using Newtonsoft.Json;
using DadongProducts = FxProductMonitor.BLL.DadongProducts;
using logs = FxProductMonitor.Model.logs;
using MJLDProducts = FxProductMonitor.BLL.MJLDProducts;
using ProductList = FxProductMonitor.Model.ProductList;
using ThreadState = System.Threading.ThreadState;
using Tickets = FxProductMonitor.BLL.Tickets;
using XieProducts = FxProductMonitor.BLL.XieProducts;
using YFPWProducts = FxProductMonitor.BLL.YFPWProducts;

namespace FxProductMonitor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PostQuitMessage(0);
            Environment.Exit(0);
            Application.Exit();
        }

        //同程产品获取
        private void TongchengProducts()
        {
            var newProductCount = 0;
            var totalCount = 0;
            try
            {
                var bll = new Tickets();
                var pageIndex = 1;
                if (bll.SetProductState(0) <= 0)
                {
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
                            newProductCount++;
                        }
                        else
                        {
                            model.id = id;
                            bll.Update(model);
                        }
                    }
                }
                var deletedList = bll.GetModelList("updated <= 0");
                if (deletedList.Count != 0)
                {
                    foreach (var m in deletedList)
                    {
                        bll.Delete(m.id);
                    }
                }
                SetTextValue("同程产品操作完毕。");
            }
            catch (Exception ex)
            {
                SetTextValue("出现异常：原因为：" + ex.Message);
            }
        }


        //private delegate void SetTextBoxValue(string value);
        private void SetTextValue(string value)
        {
            //if (richTextBox1.InvokeRequired)
            //{
            //    var objSetTextValue = new SetTextBoxValue(SetTextValue);
            //    var result = richTextBox1.BeginInvoke(objSetTextValue, new object[] { value });
            //    try
            //    {
            //        objSetTextValue.EndInvoke(result);
            //    }
            //    catch (Exception ex)
            //    {

            //    }
            //}
            //else
            //{
            //    //value = "【" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "】" + value;
            //    richTextBox1.Text += value + Environment.NewLine;
            //    richTextBox1.SelectionStart = richTextBox1.TextLength;
            //    richTextBox1.ScrollToCaret();
            //}

            IntPtr pHandle = FindWindow(null, "产品同步系统");
            PostMessage(pHandle, 99, Marshal.StringToHGlobalAnsi(value), IntPtr.Zero);

        }
        //文本框显示消息



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
            button2.Enabled = false;

            var handle = CreateThread(IntPtr.Zero, 0, TongchengProducts, IntPtr.Zero, 0, 0);
            UInt32 exitCode = 9999;
            GetExitCodeThread((IntPtr)handle, out exitCode);
            while (exitCode == (UInt32)259)
            {
                Application.DoEvents();
                GetExitCodeThread((IntPtr)handle, out exitCode);
            }



            var getUtsThread = new Thread(GetUtsProduct);
            getUtsThread.Start();
            while (getUtsThread.ThreadState != ThreadState.Stopped)
            {
                Application.DoEvents();
            }
            SetTextValue("美景联动数据更新结束。");



            var getDadongThread = new Thread(GetDadongProduct);
            getDadongThread.Start();
            while (getDadongThread.ThreadState != ThreadState.Stopped)
            {
                Application.DoEvents();
            }
            SetTextValue("大东票务数据更新结束。");



            var getYFThread = new Thread(GetYuanFanProducts);
            getYFThread.Start();
            while (getYFThread.ThreadState != ThreadState.Stopped)
            {
                Application.DoEvents();
            }
            SetTextValue("远帆票务数据更新结束。");



            GetXieProducts();
            SetTextValue("谢谢票务操作完成。");



            GetProductApi();
            SetTextValue("获取产品结束。");



            GetProductInfo();
            SetTextValue("获取价格日期结束。");

            GetProductDetail();


            var compareThread = new Thread(CompareProduct);
            compareThread.Start();
            while (compareThread.ThreadState != ThreadState.Stopped)
            {
                Application.DoEvents();
            }
            SetTextValue("产品对比已完成。");


            SendMail();


            button2.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
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

        [DllImport("user32.dll")]
        static extern void PostQuitMessage(int nExitCode);

        //计时器开始
        private void button4_Click(object sender, EventArgs e)
        {
            button4.Enabled = false;
            button3.Enabled = true;

            timer4.Interval = 1000;
            timer4.Start();

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button3.Enabled = false;
        }
        private void timer4_Tick(object sender, EventArgs e)
        {
            try
            {
                var dt1 = comboBox2.SelectedItem + ":" + comboBox1.SelectedItem + ":00";
                var now = DateTime.Now.ToString("HH:mm:ss");


                if (now == dt1)
                {
                    timer4.Stop();

                    #region 同程
                    var handle = CreateThread(IntPtr.Zero, 0, TongchengProducts, IntPtr.Zero, 0, 0);
                    UInt32 exitCode = 9999;
                    GetExitCodeThread((IntPtr)handle, out exitCode);
                    while (exitCode == (UInt32)259)
                    {
                        Application.DoEvents();
                        GetExitCodeThread((IntPtr)handle, out exitCode);
                    }
                    #endregion

                    #region 美景联动
                    var getUtsThread = new Thread(GetUtsProduct);
                    getUtsThread.Start();
                    while (getUtsThread.ThreadState != ThreadState.Stopped)
                    {
                        Application.DoEvents();
                    }
                    SetTextValue("美景联动数据更新结束。");
                    #endregion

                    #region 大东票务
                    var getDadongThread = new Thread(GetDadongProduct);
                    getDadongThread.Start();
                    while (getDadongThread.ThreadState != ThreadState.Stopped)
                    {
                        Application.DoEvents();
                    }
                    SetTextValue("大东票务数据更新结束。");
                    #endregion

                    #region 远帆票务
                    var getYFThread = new Thread(GetYuanFanProducts);
                    getYFThread.Start();
                    while (getYFThread.ThreadState != ThreadState.Stopped)
                    {
                        Application.DoEvents();
                    }
                    SetTextValue("远帆票务数据更新结束。");
                    #endregion

                    #region 谢谢票务
                    GetXieProducts();
                    SetTextValue("谢谢票务操作完成。");
                    #endregion

                    #region 更新产品
                    GetProductApi();
                    SetTextValue("获取产品结束。");
                    #endregion

                    #region 价格日期
                    GetProductInfo();
                    SetTextValue("获取价格日期结束。");
                    #endregion

                    #region 对比产品
                    var compareThread = new Thread(CompareProduct);
                    compareThread.Start();
                    while (compareThread.ThreadState != ThreadState.Stopped)
                    {
                        Application.DoEvents();
                    }
                    SetTextValue("产品对比已完成。");
                    #endregion

                    #region 发送邮件
                    SendMail();
                    #endregion

                    IntPtr pHanlde = GetCurrentProcess();
                    SetProcessWorkingSetSize(pHanlde, -1, -1);
                    //回收内存并开始计时
                    timer4.Start();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            timer4.Start();

        }

        //使用通用API接口获取产品
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
                bll = new GetProduct { CurrentPage = i + 1 };
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

        //获取产品的价格和有效期
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
            this.Show();
        }

        private void Form1_MinimumSizeChanged(object sender, EventArgs e)
        {
            //Hide();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                //this.Hide();
            }
        }


        private void CompareProduct()
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
                                #region 同程票务
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
                                bool needIdCard = false;
                                if (ticketsModel.isNeedIdCard == "True") needIdCard = true;
                                Compare(ticketsModel.agentPrice, Convert.ToInt32(ticketsModel.reserveBeforeDays), ticketsModel.startSellDate, ticketsModel.effectiveEndDate, ticketsModel.isRealName, model, "同程", ticketsModel.exceptDate, ticketsModel.stopSellDate, needIdCard);
                                totalCount++;
                                break;
                                #endregion
                            }

                        case "10000210":
                            {
                                #region 美景联动
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
                                Compare(mjldModel.SettlementPrice, 0, mjldModel.ActDate, mjldModel.RequireDate, "", model, "美景联动", "", "", false);
                                break;
                                #endregion
                            }

                        case "10000184":
                            {
                                #region 远帆票务
                                var yfpw = new YFPWProducts();
                                var yfpwModel = new Model.YFPWProducts();
                                yfpwModel = yfpw.GetModelByProductId(Convert.ToInt32(model.interfaceProdId));
                                if (yfpwModel == null)
                                {
                                    var errLog = new logs();
                                    errLog.log_category = 0;
                                    errLog.log_date = DateTime.Now;
                                    errLog.log_text = "【远帆票务】已下架。";
                                    errLog.related_product = "【产品编号】：" + model.productNo.ToString() + "，【产品名称】：" + model.productName;
                                    errLogBll.Add(errLog);
                                    break;
                                }
                                Compare(yfpwModel.product_platformvalue, 0, yfpwModel.product_start, yfpwModel.product_end, "", model, "远帆票务", "", "", false);
                                break;
                                #endregion
                            }
                        case "10000212":
                            {
                                #region 大东票务
                                var dadongBll = new DadongProducts();
                                var dadongModel = new Model.DadongProducts();
                                dadongModel = dadongBll.GetModelByProductId(Convert.ToInt32(model.interfaceProdId));
                                if (dadongModel == null)
                                {
                                    var errLog = new logs();
                                    errLog.log_category = 0;
                                    errLog.log_date = DateTime.Now;
                                    errLog.log_text = "【大东票务】已下架。";
                                    errLog.related_product = "【产品编号】：" + model.productNo.ToString() + "，【产品名称】：" + model.productName;
                                    errLogBll.Add(errLog);
                                    break;
                                }
                                Compare(dadongModel.product_platformValue, 0, "", dadongModel.product_expireTime, "", model, "大东票务", "", "", false);
                                break;
                                #endregion
                            }

                        case "10000238":
                            {
                                #region 谢谢网
                                var merchBll = new XieProducts();
                                var merchModel = new Model.XieProducts();
                                merchModel = merchBll.GetModelByProductId(Convert.ToInt32(model.interfaceProdId));
                                if (merchModel == null)
                                {
                                    var errLog = new logs();
                                    errLog.log_category = 0;
                                    errLog.log_date = DateTime.Now;
                                    errLog.log_text = "【谢谢网】已下架。";
                                    errLog.related_product = "【产品编号】：" + model.productNo.ToString() + "，【产品名称】：" + model.productName;
                                    errLogBll.Add(errLog);
                                    break;
                                }
                                var selelPrice = merchModel.SellPrice;
                                var expiredTime = merchModel.ValidTime;
                                var stopSellTime = merchModel.MerchDownTime;
                                if (expiredTime.IndexOf("自购买之日起") >= 0)
                                {
                                    expiredTime = "";
                                }
                                if (expiredTime.IndexOf("无限") >= 0)
                                {
                                    expiredTime = DateTime.Now.AddYears(10).ToString("yyyy-MM-dd");
                                }
                                if (stopSellTime.IndexOf("不限") >= 0)
                                {
                                    stopSellTime = DateTime.Now.AddYears(10).ToString("yyyy-MM-dd");
                                }
                                Compare(selelPrice, 0, "", expiredTime, "", model, "谢谢网", "", stopSellTime, false);
                                break;

                                #endregion
                            }
                    }
                }
            }
        }

        public void Compare(string settlementPrice, int startNum, string startDate, string endDate, string isSingle, ProductList model, string name, string excptDate, string stopSellDate, bool isNeedIdCard)
        {

            var logBll = new BLL.logs();
            var logsModel = new logs();

            DateTime startDt;
            if (!string.IsNullOrEmpty(startDate))
                startDt = Convert.ToDateTime(startDate);
            else
                startDt = DateTime.Now.AddYears(-10);
            DateTime endDt;
            if (!string.IsNullOrEmpty(endDate))
            {
                endDt = Convert.ToDateTime(endDate);
            }
            else
            {
                endDt = DateTime.Now.AddYears(-10);
            }

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

            if (isNeedIdCard)
            {
                if (model.custFiled.IndexOf("link_credit_no") < 0)
                {
                    logsModel = new logs();
                    logsModel.log_category = 22;
                    logsModel.log_date = DateTime.Now;
                    logsModel.log_text = "【" + name + "】需要身份证号。";
                    logsModel.related_product = "【产品编号】：" + model.productNo.ToString() + "，【产品名称】：" + model.productName;
                    logBll.Add(logsModel);
                }
            }


            if (!string.IsNullOrEmpty(excptDate))
            {
                var url = "http://fx.henghengw.net/api/price.jsp?productNo=" + model.productNo.ToString();
                url += "&custId=234200&apikey=05908E8BF370E0BFB58F36F15774E499";
                url += "&travelDate=" + DateTime.Now.ToString("yyyy-MM-01");
                var request = WebRequest.Create(url);
                var response = (HttpWebResponse)request.GetResponse();
                var stream = response.GetResponseStream();
                var dtReader = new StreamReader(stream);
                var dtData = dtReader.ReadToEnd();
                stream.Close();
                response.Close();
                dtReader.Close();

                url = "http://fx.henghengw.net/api/price.jsp?productNo=" + model.productNo.ToString();
                url += "&custId=234200&apikey=05908E8BF370E0BFB58F36F15774E499";
                url += "&travelDate=" + DateTime.Now.AddMonths(1).ToString("yyyy-MM-01");
                var nextMonthRequest = WebRequest.Create(url);
                var nextMothResponse = (HttpWebResponse)nextMonthRequest.GetResponse();
                var nextMonthStream = nextMothResponse.GetResponseStream();
                var nextMonthReader = new StreamReader(nextMonthStream);
                dtData += nextMonthReader.ReadToEnd();
                nextMonthStream.Close();
                nextMothResponse.Close();
                nextMonthReader.Close();
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

        /// <summary>
        /// 美景联动
        /// 先获取总产品数量，然后循环建立线程获取每页数据。
        /// </summary>
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
            var totalCount = Convert.ToInt32(itemCount);
            var pageSize = 255;
            var pageCount = totalCount / pageSize;
            if (totalCount % pageSize > 0)
                pageCount++;
            var mjldBll = new MJLDProducts();
            mjldBll.SetProductState();
            var getUts = new List<Thread>();
            for (var i = 1; i <= pageCount; i++)
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
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        private void SendMail()
        {
            var strTxt = "";
            var log = new List<logs>();
            var logBll = new BLL.logs();
            log = logBll.GetModelList("log_state is null");

            if ((log.Any(x => x.log_category == 0)))
            {
                strTxt += "【已下架的产品】：<a style=\"color:Red;text-decoration:none;text-decoration:none;cursor:default;\">对接产品可能已更换。</a>\r\n";
                strTxt = log.Where(x => x.log_category == 0).Aggregate(strTxt, (current, logModel) => current + (logModel.related_product + logModel.log_text.Substring(0, logModel.log_text.IndexOf("】") + 1) + "\r\n"));
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
                strTxt += "【分销价设置错误】：<a style=\"color:Red;text-decoration:none;text-decoration:none;cursor:default;\">对接产品可能已更换。</a>\r\n";
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
            if (log.Any(x => x.log_category == 20))
            {
                strTxt += "【未绑定景区】：\r\n";
                strTxt = log.Where(x => x.log_category == 20).Aggregate(strTxt, (current, logModel) => current + (logModel.related_product += "," + logModel.log_text + "\r\n"));
                strTxt += "\r\n";
            }
            if (log.Any(x => x.log_category == 22))
            {
                strTxt += "【需要身份证】：\r\n";
                strTxt = log.Where(x => x.log_category == 22).Aggregate(strTxt, (current, logModel) => current + (logModel.related_product += "," + logModel.log_text + "\r\n"));
                strTxt += "\r\n";
            }

            logBll.SetLogState();

            //获取最新的产品并保存到文本文件
            GetNewProducts();
            strTxt = strTxt.Replace("\r\n", "\r\n<br />");
            strTxt = strTxt.Replace("【产品编号】：", "");
            strTxt = strTxt.Replace("【产品名称】：", "");
            strTxt = strTxt.Replace("需要身份证号。", "。");

            string attachments = "";
            attachments = "yuanfan.txt;xiexie.txt;meijing.txt;dadong.txt;tongcheng.txt";
            var mail = new SendMail();
            mail.Send("<p style=\"color:Red;font-size:24px;\">附件为各个系统最新的产品</p>\r\n" + strTxt, "2405192532@qq.com;499006486@qq.com", attachments);
            //mail.Send("<p style=\"color:Red;font-size:24px;\">附件为各个系统最新的产品</p>\r\n" + strTxt, "", attachments);
        }

        public string GetMd5(string str)
        {
            var md5 = new MD5CryptoServiceProvider();
            var md5bytes = UTF8Encoding.Default.GetBytes(str);
            var output = md5.ComputeHash(md5bytes);
            str = BitConverter.ToString(output, 4, 8);
            str = str.Replace("-", "").ToLower();
            return str;
        }

        /// <summary>
        /// 远帆票务
        /// </summary>
        public void GetYuanFanProducts()
        {
            var yfBll = new YFPWProducts();
            yfBll.SetProductState();
            var requestData = "";
            requestData = "{\"Head\":{\"account\":\"亨亨票务\",\"SequenceId\":\"sid\",\"sign\":\"md5sign\",\"TimeStamp\":\"时间戳\",\"organization\":\"323\",\"method\":\"GetProduct\"},\"Body\":{";
            requestData += "\"Data\": \"ALL\"}";
            requestData = requestData.Replace("时间戳", DateTime.Now.ToString("yyyyMMddHHmmss"));
            var rnd = new Random();
            var sid = rnd.Next(10000000, 99999999).ToString();
            requestData = requestData.Replace("sid", sid);
            requestData = requestData.Replace("md5sign", GetMd5("323" + sid + DateTime.Now.ToString("yyyyMMddHHmmss") + "D32B6CDBA001D645A6A346DA"));
            requestData = TCodeServiceCrypt.Encrypt3DESToBase64(requestData, "D70A690DD21ADD12DA1101A2");
            var url = "http://pw.yfpw.cn/yfinterface/yfInterface.ashx";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ProtocolVersion = HttpVersion.Version11;

            request.KeepAlive = true;
            request.Headers.Add("Accept-Language", "zh-cn");
            request.Accept = "*/*";
            request.Method = "POST";
            var b = Encoding.UTF8.GetBytes(requestData);
            request.ContentLength = b.Length;
            request.ContentType = "application/x-www-form-urlencoded";
            using (var sw = request.GetRequestStream())
            {
                sw.Write(b, 0, b.Length);
            }

            using (var response = request.GetResponse() as HttpWebResponse)
            {
                if (response != null)
                    using (var sr = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("gb2312")))
                    {
                        var data = sr.ReadToEnd();
                        data = TCodeServiceCrypt.Decrypt3DESFromBase64(data, "D70A690DD21ADD12DA1101A2");
                        data = data.Substring(data.IndexOf("\"Body\":[") + 7);
                        data = data.Substring(0, data.Length - 1);
                        data = data.Replace("]}", "\"}");
                        data = data.Replace("product_imgs\":[", "product_imgs\":\"");
                        var list = JsonConvert.DeserializeObject<List<Model.YFPWProducts>>(data);
                        foreach (var model in list)
                        {
                            model.product_Introduction = "";
                            if (yfBll.GetModelByProductId(Convert.ToInt32(model.product_id)) != null)
                            {
                                model.id = yfBll.GetModelByProductId(Convert.ToInt32(model.product_id)).id;
                                yfBll.Update(model);
                            }
                            else
                            {
                                yfBll.Add(model);
                            }
                        }
                        sr.Close();
                        response.Close();
                    }
            }

            var deletedList = yfBll.GetModelList("updated=0");
            foreach (var model in deletedList)
            {
                yfBll.Delete(model.id);
            }
        }

        public void GetDadongProduct()
        {
            var pageIndex = 1;
            var bll = new DadongProducts();
            bll.SetProductState();
            while (true)
            {
                var url = "http://www.ddrtty.net/bjskiService.action?xml=";
                url += "<business_trans><request_type>getProduct_request</request_type><organization>2014092014520877</organization><pageIndex>" + pageIndex + "</pageIndex></business_trans>";
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.KeepAlive = false;
                var response = (HttpWebResponse)request.GetResponse();
                var stream = response.GetResponseStream();
                var sr = new StreamReader(stream, Encoding.GetEncoding("gb2312"));
                var data = sr.ReadToEnd();
                if (data.IndexOf("</response_type><products></products></business_trans>") >= 0)
                {
                    break;
                }

                data = data.Substring(data.IndexOf("<products>"));
                data = data.Substring(0, data.IndexOf("</business_trans>"));
                data = data.Replace("products", "ArrayOfDadongProducts");
                data = data.Replace("<product>", "<DadongProducts>");
                data = data.Replace("</product>", "</DadongProducts>");
                data = "<?xml version=\"1.0\"?>" + data;
                data = data.Replace("<ArrayOfDadongProducts>", "<ArrayOfDadongProducts xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">");
                data = data.Replace("null", "0");
                data = data.Replace("\n", "");
                data = data.Replace("\r", "");
                data = data.Replace("&nbsp;", "");
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("<[\u4e00-\u9fbb]");
                while (regex.IsMatch(data))
                {
                    data = data.Replace(regex.Match(data).ToString(), regex.Match(data).ToString().Replace("<", ""));
                }
                regex = new System.Text.RegularExpressions.Regex("[\u4e00-\u9fbb]>");
                while (regex.IsMatch(data))
                {
                    data = data.Replace(regex.Match(data).ToString(), regex.Match(data).ToString().Replace(">", ""));
                }
                var reader = new StringReader(data);
                //string msg = data.Substring(13460, 200);
                //MessageBox.Show(msg);
                var xmlSerializer = new XmlSerializer(typeof(List<Model.DadongProducts>));
                var list = xmlSerializer.Deserialize(reader) as List<Model.DadongProducts>;

                sr.Close();
                stream.Close();
                response.Close();
                reader.Close();

                foreach (var model in list)
                {
                    model.updated = 1;
                    var iProductId = bll.GetModelId(Convert.ToInt32(model.product_id));
                    if (iProductId <= 0)
                    {
                        bll.Add(model);
                    }
                    else
                    {
                        model.id = iProductId;
                        bll.Add(model);
                    }
                }
                pageIndex++;
            }
            var listOfDaDongDeleted = bll.GetModelList("updated=0");
            foreach (var model in listOfDaDongDeleted)
            {
                bll.Delete(model.id);
            }
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);

        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Ansi)]
        static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)]string lpFileName);

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("GetUrl.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true, EntryPoint = "Perform")]
        static extern string Perform(string url, string cookie, string host, string getUrl, string referer);
        // For Windows Mobile, replace user32.dll with coredll.dll
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);


        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 99:
                    {
                        //SetTextValue(Marshal.PtrToStringAnsi(m.WParam));
                        string text = Marshal.PtrToStringAnsi(m.WParam);
                        richTextBox1.Text += text;
                        richTextBox1.Text += Environment.NewLine;
                        richTextBox1.SelectionStart = richTextBox1.TextLength;
                        richTextBox1.ScrollToCaret();
                        Marshal.FreeHGlobal(m.WParam);
                        break;
                    }

                default:
                    {
                        base.WndProc(ref m);
                        break;
                    }
            }

        }
        //public delegate string Perform(string url, string cookie, string host, string getUrl, string referer);
        private void button6_Click(object sender, EventArgs e)
        {
            IntPtr pHandle = FindWindow(null, "产品同步系统");

            PostMessage(pHandle, 99, Marshal.StringToHGlobalAnsi("text"), IntPtr.Zero);
        }

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool InternetSetCookie(string lpszUrl, string lpszCookieName, string lpszCookieData);
        [DllImport("KERNEL32.DLL ")]
        public static extern IntPtr CreateToolhelp32Snapshot(uint flags, uint processid);
        [DllImport("KERNEL32.DLL ")]
        public static extern int CloseHandle(IntPtr handle);
        [DllImport("KERNEL32.DLL ")]
        public static extern int Process32First(IntPtr handle, ref   ProcessEntry32 pe);
        [DllImport("KERNEL32.DLL ")]
        public static extern int Process32Next(IntPtr handle, ref   ProcessEntry32 pe);
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int processAccess, bool bInheritHandle, uint processId);
        [DllImport("psapi.dll", SetLastError = true)]
        public static extern bool GetProcessMemoryInfo(IntPtr hProcess, out PROCESS_MEMORY_COUNTERS Memcounters, int size);
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool OpenProcessToken(IntPtr ProcessHandle,
            UInt32 DesiredAccess, out IntPtr TokenHandle);
        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool LookupPrivilegeValue(string lpSystemName, string lpName,
            out long lpLuid);
        // Use this signature if you do not want the previous state
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AdjustTokenPrivileges(IntPtr TokenHandle,
          [MarshalAs(UnmanagedType.Bool)]bool DisableAllPrivileges,
          ref TokPriv1Luid NewState,
          UInt32 Zero,
          IntPtr Null1,
          IntPtr Null2);


        /// <summary>
        /// 谢谢网产品获取
        /// Cookie抓取总是失效，使用WebBrowser抓取。
        /// </summary>
        public void GetXieProducts()
        {
            var webBrowser1 = new WebBrowser();
            var cookieContainer = new CookieContainer();
            InternetSetCookie("http://www.xiexie.com.cn", "user", "firebirdwz-hbbw2014");
            webBrowser1.Navigate("http://www.xiexie.com.cn");
            while (webBrowser1.ReadyState != WebBrowserReadyState.Complete)
            {
                Application.DoEvents();
            }
            for (var i = 0; i < 10000000; i++)
            {
                Application.DoEvents();
            }
            var merchBll = new XieProducts();
            var pageIndex = 0;
            while (true)
            {
                var url = "http://www.xiexie.com.cn/disManage/allMerch.action?state=0&putaway=1&pageNo=";
                pageIndex++;
                webBrowser1.Navigate(url + pageIndex.ToString());
                while (webBrowser1.ReadyState != WebBrowserReadyState.Complete)
                {
                    Application.DoEvents();
                }
                var seperators = new string[] { "</tr><tr>" };
                var data = webBrowser1.DocumentText;
                if (data.IndexOf("<th>操作</th>") <= 0)
                {
                    break;
                }
                data = data.Substring(data.IndexOf("<th>操作</th>"));
                data = data.Substring(data.IndexOf("</tr>"));
                data = data.Substring(0, data.IndexOf("</table>"));
                data = data.Replace("\r\n", "");
                data = data.Replace("\t", "");
                if (data.IndexOf("action?merchID") < 0)
                {
                    break;
                }
                foreach (var oneColumn in data.Split(seperators, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (oneColumn.IndexOf("action?merchID") < 0) continue;
                    string effectiveDate = "", stopSellDate = "", marketPrice = "", advisePrice = "", agentPrice = "", productId = "";
                    var productName = oneColumn.Substring(oneColumn.IndexOf("action?merchID="));
                    productName = productName.Substring(productName.IndexOf(">") + 1);
                    productName = productName.Substring(0, productName.IndexOf("</a>"));
                    effectiveDate = oneColumn.Substring(oneColumn.IndexOf("+</a></td>") + 6);
                    effectiveDate = effectiveDate.Substring(effectiveDate.IndexOf("至") + 1);
                    stopSellDate = effectiveDate;
                    effectiveDate = effectiveDate.Substring(0, effectiveDate.IndexOf("</td>"));
                    stopSellDate = stopSellDate.Substring(stopSellDate.IndexOf("</td><td>") + 9);
                    marketPrice = stopSellDate;
                    stopSellDate = stopSellDate.Substring(0, stopSellDate.IndexOf("</td>"));
                    marketPrice = marketPrice.Substring(marketPrice.IndexOf("</td><td>") + 9);
                    advisePrice = marketPrice;
                    marketPrice = marketPrice.Substring(0, marketPrice.IndexOf("</td>"));
                    advisePrice = advisePrice.Substring(advisePrice.IndexOf("</td><td>") + 9);
                    agentPrice = advisePrice;
                    advisePrice = advisePrice.Substring(0, advisePrice.IndexOf("</td>"));
                    agentPrice = agentPrice.Replace(" class=\"cf00\"", "");
                    productId = agentPrice;
                    agentPrice = agentPrice.Substring(agentPrice.IndexOf("</td><td>") + 9);
                    agentPrice = agentPrice.Substring(0, agentPrice.IndexOf("</td>"));
                    productId = productId.Substring(productId.IndexOf("copyRight") + 10);
                    productId = productId.Substring(0, productId.IndexOf(")"));
                    if (merchBll.GetModelByProductId(Convert.ToInt32(productId)) != null)
                    {
                        var newProduct = merchBll.GetModelByProductId(Convert.ToInt32(productId));
                        newProduct.MerchName = productName;
                        newProduct.ValidTime = effectiveDate.Replace("/td><td>", "");
                        newProduct.MerchDownTime = stopSellDate;
                        newProduct.SellPrice = agentPrice.Replace("元", "");
                        newProduct.CostPrice = marketPrice.Replace("元", "");
                        newProduct.AdvisePrice = advisePrice.Replace("元", "");
                        newProduct.Updated = 1;
                        merchBll.Update(newProduct);
                    }
                    else
                    {
                        var newProduct = new Model.XieProducts();
                        newProduct.MerchName = productName;
                        newProduct.ValidTime = effectiveDate.Replace("/td><td>", "");
                        newProduct.MerchDownTime = stopSellDate;
                        newProduct.SellPrice = agentPrice.Replace("元", "");
                        newProduct.CostPrice = marketPrice.Replace("元", "");
                        newProduct.AdvisePrice = advisePrice.Replace("元", "");
                        newProduct.Updated = 1;
                        newProduct.MerchID = Convert.ToInt32(productId);
                        merchBll.Add(newProduct);
                    }
                }
            }
            webBrowser1.Dispose();
        }

        public void GetNewProducts()
        {
            var fs = new FileStream("tongcheng.txt", FileMode.Create);
            var sw = new StreamWriter(fs);
            var tickets = new Tickets();
            List<Model.Tickets> ticketsList = tickets.GetModelList("ticketPriceId not in(select cast(replace(interfaceProdId,'	','') as int) from ProductList where interfaceId=10000218)");
            foreach (var model in ticketsList)
            {
                sw.Write(model.sceneryName + "\r\n");
            }
            sw.Flush();
            sw.Close();
            fs.Close();



            fs = new FileStream("yuanfan.txt", FileMode.Create);
            sw = new StreamWriter(fs);
            var yfpw = new BLL.YFPWProducts();
            List<Model.YFPWProducts> yfpwList = yfpw.GetModelList("product_id not in(select cast(replace(interfaceProdId,'	','') as int) from ProductList where interfaceId=10000184)");
            foreach (var model in yfpwList)
            {
                sw.Write(model.scenic_name + "\r\n");
            }
            sw.Flush();
            sw.Close();
            fs.Close();


            fs = new FileStream("meijing.txt", FileMode.Create);
            sw = new StreamWriter(fs);
            var mjld = new MJLDProducts();
            List<Model.MJLDProducts> mjldList = mjld.GetModelList("Id not in(select cast(replace(interfaceProdId,'	','') as int) from ProductList where interfaceId=10000210)");
            foreach (var model in mjldList)
            {
                sw.Write(model.Name + "\r\n");
            }
            sw.Flush();
            sw.Close();
            fs.Close();



            fs = new FileStream("dadong.txt", FileMode.Create);
            sw = new StreamWriter(fs);
            var dadong = new BLL.DadongProducts();
            List<Model.DadongProducts> dadongList = dadong.GetModelList("product_id not in(select cast(replace(interfaceProdId,'	','') as int) from ProductList where interfaceId=10000212)");
            foreach (var model in dadongList)
            {
                sw.Write(model.product_name + "\r\n");
            }
            sw.Flush();
            sw.Close();
            fs.Close();


            fs = new FileStream("xiexie.txt", FileMode.Create);
            sw = new StreamWriter(fs);
            var xiexie = new BLL.XieProducts();
            List<Model.XieProducts> xieList = xiexie.GetModelList("MerchID not in(select cast(replace(interfaceProdId,'	','') as int) from ProductList where interfaceId=10000238)");
            foreach (var model in xieList)
            {
                sw.Write(model.MerchName + "\r\n");
            }
            sw.Flush();
            sw.Close();
            fs.Close();

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            IntPtr handle = CreateToolhelp32Snapshot(0x2, 0);
            List<ProcessEntry32> list = new List<ProcessEntry32>();
            if ((int)handle > 0)
            {
                ProcessEntry32 pe32 = new ProcessEntry32();
                pe32.dwSize = (uint)Marshal.SizeOf(pe32);
                int bMore = Process32First(handle, ref pe32);
                while (bMore == 1)
                {
                    IntPtr temp = Marshal.AllocHGlobal((int)pe32.dwSize);
                    Marshal.StructureToPtr(pe32, temp, true);
                    ProcessEntry32 pe = (ProcessEntry32)Marshal.PtrToStructure(temp, typeof(ProcessEntry32));
                    Marshal.FreeHGlobal(temp);
                    list.Add(pe);
                    bMore = Process32Next(handle, ref pe32);
                }

                IntPtr hToken;
                TokPriv1Luid tp;
                if (!OpenProcessToken(GetCurrentProcess(), 0x00000020 | 0x00000008, out hToken))
                {
                    SetTextValue("打开进程token失败。");
                }
                tp.Count = 1;
                tp.Luid = 0;
                tp.Attr = 0x00000002;
                if (!LookupPrivilegeValue(null, "SeDebugPrivilege", out tp.Luid))
                {
                    SetTextValue("LookupPrivilegeValue false");
                    CloseHandle(hToken);
                    return;
                }
                if (!AdjustTokenPrivileges(hToken, false, ref tp, (uint)Marshal.SizeOf(typeof(TokPriv1Luid)), IntPtr.Zero, IntPtr.Zero))
                {
                    SetTextValue("AdjustTokenPrivileges failed");
                    CloseHandle(hToken);
                    return;
                }

                CloseHandle(handle);
                foreach (ProcessEntry32 p in list)
                {
                    IntPtr pHandle = OpenProcess(0x1F0FFF, false, p.th32ProcessID);
                    PROCESS_MEMORY_COUNTERS pmc;

                    GetProcessMemoryInfo(pHandle, out pmc, Marshal.SizeOf(typeof(PROCESS_MEMORY_COUNTERS)));
                    //if (pmc.WorkingSetSize / 1024 / 1024 > 10)
                    SetProcessWorkingSetSize(pHandle, -1, -1);
                    SetTextValue(p.szExeFile + "\t" + (pmc.WorkingSetSize).ToString());
                }
            }
        }

        public void GetProductDetail()
        {
            List<ProductList> list = new List<ProductList>();
            FxProductMonitor.BLL.ProductList productDetailBll = new BLL.ProductList();
            list = productDetailBll.GetModelList("");
            string url = "http://fx.henghengw.net/api/detail.jsp?custId=233286&apikey=8411B4D5B6A600FCB6FBD217E3DD2E21";
            FxProductMonitor.BLL.EasyApi easy = new EasyApi();
            Thread[] getProductThread = new Thread[list.Count];
            int i = 0;
            foreach (var model in list)
            {
                if (string.IsNullOrEmpty(model.viewName) || model.viewName == "2014-01-01")
                {
                    logs logModel = new logs();
                    logModel.related_product = "【产品编号】：" + model.productNo.ToString() + "，【产品名称】" + model.productName;
                    logModel.log_text = "未绑定景区";
                    logModel.log_date = DateTime.Now;
                    logModel.log_category = 20;
                    FxProductMonitor.BLL.logs logBll = new BLL.logs();
                    logBll.Add(logModel);
                    continue;
                }
                FxProductMonitor.BLL.GetProductDetail de = new BLL.GetProductDetail();
                de.model = model;
                getProductThread[i] = new Thread(new ThreadStart(de.GetCustFiled));
                getProductThread[i].Start();
                i++;
                //string productData = easy.Perform("http://fx.henghengw.net", "", "fx.henghengw.net", tempUrl.Replace("http://fx.henghengw.net", ""), "http://fx.henghengw.net");
            }

            foreach (var t in getProductThread)
            {
                while (t != null && t.ThreadState != ThreadState.Stopped)
                {
                    Application.DoEvents();
                }
            }
        }


    }
}
