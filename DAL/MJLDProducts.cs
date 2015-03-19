/**  版本信息模板在安装目录下，可自行修改。
* MJLDProducts.cs
*
* 功 能： N/A
* 类 名： MJLDProducts
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015-03-16 16:08:09   N/A    初版
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
    /// 数据访问类:MJLDProducts
    /// </summary>
    public partial class MJLDProducts
    {
        public MJLDProducts()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("mj_id", "MJLDProducts");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int mj_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from MJLDProducts");
            strSql.Append(" where mj_id=@mj_id");
            SqlParameter[] parameters = {
					new SqlParameter("@mj_id", SqlDbType.Int,4)
			};
            parameters[0].Value = mj_id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(FxProductMonitor.Model.MJLDProducts model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MJLDProducts(");
            strSql.Append("Id,Name,ProductType,SaleType,AreaName,ThemeName,Stock,GroupName,IsReserve,ActDate,RequireDate,SalesPrice,RetailPrice,SettlementPrice,Image,Details,Updated)");
            strSql.Append(" values (");
            strSql.Append("@Id,@Name,@ProductType,@SaleType,@AreaName,@ThemeName,@Stock,@GroupName,@IsReserve,@ActDate,@RequireDate,@SalesPrice,@RetailPrice,@SettlementPrice,@Image,@Details,@Updated)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.VarChar,50),
					new SqlParameter("@ProductType", SqlDbType.Int,4),
					new SqlParameter("@SaleType", SqlDbType.Int,4),
					new SqlParameter("@AreaName", SqlDbType.VarChar,255),
					new SqlParameter("@ThemeName", SqlDbType.VarChar,255),
					new SqlParameter("@Stock", SqlDbType.Int,4),
					new SqlParameter("@GroupName", SqlDbType.VarChar,50),
					new SqlParameter("@IsReserve", SqlDbType.VarChar,50),
					new SqlParameter("@ActDate", SqlDbType.VarChar,50),
					new SqlParameter("@RequireDate", SqlDbType.VarChar,50),
					new SqlParameter("@SalesPrice", SqlDbType.VarChar,50),
					new SqlParameter("@RetailPrice", SqlDbType.VarChar,50),
					new SqlParameter("@SettlementPrice", SqlDbType.VarChar,50),
					new SqlParameter("@Image", SqlDbType.VarChar,255),
					new SqlParameter("@Details", SqlDbType.VarChar,1000),
					new SqlParameter("@Updated", SqlDbType.Int,4)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.ProductType;
            parameters[3].Value = model.SaleType;
            parameters[4].Value = model.AreaName;
            parameters[5].Value = model.ThemeName;
            parameters[6].Value = model.Stock;
            parameters[7].Value = model.GroupName;
            parameters[8].Value = model.IsReserve;
            parameters[9].Value = model.ActDate;
            parameters[10].Value = model.RequireDate;
            parameters[11].Value = model.SalesPrice;
            parameters[12].Value = model.RetailPrice;
            parameters[13].Value = model.SettlementPrice;
            parameters[14].Value = model.Image;
            parameters[15].Value = model.Details;
            parameters[16].Value = model.Updated;

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
        public bool Update(FxProductMonitor.Model.MJLDProducts model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update MJLDProducts set ");
            strSql.Append("Id=@Id,");
            strSql.Append("Name=@Name,");
            strSql.Append("ProductType=@ProductType,");
            strSql.Append("SaleType=@SaleType,");
            strSql.Append("AreaName=@AreaName,");
            strSql.Append("ThemeName=@ThemeName,");
            strSql.Append("Stock=@Stock,");
            strSql.Append("GroupName=@GroupName,");
            strSql.Append("IsReserve=@IsReserve,");
            strSql.Append("ActDate=@ActDate,");
            strSql.Append("RequireDate=@RequireDate,");
            strSql.Append("SalesPrice=@SalesPrice,");
            strSql.Append("RetailPrice=@RetailPrice,");
            strSql.Append("SettlementPrice=@SettlementPrice,");
            strSql.Append("Image=@Image,");
            strSql.Append("Details=@Details,");
            strSql.Append("Updated=@Updated");
            strSql.Append(" where mj_id=@mj_id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@Name", SqlDbType.VarChar,50),
					new SqlParameter("@ProductType", SqlDbType.Int,4),
					new SqlParameter("@SaleType", SqlDbType.Int,4),
					new SqlParameter("@AreaName", SqlDbType.VarChar,255),
					new SqlParameter("@ThemeName", SqlDbType.VarChar,255),
					new SqlParameter("@Stock", SqlDbType.Int,4),
					new SqlParameter("@GroupName", SqlDbType.VarChar,50),
					new SqlParameter("@IsReserve", SqlDbType.VarChar,50),
					new SqlParameter("@ActDate", SqlDbType.VarChar,50),
					new SqlParameter("@RequireDate", SqlDbType.VarChar,50),
					new SqlParameter("@SalesPrice", SqlDbType.VarChar,50),
					new SqlParameter("@RetailPrice", SqlDbType.VarChar,50),
					new SqlParameter("@SettlementPrice", SqlDbType.VarChar,50),
					new SqlParameter("@Image", SqlDbType.VarChar,255),
					new SqlParameter("@Details", SqlDbType.VarChar,1000),
					new SqlParameter("@Updated", SqlDbType.Int,4),
					new SqlParameter("@mj_id", SqlDbType.Int,4)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.ProductType;
            parameters[3].Value = model.SaleType;
            parameters[4].Value = model.AreaName;
            parameters[5].Value = model.ThemeName;
            parameters[6].Value = model.Stock;
            parameters[7].Value = model.GroupName;
            parameters[8].Value = model.IsReserve;
            parameters[9].Value = model.ActDate;
            parameters[10].Value = model.RequireDate;
            parameters[11].Value = model.SalesPrice;
            parameters[12].Value = model.RetailPrice;
            parameters[13].Value = model.SettlementPrice;
            parameters[14].Value = model.Image;
            parameters[15].Value = model.Details;
            parameters[16].Value = model.Updated;
            parameters[17].Value = model.mj_id;

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
        public bool Delete(int mj_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from MJLDProducts ");
            strSql.Append(" where mj_id=@mj_id");
            SqlParameter[] parameters = {
					new SqlParameter("@mj_id", SqlDbType.Int,4)
			};
            parameters[0].Value = mj_id;

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
        public bool DeleteList(string mj_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from MJLDProducts ");
            strSql.Append(" where mj_id in (" + mj_idlist + ")  ");
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
        public FxProductMonitor.Model.MJLDProducts GetModel(int mj_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 mj_id,Id,Name,ProductType,SaleType,AreaName,ThemeName,Stock,GroupName,IsReserve,ActDate,RequireDate,SalesPrice,RetailPrice,SettlementPrice,Image,Details,Updated from MJLDProducts ");
            strSql.Append(" where mj_id=@mj_id");
            SqlParameter[] parameters = {
					new SqlParameter("@mj_id", SqlDbType.Int,4)
			};
            parameters[0].Value = mj_id;

            FxProductMonitor.Model.MJLDProducts model = new FxProductMonitor.Model.MJLDProducts();
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
        public FxProductMonitor.Model.MJLDProducts DataRowToModel(DataRow row)
        {
            FxProductMonitor.Model.MJLDProducts model = new FxProductMonitor.Model.MJLDProducts();
            if (row != null)
            {
                if (row["mj_id"] != null && row["mj_id"].ToString() != "")
                {
                    model.mj_id = int.Parse(row["mj_id"].ToString());
                }
                if (row["Id"] != null && row["Id"].ToString() != "")
                {
                    model.Id = int.Parse(row["Id"].ToString());
                }
                if (row["Name"] != null)
                {
                    model.Name = row["Name"].ToString();
                }
                if (row["ProductType"] != null && row["ProductType"].ToString() != "")
                {
                    model.ProductType = int.Parse(row["ProductType"].ToString());
                }
                if (row["SaleType"] != null && row["SaleType"].ToString() != "")
                {
                    model.SaleType = int.Parse(row["SaleType"].ToString());
                }
                if (row["AreaName"] != null)
                {
                    model.AreaName = row["AreaName"].ToString();
                }
                if (row["ThemeName"] != null)
                {
                    model.ThemeName = row["ThemeName"].ToString();
                }
                if (row["Stock"] != null && row["Stock"].ToString() != "")
                {
                    model.Stock = int.Parse(row["Stock"].ToString());
                }
                if (row["GroupName"] != null)
                {
                    model.GroupName = row["GroupName"].ToString();
                }
                if (row["IsReserve"] != null)
                {
                    model.IsReserve = row["IsReserve"].ToString();
                }
                if (row["ActDate"] != null)
                {
                    model.ActDate = row["ActDate"].ToString();
                }
                if (row["RequireDate"] != null)
                {
                    model.RequireDate = row["RequireDate"].ToString();
                }
                if (row["SalesPrice"] != null)
                {
                    model.SalesPrice = row["SalesPrice"].ToString();
                }
                if (row["RetailPrice"] != null)
                {
                    model.RetailPrice = row["RetailPrice"].ToString();
                }
                if (row["SettlementPrice"] != null)
                {
                    model.SettlementPrice = row["SettlementPrice"].ToString();
                }
                if (row["Image"] != null)
                {
                    model.Image = row["Image"].ToString();
                }
                if (row["Details"] != null)
                {
                    model.Details = row["Details"].ToString();
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
            strSql.Append("select mj_id,Id,Name,ProductType,SaleType,AreaName,ThemeName,Stock,GroupName,IsReserve,ActDate,RequireDate,SalesPrice,RetailPrice,SettlementPrice,Image,Details,Updated ");
            strSql.Append(" FROM MJLDProducts ");
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
            strSql.Append(" mj_id,Id,Name,ProductType,SaleType,AreaName,ThemeName,Stock,GroupName,IsReserve,ActDate,RequireDate,SalesPrice,RetailPrice,SettlementPrice,Image,Details,Updated ");
            strSql.Append(" FROM MJLDProducts ");
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
            strSql.Append("select count(1) FROM MJLDProducts ");
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
                strSql.Append("order by T.mj_id desc");
            }
            strSql.Append(")AS Row, T.*  from MJLDProducts T ");
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
            parameters[0].Value = "MJLDProducts";
            parameters[1].Value = "mj_id";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        public void SetProductState()
        {
            string sql = "Update MJLDProducts set Updated=0";
            DbHelperSQL.ExecuteSql(sql);
        }

        public int GetProductId(string id)
        {
            string sql = "select mj_id from MJLDProducts where Id=" + id;

            try
            {
                return Convert.ToInt32(DbHelperSQL.GetSingle(sql));
            }
            catch
            {
                return -1;
            }
        }

        public FxProductMonitor.Model.MJLDProducts GetModelById(int mj_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 mj_id,Id,Name,ProductType,SaleType,AreaName,ThemeName,Stock,GroupName,IsReserve,ActDate,RequireDate,SalesPrice,RetailPrice,SettlementPrice,Image,Details,Updated from MJLDProducts ");
            strSql.Append(" where Id=@mj_id");
            SqlParameter[] parameters = {
					new SqlParameter("@mj_id", SqlDbType.Int)
			};
            parameters[0].Value = mj_id;

            FxProductMonitor.Model.MJLDProducts model = new FxProductMonitor.Model.MJLDProducts();
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
        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

