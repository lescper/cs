using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using FxProductMonitor.BLL;
using System.Xml.Serialization;

namespace FxProductMonitor.BLL
{
    public class Uts
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }

        readonly MJLDProducts _bll = new MJLDProducts();
        public void Get()
        {
            var model = new FxProductMonitor.Model.MJLDProductRequest
            {
                AreaId = 0,
                AreaName = "",
                goodsName = "",
                PageIndex = PageIndex,
                PageSize = PageSize,
                password = "a1s2d3",
                SaleType = 1,
                ThemeId = 0,
                timeStamp =
                    Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds).ToString(CultureInfo.InvariantCulture),
                user = "liuyang"
            };
            var request = (HttpWebRequest)WebRequest.Create("http://outer.mjld.com.cn/Outer/Interface/SelectProductList");
            request.Method = "POST";
            byte[] byteData = TCodeServiceCrypt.GetPostData(model);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteData.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteData, 0, byteData.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            dataStream = response.GetResponseStream();
            var sr = new StreamReader(dataStream);
            string data = sr.ReadToEnd();
            sr.Close();
            response.Close();
            dataStream.Close();
            data = TCodeServiceCrypt.Decrypt3DESFromBase64(data, TCodeServiceCrypt.keyStr);
            data = data.Substring(data.IndexOf("<Products>"));
            data = data.Substring(0, data.IndexOf("<status>"));
            data = data.Replace("<Product>", "<MJLDProducts>");
            data = "<?xml version=\"1.0\"?><ArrayOfMJLDProducts xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">" + data;
            data = data.Replace("</Product>", "</MJLDProducts>");
            data = data.Replace("<Products>", "");
            data = data.Replace("</Products>", "</ArrayOfMJLDProducts>");

            StringReader reader = new StringReader(data);
            var serializer = new XmlSerializer(typeof(List<Model.MJLDProducts>));
            var list = serializer.Deserialize(reader) as List<Model.MJLDProducts>;
            if (list != null)
                foreach (var mj in list)
                {
                    mj.Updated = 1;
                    int recordCount = _bll.GetRecordCount("Id=" + mj.Id);
                    if (recordCount <= 0)
                    {
                        _bll.Add(mj);
                    }
                    else
                    {
                        mj.mj_id = _bll.GetProductId(mj.Id.ToString());
                        if (mj.mj_id <= 0)
                        {
                            throw (new Exception("美景联动产品编号错误。"));
                        }
                        _bll.Update(mj);
                    }
                }
        }
    }
}
