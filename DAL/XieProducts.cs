/**  版本信息模板在安装目录下，可自行修改。
* XieProducts.cs
*
* 功 能： N/A
* 类 名： XieProducts
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015-03-24 13:49:30   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace FxProductMonitor.DAL
{
    /// <summary>
    /// 数据访问类:XieProducts
    /// </summary>
    public partial class XieProducts
    {
        public XieProducts()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("id", "XieProducts");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from XieProducts");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(FxProductMonitor.Model.XieProducts model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into XieProducts(");
            strSql.Append("MerchID,MerchName,IsRealName,IsYuyueDay,SortID,SellPrice,ValidTime,MerchDownTime,Address,ServiceTel,CostPrice,AdvisePrice,BuyPrompt,ProductDetail,Updated)");
            strSql.Append(" values (");
            strSql.Append("@MerchID,@MerchName,@IsRealName,@IsYuyueDay,@SortID,@SellPrice,@ValidTime,@MerchDownTime,@Address,@ServiceTel,@CostPrice,@AdvisePrice,@BuyPrompt,@ProductDetail,@Updated)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@MerchID", SqlDbType.Int,4),
					new SqlParameter("@MerchName", SqlDbType.VarChar,255),
					new SqlParameter("@IsRealName", SqlDbType.VarChar,50),
					new SqlParameter("@IsYuyueDay", SqlDbType.VarChar,50),
					new SqlParameter("@SortID", SqlDbType.VarChar,50),
					new SqlParameter("@SellPrice", SqlDbType.VarChar,50),
					new SqlParameter("@ValidTime", SqlDbType.VarChar,50),
					new SqlParameter("@MerchDownTime", SqlDbType.VarChar,50),
					new SqlParameter("@Address", SqlDbType.VarChar,1000),
					new SqlParameter("@ServiceTel", SqlDbType.VarChar,50),
					new SqlParameter("@CostPrice", SqlDbType.VarChar,50),
					new SqlParameter("@AdvisePrice", SqlDbType.VarChar,50),
					new SqlParameter("@BuyPrompt", SqlDbType.VarChar,2000),
					new SqlParameter("@ProductDetail", SqlDbType.VarChar,2000),
					new SqlParameter("@Updated", SqlDbType.Int,4)};
            parameters[0].Value = model.MerchID;
            parameters[1].Value = model.MerchName;
            parameters[2].Value = model.IsRealName;
            parameters[3].Value = model.IsYuyueDay;
            parameters[4].Value = model.SortID;
            parameters[5].Value = model.SellPrice;
            parameters[6].Value = model.ValidTime;
            parameters[7].Value = model.MerchDownTime;
            parameters[8].Value = model.Address;
            parameters[9].Value = model.ServiceTel;
            parameters[10].Value = model.CostPrice;
            parameters[11].Value = model.AdvisePrice;
            parameters[12].Value = model.BuyPrompt;
            parameters[13].Value = model.ProductDetail;
            parameters[14].Value = model.Updated;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(FxProductMonitor.Model.XieProducts model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update XieProducts set ");
            strSql.Append("MerchID=@MerchID,");
            strSql.Append("MerchName=@MerchName,");
            strSql.Append("IsRealName=@IsRealName,");
            strSql.Append("IsYuyueDay=@IsYuyueDay,");
            strSql.Append("SortID=@SortID,");
            strSql.Append("SellPrice=@SellPrice,");
            strSql.Append("ValidTime=@ValidTime,");
            strSql.Append("MerchDownTime=@MerchDownTime,");
            strSql.Append("Address=@Address,");
            strSql.Append("ServiceTel=@ServiceTel,");
            strSql.Append("CostPrice=@CostPrice,");
            strSql.Append("AdvisePrice=@AdvisePrice,");
            strSql.Append("BuyPrompt=@BuyPrompt,");
            strSql.Append("ProductDetail=@ProductDetail,");
            strSql.Append("Updated=@Updated");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@MerchID", SqlDbType.Int,4),
					new SqlParameter("@MerchName", SqlDbType.VarChar,255),
					new SqlParameter("@IsRealName", SqlDbType.VarChar,50),
					new SqlParameter("@IsYuyueDay", SqlDbType.VarChar,50),
					new SqlParameter("@SortID", SqlDbType.VarChar,50),
					new SqlParameter("@SellPrice", SqlDbType.VarChar,50),
					new SqlParameter("@ValidTime", SqlDbType.VarChar,50),
					new SqlParameter("@MerchDownTime", SqlDbType.VarChar,50),
					new SqlParameter("@Address", SqlDbType.VarChar,1000),
					new SqlParameter("@ServiceTel", SqlDbType.VarChar,50),
					new SqlParameter("@CostPrice", SqlDbType.VarChar,50),
					new SqlParameter("@AdvisePrice", SqlDbType.VarChar,50),
					new SqlParameter("@BuyPrompt", SqlDbType.VarChar,2000),
					new SqlParameter("@ProductDetail", SqlDbType.VarChar,2000),
					new SqlParameter("@Updated", SqlDbType.Int,4),
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.MerchID;
            parameters[1].Value = model.MerchName;
            parameters[2].Value = model.IsRealName;
            parameters[3].Value = model.IsYuyueDay;
            parameters[4].Value = model.SortID;
            parameters[5].Value = model.SellPrice;
            parameters[6].Value = model.ValidTime;
            parameters[7].Value = model.MerchDownTime;
            parameters[8].Value = model.Address;
            parameters[9].Value = model.ServiceTel;
            parameters[10].Value = model.CostPrice;
            parameters[11].Value = model.AdvisePrice;
            parameters[12].Value = model.BuyPrompt;
            parameters[13].Value = model.ProductDetail;
            parameters[14].Value = model.Updated;
            parameters[15].Value = model.id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from XieProducts ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from XieProducts ");
            strSql.Append(" where id in (" + idlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public FxProductMonitor.Model.XieProducts GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,MerchID,MerchName,IsRealName,IsYuyueDay,SortID,SellPrice,ValidTime,MerchDownTime,Address,ServiceTel,CostPrice,AdvisePrice,BuyPrompt,ProductDetail,Updated from XieProducts ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            FxProductMonitor.Model.XieProducts model = new FxProductMonitor.Model.XieProducts();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public FxProductMonitor.Model.XieProducts DataRowToModel(DataRow row)
        {
            FxProductMonitor.Model.XieProducts model = new FxProductMonitor.Model.XieProducts();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["MerchID"] != null && row["MerchID"].ToString() != "")
                {
                    model.MerchID = int.Parse(row["MerchID"].ToString());
                }
                if (row["MerchName"] != null)
                {
                    model.MerchName = row["MerchName"].ToString();
                }
                if (row["IsRealName"] != null)
                {
                    model.IsRealName = row["IsRealName"].ToString();
                }
                if (row["IsYuyueDay"] != null)
                {
                    model.IsYuyueDay = row["IsYuyueDay"].ToString();
                }
                if (row["SortID"] != null)
                {
                    model.SortID = row["SortID"].ToString();
                }
                if (row["SellPrice"] != null)
                {
                    model.SellPrice = row["SellPrice"].ToString();
                }
                if (row["ValidTime"] != null)
                {
                    model.ValidTime = row["ValidTime"].ToString();
                }
                if (row["MerchDownTime"] != null)
                {
                    model.MerchDownTime = row["MerchDownTime"].ToString();
                }
                if (row["Address"] != null)
                {
                    model.Address = row["Address"].ToString();
                }
                if (row["ServiceTel"] != null)
                {
                    model.ServiceTel = row["ServiceTel"].ToString();
                }
                if (row["CostPrice"] != null)
                {
                    model.CostPrice = row["CostPrice"].ToString();
                }
                if (row["AdvisePrice"] != null)
                {
                    model.AdvisePrice = row["AdvisePrice"].ToString();
                }
                if (row["BuyPrompt"] != null)
                {
                    model.BuyPrompt = row["BuyPrompt"].ToString();
                }
                if (row["ProductDetail"] != null)
                {
                    model.ProductDetail = row["ProductDetail"].ToString();
                }
                if (row["Updated"] != null && row["Updated"].ToString() != "")
                {
                    model.Updated = int.Parse(row["Updated"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,MerchID,MerchName,IsRealName,IsYuyueDay,SortID,SellPrice,ValidTime,MerchDownTime,Address,ServiceTel,CostPrice,AdvisePrice,BuyPrompt,ProductDetail,Updated ");
            strSql.Append(" FROM XieProducts ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" id,MerchID,MerchName,IsRealName,IsYuyueDay,SortID,SellPrice,ValidTime,MerchDownTime,Address,ServiceTel,CostPrice,AdvisePrice,BuyPrompt,ProductDetail,Updated ");
            strSql.Append(" FROM XieProducts ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM XieProducts ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.id desc");
            }
            strSql.Append(")AS Row, T.*  from XieProducts T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "XieProducts";
            parameters[1].Value = "id";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/


        public FxProductMonitor.Model.XieProducts GetModelByProductId(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,MerchID,MerchName,IsRealName,IsYuyueDay,SortID,SellPrice,ValidTime,MerchDownTime,Address,ServiceTel,CostPrice,AdvisePrice,BuyPrompt,ProductDetail,Updated from XieProducts ");
            strSql.Append(" where MerchID=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int)
			};
            parameters[0].Value = id;

            FxProductMonitor.Model.XieProducts model = new FxProductMonitor.Model.XieProducts();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        public void SetProductState()
        {
            string sql = "update XieProducts set Updated=0";
            try
            {
                DbHelperSQL.ExecuteSql(sql);
            }
            catch
            {

            }
        }
        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

