/**  版本信息模板在安装目录下，可自行修改。
* Tickets.cs
*
* 功 能： N/A
* 类 名： Tickets
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015-03-13 9:56:46   N/A    初版
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
	/// 数据访问类:Tickets
	/// </summary>
	public partial class Tickets
	{
		public Tickets()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("id", "Tickets"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Tickets");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(FxProductMonitor.Model.Tickets model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Tickets(");
			strSql.Append("ticketPriceId,ticketName,effectiveBeginDate,effectiveEndDate,startSellDate,stopSellDate,exceptDate,sceneryId,sceneryName,marketPrice,tcAmountPrice,agentPrice,isRealName,isNeedIdCard,minSaleQty,maxSaleQty,reserveBeforeDays,reserveBeforeTime,reserveType,reserveTimes,reserveTotalTickets,reserveDaysLimit,updated)");
			strSql.Append(" values (");
			strSql.Append("@ticketPriceId,@ticketName,@effectiveBeginDate,@effectiveEndDate,@startSellDate,@stopSellDate,@exceptDate,@sceneryId,@sceneryName,@marketPrice,@tcAmountPrice,@agentPrice,@isRealName,@isNeedIdCard,@minSaleQty,@maxSaleQty,@reserveBeforeDays,@reserveBeforeTime,@reserveType,@reserveTimes,@reserveTotalTickets,@reserveDaysLimit,@updated)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@ticketPriceId", SqlDbType.Int,4),
					new SqlParameter("@ticketName", SqlDbType.VarChar,255),
					new SqlParameter("@effectiveBeginDate", SqlDbType.VarChar,50),
					new SqlParameter("@effectiveEndDate", SqlDbType.VarChar,50),
					new SqlParameter("@startSellDate", SqlDbType.VarChar,50),
					new SqlParameter("@stopSellDate", SqlDbType.VarChar,50),
					new SqlParameter("@exceptDate", SqlDbType.VarChar,1000),
					new SqlParameter("@sceneryId", SqlDbType.VarChar,50),
					new SqlParameter("@sceneryName", SqlDbType.VarChar,100),
					new SqlParameter("@marketPrice", SqlDbType.VarChar,50),
					new SqlParameter("@tcAmountPrice", SqlDbType.VarChar,50),
					new SqlParameter("@agentPrice", SqlDbType.VarChar,50),
					new SqlParameter("@isRealName", SqlDbType.VarChar,50),
					new SqlParameter("@isNeedIdCard", SqlDbType.VarChar,50),
					new SqlParameter("@minSaleQty", SqlDbType.Int,4),
					new SqlParameter("@maxSaleQty", SqlDbType.Int,4),
					new SqlParameter("@reserveBeforeDays", SqlDbType.Int,4),
					new SqlParameter("@reserveBeforeTime", SqlDbType.VarChar,50),
					new SqlParameter("@reserveType", SqlDbType.Int,4),
					new SqlParameter("@reserveTimes", SqlDbType.Int,4),
					new SqlParameter("@reserveTotalTickets", SqlDbType.Int,4),
					new SqlParameter("@reserveDaysLimit", SqlDbType.Int,4),
					new SqlParameter("@updated", SqlDbType.Int,4)};
			parameters[0].Value = model.ticketPriceId;
			parameters[1].Value = model.ticketName;
			parameters[2].Value = model.effectiveBeginDate;
			parameters[3].Value = model.effectiveEndDate;
			parameters[4].Value = model.startSellDate;
			parameters[5].Value = model.stopSellDate;
			parameters[6].Value = model.exceptDate;
			parameters[7].Value = model.sceneryId;
			parameters[8].Value = model.sceneryName;
			parameters[9].Value = model.marketPrice;
			parameters[10].Value = model.tcAmountPrice;
			parameters[11].Value = model.agentPrice;
			parameters[12].Value = model.isRealName;
			parameters[13].Value = model.isNeedIdCard;
			parameters[14].Value = model.minSaleQty;
			parameters[15].Value = model.maxSaleQty;
			parameters[16].Value = model.reserveBeforeDays;
			parameters[17].Value = model.reserveBeforeTime;
			parameters[18].Value = model.reserveType;
			parameters[19].Value = model.reserveTimes;
			parameters[20].Value = model.reserveTotalTickets;
			parameters[21].Value = model.reserveDaysLimit;
			parameters[22].Value = model.updated;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
		public bool Update(FxProductMonitor.Model.Tickets model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Tickets set ");
			strSql.Append("ticketPriceId=@ticketPriceId,");
			strSql.Append("ticketName=@ticketName,");
			strSql.Append("effectiveBeginDate=@effectiveBeginDate,");
			strSql.Append("effectiveEndDate=@effectiveEndDate,");
			strSql.Append("startSellDate=@startSellDate,");
			strSql.Append("stopSellDate=@stopSellDate,");
			strSql.Append("exceptDate=@exceptDate,");
			strSql.Append("sceneryId=@sceneryId,");
			strSql.Append("sceneryName=@sceneryName,");
			strSql.Append("marketPrice=@marketPrice,");
			strSql.Append("tcAmountPrice=@tcAmountPrice,");
			strSql.Append("agentPrice=@agentPrice,");
			strSql.Append("isRealName=@isRealName,");
			strSql.Append("isNeedIdCard=@isNeedIdCard,");
			strSql.Append("minSaleQty=@minSaleQty,");
			strSql.Append("maxSaleQty=@maxSaleQty,");
			strSql.Append("reserveBeforeDays=@reserveBeforeDays,");
			strSql.Append("reserveBeforeTime=@reserveBeforeTime,");
			strSql.Append("reserveType=@reserveType,");
			strSql.Append("reserveTimes=@reserveTimes,");
			strSql.Append("reserveTotalTickets=@reserveTotalTickets,");
			strSql.Append("reserveDaysLimit=@reserveDaysLimit,");
			strSql.Append("updated=@updated");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@ticketPriceId", SqlDbType.Int,4),
					new SqlParameter("@ticketName", SqlDbType.VarChar,255),
					new SqlParameter("@effectiveBeginDate", SqlDbType.VarChar,50),
					new SqlParameter("@effectiveEndDate", SqlDbType.VarChar,50),
					new SqlParameter("@startSellDate", SqlDbType.VarChar,50),
					new SqlParameter("@stopSellDate", SqlDbType.VarChar,50),
					new SqlParameter("@exceptDate", SqlDbType.VarChar,1000),
					new SqlParameter("@sceneryId", SqlDbType.VarChar,50),
					new SqlParameter("@sceneryName", SqlDbType.VarChar,100),
					new SqlParameter("@marketPrice", SqlDbType.VarChar,50),
					new SqlParameter("@tcAmountPrice", SqlDbType.VarChar,50),
					new SqlParameter("@agentPrice", SqlDbType.VarChar,50),
					new SqlParameter("@isRealName", SqlDbType.VarChar,50),
					new SqlParameter("@isNeedIdCard", SqlDbType.VarChar,50),
					new SqlParameter("@minSaleQty", SqlDbType.Int,4),
					new SqlParameter("@maxSaleQty", SqlDbType.Int,4),
					new SqlParameter("@reserveBeforeDays", SqlDbType.Int,4),
					new SqlParameter("@reserveBeforeTime", SqlDbType.VarChar,50),
					new SqlParameter("@reserveType", SqlDbType.Int,4),
					new SqlParameter("@reserveTimes", SqlDbType.Int,4),
					new SqlParameter("@reserveTotalTickets", SqlDbType.Int,4),
					new SqlParameter("@reserveDaysLimit", SqlDbType.Int,4),
					new SqlParameter("@updated", SqlDbType.Int,4),
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = model.ticketPriceId;
			parameters[1].Value = model.ticketName;
			parameters[2].Value = model.effectiveBeginDate;
			parameters[3].Value = model.effectiveEndDate;
			parameters[4].Value = model.startSellDate;
			parameters[5].Value = model.stopSellDate;
			parameters[6].Value = model.exceptDate;
			parameters[7].Value = model.sceneryId;
			parameters[8].Value = model.sceneryName;
			parameters[9].Value = model.marketPrice;
			parameters[10].Value = model.tcAmountPrice;
			parameters[11].Value = model.agentPrice;
			parameters[12].Value = model.isRealName;
			parameters[13].Value = model.isNeedIdCard;
			parameters[14].Value = model.minSaleQty;
			parameters[15].Value = model.maxSaleQty;
			parameters[16].Value = model.reserveBeforeDays;
			parameters[17].Value = model.reserveBeforeTime;
			parameters[18].Value = model.reserveType;
			parameters[19].Value = model.reserveTimes;
			parameters[20].Value = model.reserveTotalTickets;
			parameters[21].Value = model.reserveDaysLimit;
			parameters[22].Value = model.updated;
			parameters[23].Value = model.id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Tickets ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Tickets ");
			strSql.Append(" where id in ("+idlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
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
		public FxProductMonitor.Model.Tickets GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,ticketPriceId,ticketName,effectiveBeginDate,effectiveEndDate,startSellDate,stopSellDate,exceptDate,sceneryId,sceneryName,marketPrice,tcAmountPrice,agentPrice,isRealName,isNeedIdCard,minSaleQty,maxSaleQty,reserveBeforeDays,reserveBeforeTime,reserveType,reserveTimes,reserveTotalTickets,reserveDaysLimit,updated from Tickets ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			FxProductMonitor.Model.Tickets model=new FxProductMonitor.Model.Tickets();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
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
		public FxProductMonitor.Model.Tickets DataRowToModel(DataRow row)
		{
			FxProductMonitor.Model.Tickets model=new FxProductMonitor.Model.Tickets();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["ticketPriceId"]!=null && row["ticketPriceId"].ToString()!="")
				{
					model.ticketPriceId=int.Parse(row["ticketPriceId"].ToString());
				}
				if(row["ticketName"]!=null)
				{
					model.ticketName=row["ticketName"].ToString();
				}
				if(row["effectiveBeginDate"]!=null)
				{
					model.effectiveBeginDate=row["effectiveBeginDate"].ToString();
				}
				if(row["effectiveEndDate"]!=null)
				{
					model.effectiveEndDate=row["effectiveEndDate"].ToString();
				}
				if(row["startSellDate"]!=null)
				{
					model.startSellDate=row["startSellDate"].ToString();
				}
				if(row["stopSellDate"]!=null)
				{
					model.stopSellDate=row["stopSellDate"].ToString();
				}
				if(row["exceptDate"]!=null)
				{
					model.exceptDate=row["exceptDate"].ToString();
				}
				if(row["sceneryId"]!=null)
				{
					model.sceneryId=row["sceneryId"].ToString();
				}
				if(row["sceneryName"]!=null)
				{
					model.sceneryName=row["sceneryName"].ToString();
				}
				if(row["marketPrice"]!=null)
				{
					model.marketPrice=row["marketPrice"].ToString();
				}
				if(row["tcAmountPrice"]!=null)
				{
					model.tcAmountPrice=row["tcAmountPrice"].ToString();
				}
				if(row["agentPrice"]!=null)
				{
					model.agentPrice=row["agentPrice"].ToString();
				}
				if(row["isRealName"]!=null)
				{
					model.isRealName=row["isRealName"].ToString();
				}
				if(row["isNeedIdCard"]!=null)
				{
					model.isNeedIdCard=row["isNeedIdCard"].ToString();
				}
				if(row["minSaleQty"]!=null && row["minSaleQty"].ToString()!="")
				{
					model.minSaleQty=int.Parse(row["minSaleQty"].ToString());
				}
				if(row["maxSaleQty"]!=null && row["maxSaleQty"].ToString()!="")
				{
					model.maxSaleQty=int.Parse(row["maxSaleQty"].ToString());
				}
				if(row["reserveBeforeDays"]!=null && row["reserveBeforeDays"].ToString()!="")
				{
					model.reserveBeforeDays=int.Parse(row["reserveBeforeDays"].ToString());
				}
				if(row["reserveBeforeTime"]!=null)
				{
					model.reserveBeforeTime=row["reserveBeforeTime"].ToString();
				}
				if(row["reserveType"]!=null && row["reserveType"].ToString()!="")
				{
					model.reserveType=int.Parse(row["reserveType"].ToString());
				}
				if(row["reserveTimes"]!=null && row["reserveTimes"].ToString()!="")
				{
					model.reserveTimes=int.Parse(row["reserveTimes"].ToString());
				}
				if(row["reserveTotalTickets"]!=null && row["reserveTotalTickets"].ToString()!="")
				{
					model.reserveTotalTickets=int.Parse(row["reserveTotalTickets"].ToString());
				}
				if(row["reserveDaysLimit"]!=null && row["reserveDaysLimit"].ToString()!="")
				{
					model.reserveDaysLimit=int.Parse(row["reserveDaysLimit"].ToString());
				}
				if(row["updated"]!=null && row["updated"].ToString()!="")
				{
					model.updated=int.Parse(row["updated"].ToString());
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,ticketPriceId,ticketName,effectiveBeginDate,effectiveEndDate,startSellDate,stopSellDate,exceptDate,sceneryId,sceneryName,marketPrice,tcAmountPrice,agentPrice,isRealName,isNeedIdCard,minSaleQty,maxSaleQty,reserveBeforeDays,reserveBeforeTime,reserveType,reserveTimes,reserveTotalTickets,reserveDaysLimit,updated ");
			strSql.Append(" FROM Tickets ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" id,ticketPriceId,ticketName,effectiveBeginDate,effectiveEndDate,startSellDate,stopSellDate,exceptDate,sceneryId,sceneryName,marketPrice,tcAmountPrice,agentPrice,isRealName,isNeedIdCard,minSaleQty,maxSaleQty,reserveBeforeDays,reserveBeforeTime,reserveType,reserveTimes,reserveTotalTickets,reserveDaysLimit,updated ");
			strSql.Append(" FROM Tickets ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM Tickets ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.id desc");
			}
			strSql.Append(")AS Row, T.*  from Tickets T ");
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
			parameters[0].Value = "Tickets";
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
        public int GetTicketId(string ticketPriceId)
        {
            string sql = "select id from Tickets where ticketpriceId=" + ticketPriceId;
            try
            {
                return Convert.ToInt32(DbHelperSQL.GetSingle(sql));
            }
            catch
            {
                return -1;
            }
        }


        public FxProductMonitor.Model.Tickets GetModelByTicketId(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,ticketPriceId,ticketName,effectiveBeginDate,effectiveEndDate,startSellDate,stopSellDate,exceptDate,sceneryId,sceneryName,marketPrice,tcAmountPrice,agentPrice,isRealName,isNeedIdCard,minSaleQty,maxSaleQty,reserveBeforeDays,reserveBeforeTime,reserveType,reserveTimes,reserveTotalTickets,reserveDaysLimit,updated from Tickets ");
            strSql.Append(" where ticketPriceId=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            FxProductMonitor.Model.Tickets model = new FxProductMonitor.Model.Tickets();
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

        public int SetProductState(int state)
        {
            string sql = "update Tickets set updated=" + state;
            try
            {
                return DbHelperSQL.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
		#endregion  ExtensionMethod
	}
}

