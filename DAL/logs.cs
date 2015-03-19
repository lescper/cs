/**  版本信息模板在安装目录下，可自行修改。
* logs.cs
*
* 功 能： N/A
* 类 名： logs
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015-03-19 9:10:16   N/A    初版
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
    /// 数据访问类:logs
    /// </summary>
    public partial class logs
    {
        public logs()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("id", "logs");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from logs");
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
        public int Add(FxProductMonitor.Model.logs model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into logs(");
            strSql.Append("related_product,log_text,log_date,log_category,log_state)");
            strSql.Append(" values (");
            strSql.Append("@related_product,@log_text,@log_date,@log_category,@log_state)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@related_product", SqlDbType.VarChar,255),
					new SqlParameter("@log_text", SqlDbType.Text),
					new SqlParameter("@log_date", SqlDbType.DateTime),
					new SqlParameter("@log_category", SqlDbType.Int,4),
					new SqlParameter("@log_state", SqlDbType.Int,4)};
            parameters[0].Value = model.related_product;
            parameters[1].Value = model.log_text;
            parameters[2].Value = model.log_date;
            parameters[3].Value = model.log_category;
            parameters[4].Value = model.log_state;

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
        public bool Update(FxProductMonitor.Model.logs model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update logs set ");
            strSql.Append("related_product=@related_product,");
            strSql.Append("log_text=@log_text,");
            strSql.Append("log_date=@log_date,");
            strSql.Append("log_category=@log_category,");
            strSql.Append("log_state=@log_state");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@related_product", SqlDbType.VarChar,255),
					new SqlParameter("@log_text", SqlDbType.Text),
					new SqlParameter("@log_date", SqlDbType.DateTime),
					new SqlParameter("@log_category", SqlDbType.Int,4),
					new SqlParameter("@log_state", SqlDbType.Int,4),
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.related_product;
            parameters[1].Value = model.log_text;
            parameters[2].Value = model.log_date;
            parameters[3].Value = model.log_category;
            parameters[4].Value = model.log_state;
            parameters[5].Value = model.id;

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
            strSql.Append("delete from logs ");
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
            strSql.Append("delete from logs ");
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
        public FxProductMonitor.Model.logs GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,related_product,log_text,log_date,log_category,log_state from logs ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            FxProductMonitor.Model.logs model = new FxProductMonitor.Model.logs();
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
        public FxProductMonitor.Model.logs DataRowToModel(DataRow row)
        {
            FxProductMonitor.Model.logs model = new FxProductMonitor.Model.logs();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["related_product"] != null)
                {
                    model.related_product = row["related_product"].ToString();
                }
                if (row["log_text"] != null)
                {
                    model.log_text = row["log_text"].ToString();
                }
                if (row["log_date"] != null && row["log_date"].ToString() != "")
                {
                    model.log_date = DateTime.Parse(row["log_date"].ToString());
                }
                if (row["log_category"] != null && row["log_category"].ToString() != "")
                {
                    model.log_category = int.Parse(row["log_category"].ToString());
                }
                if (row["log_state"] != null && row["log_state"].ToString() != "")
                {
                    model.log_state = int.Parse(row["log_state"].ToString());
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
            strSql.Append("select id,related_product,log_text,log_date,log_category,log_state ");
            strSql.Append(" FROM logs ");
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
            strSql.Append(" id,related_product,log_text,log_date,log_category,log_state ");
            strSql.Append(" FROM logs ");
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
            strSql.Append("select count(1) FROM logs ");
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
            strSql.Append(")AS Row, T.*  from logs T ");
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
            parameters[0].Value = "logs";
            parameters[1].Value = "id";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod
        public int SetLogState()
        {
            string sql = "update logs set log_state=1";
            try
            {
                return DbHelperSQL.ExecuteSql(sql);
            }
            catch
            {
                return -1;
            }
        }
        #endregion  ExtensionMethod
    }
}

