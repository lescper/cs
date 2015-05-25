/**  版本信息模板在安装目录下，可自行修改。
* ProductList.cs
*
* 功 能： N/A
* 类 名： ProductList
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015-05-22 10:56:29   N/A    初版
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
	/// 数据访问类:ProductList
	/// </summary>
	public partial class ProductList
	{
		public ProductList()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("id", "ProductList"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from ProductList");
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
		public int Add(FxProductMonitor.Model.ProductList model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into ProductList(");
			strSql.Append("productNo,productName,img,isOnlinepay,ticketCount,salePrice,cityName,cutPrice,marketPrice,express,orderDesc,priceStartDate,priceEndDate,viewName,viewLongitude,viewLatitude,viewAddress,viewId,interfaceId,interfaceProdId,state,ProductState,updateDate,is_single,isTaoBaoCode,SettlementPrice,StartNum,custFiled)");
			strSql.Append(" values (");
			strSql.Append("@productNo,@productName,@img,@isOnlinepay,@ticketCount,@salePrice,@cityName,@cutPrice,@marketPrice,@express,@orderDesc,@priceStartDate,@priceEndDate,@viewName,@viewLongitude,@viewLatitude,@viewAddress,@viewId,@interfaceId,@interfaceProdId,@state,@ProductState,@updateDate,@is_single,@isTaoBaoCode,@SettlementPrice,@StartNum,@custFiled)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@productNo", SqlDbType.Int,4),
					new SqlParameter("@productName", SqlDbType.VarChar,100),
					new SqlParameter("@img", SqlDbType.VarChar,255),
					new SqlParameter("@isOnlinepay", SqlDbType.VarChar,50),
					new SqlParameter("@ticketCount", SqlDbType.Int,4),
					new SqlParameter("@salePrice", SqlDbType.VarChar,50),
					new SqlParameter("@cityName", SqlDbType.VarChar,50),
					new SqlParameter("@cutPrice", SqlDbType.VarChar,50),
					new SqlParameter("@marketPrice", SqlDbType.VarChar,50),
					new SqlParameter("@express", SqlDbType.VarChar,255),
					new SqlParameter("@orderDesc", SqlDbType.VarChar,255),
					new SqlParameter("@priceStartDate", SqlDbType.DateTime),
					new SqlParameter("@priceEndDate", SqlDbType.DateTime),
					new SqlParameter("@viewName", SqlDbType.VarChar,255),
					new SqlParameter("@viewLongitude", SqlDbType.VarChar,255),
					new SqlParameter("@viewLatitude", SqlDbType.VarChar,255),
					new SqlParameter("@viewAddress", SqlDbType.VarChar,255),
					new SqlParameter("@viewId", SqlDbType.VarChar,255),
					new SqlParameter("@interfaceId", SqlDbType.VarChar,255),
					new SqlParameter("@interfaceProdId", SqlDbType.VarChar,255),
					new SqlParameter("@state", SqlDbType.Int,4),
					new SqlParameter("@ProductState", SqlDbType.Int,4),
					new SqlParameter("@updateDate", SqlDbType.DateTime),
					new SqlParameter("@is_single", SqlDbType.Int,4),
					new SqlParameter("@isTaoBaoCode", SqlDbType.Int,4),
					new SqlParameter("@SettlementPrice", SqlDbType.VarChar,50),
					new SqlParameter("@StartNum", SqlDbType.Int,4),
					new SqlParameter("@custFiled", SqlDbType.VarChar,1000)};
			parameters[0].Value = model.productNo;
			parameters[1].Value = model.productName;
			parameters[2].Value = model.img;
			parameters[3].Value = model.isOnlinepay;
			parameters[4].Value = model.ticketCount;
			parameters[5].Value = model.salePrice;
			parameters[6].Value = model.cityName;
			parameters[7].Value = model.cutPrice;
			parameters[8].Value = model.marketPrice;
			parameters[9].Value = model.express;
			parameters[10].Value = model.orderDesc;
			parameters[11].Value = model.priceStartDate;
			parameters[12].Value = model.priceEndDate;
			parameters[13].Value = model.viewName;
			parameters[14].Value = model.viewLongitude;
			parameters[15].Value = model.viewLatitude;
			parameters[16].Value = model.viewAddress;
			parameters[17].Value = model.viewId;
			parameters[18].Value = model.interfaceId;
			parameters[19].Value = model.interfaceProdId;
			parameters[20].Value = model.state;
			parameters[21].Value = model.ProductState;
			parameters[22].Value = model.updateDate;
			parameters[23].Value = model.is_single;
			parameters[24].Value = model.isTaoBaoCode;
			parameters[25].Value = model.SettlementPrice;
			parameters[26].Value = model.StartNum;
			parameters[27].Value = model.custFiled;

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
		public bool Update(FxProductMonitor.Model.ProductList model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update ProductList set ");
			strSql.Append("productNo=@productNo,");
			strSql.Append("productName=@productName,");
			strSql.Append("img=@img,");
			strSql.Append("isOnlinepay=@isOnlinepay,");
			strSql.Append("ticketCount=@ticketCount,");
			strSql.Append("salePrice=@salePrice,");
			strSql.Append("cityName=@cityName,");
			strSql.Append("cutPrice=@cutPrice,");
			strSql.Append("marketPrice=@marketPrice,");
			strSql.Append("express=@express,");
			strSql.Append("orderDesc=@orderDesc,");
			strSql.Append("priceStartDate=@priceStartDate,");
			strSql.Append("priceEndDate=@priceEndDate,");
			strSql.Append("viewName=@viewName,");
			strSql.Append("viewLongitude=@viewLongitude,");
			strSql.Append("viewLatitude=@viewLatitude,");
			strSql.Append("viewAddress=@viewAddress,");
			strSql.Append("viewId=@viewId,");
			strSql.Append("interfaceId=@interfaceId,");
			strSql.Append("interfaceProdId=@interfaceProdId,");
			strSql.Append("state=@state,");
			strSql.Append("ProductState=@ProductState,");
			strSql.Append("updateDate=@updateDate,");
			strSql.Append("is_single=@is_single,");
			strSql.Append("isTaoBaoCode=@isTaoBaoCode,");
			strSql.Append("SettlementPrice=@SettlementPrice,");
			strSql.Append("StartNum=@StartNum,");
			strSql.Append("custFiled=@custFiled");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@productNo", SqlDbType.Int,4),
					new SqlParameter("@productName", SqlDbType.VarChar,100),
					new SqlParameter("@img", SqlDbType.VarChar,255),
					new SqlParameter("@isOnlinepay", SqlDbType.VarChar,50),
					new SqlParameter("@ticketCount", SqlDbType.Int,4),
					new SqlParameter("@salePrice", SqlDbType.VarChar,50),
					new SqlParameter("@cityName", SqlDbType.VarChar,50),
					new SqlParameter("@cutPrice", SqlDbType.VarChar,50),
					new SqlParameter("@marketPrice", SqlDbType.VarChar,50),
					new SqlParameter("@express", SqlDbType.VarChar,255),
					new SqlParameter("@orderDesc", SqlDbType.VarChar,255),
					new SqlParameter("@priceStartDate", SqlDbType.DateTime),
					new SqlParameter("@priceEndDate", SqlDbType.DateTime),
					new SqlParameter("@viewName", SqlDbType.VarChar,255),
					new SqlParameter("@viewLongitude", SqlDbType.VarChar,255),
					new SqlParameter("@viewLatitude", SqlDbType.VarChar,255),
					new SqlParameter("@viewAddress", SqlDbType.VarChar,255),
					new SqlParameter("@viewId", SqlDbType.VarChar,255),
					new SqlParameter("@interfaceId", SqlDbType.VarChar,255),
					new SqlParameter("@interfaceProdId", SqlDbType.VarChar,255),
					new SqlParameter("@state", SqlDbType.Int,4),
					new SqlParameter("@ProductState", SqlDbType.Int,4),
					new SqlParameter("@updateDate", SqlDbType.DateTime),
					new SqlParameter("@is_single", SqlDbType.Int,4),
					new SqlParameter("@isTaoBaoCode", SqlDbType.Int,4),
					new SqlParameter("@SettlementPrice", SqlDbType.VarChar,50),
					new SqlParameter("@StartNum", SqlDbType.Int,4),
					new SqlParameter("@custFiled", SqlDbType.VarChar,1000),
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = model.productNo;
			parameters[1].Value = model.productName;
			parameters[2].Value = model.img;
			parameters[3].Value = model.isOnlinepay;
			parameters[4].Value = model.ticketCount;
			parameters[5].Value = model.salePrice;
			parameters[6].Value = model.cityName;
			parameters[7].Value = model.cutPrice;
			parameters[8].Value = model.marketPrice;
			parameters[9].Value = model.express;
			parameters[10].Value = model.orderDesc;
			parameters[11].Value = model.priceStartDate;
			parameters[12].Value = model.priceEndDate;
			parameters[13].Value = model.viewName;
			parameters[14].Value = model.viewLongitude;
			parameters[15].Value = model.viewLatitude;
			parameters[16].Value = model.viewAddress;
			parameters[17].Value = model.viewId;
			parameters[18].Value = model.interfaceId;
			parameters[19].Value = model.interfaceProdId;
			parameters[20].Value = model.state;
			parameters[21].Value = model.ProductState;
			parameters[22].Value = model.updateDate;
			parameters[23].Value = model.is_single;
			parameters[24].Value = model.isTaoBaoCode;
			parameters[25].Value = model.SettlementPrice;
			parameters[26].Value = model.StartNum;
			parameters[27].Value = model.custFiled;
			parameters[28].Value = model.id;

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
			strSql.Append("delete from ProductList ");
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
			strSql.Append("delete from ProductList ");
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
		public FxProductMonitor.Model.ProductList GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,productNo,productName,img,isOnlinepay,ticketCount,salePrice,cityName,cutPrice,marketPrice,express,orderDesc,priceStartDate,priceEndDate,viewName,viewLongitude,viewLatitude,viewAddress,viewId,interfaceId,interfaceProdId,state,ProductState,updateDate,is_single,isTaoBaoCode,SettlementPrice,StartNum,custFiled from ProductList ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			FxProductMonitor.Model.ProductList model=new FxProductMonitor.Model.ProductList();
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
		public FxProductMonitor.Model.ProductList DataRowToModel(DataRow row)
		{
			FxProductMonitor.Model.ProductList model=new FxProductMonitor.Model.ProductList();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["productNo"]!=null && row["productNo"].ToString()!="")
				{
					model.productNo=int.Parse(row["productNo"].ToString());
				}
				if(row["productName"]!=null)
				{
					model.productName=row["productName"].ToString();
				}
				if(row["img"]!=null)
				{
					model.img=row["img"].ToString();
				}
				if(row["isOnlinepay"]!=null)
				{
					model.isOnlinepay=row["isOnlinepay"].ToString();
				}
				if(row["ticketCount"]!=null && row["ticketCount"].ToString()!="")
				{
					model.ticketCount=int.Parse(row["ticketCount"].ToString());
				}
				if(row["salePrice"]!=null)
				{
					model.salePrice=row["salePrice"].ToString();
				}
				if(row["cityName"]!=null)
				{
					model.cityName=row["cityName"].ToString();
				}
				if(row["cutPrice"]!=null)
				{
					model.cutPrice=row["cutPrice"].ToString();
				}
				if(row["marketPrice"]!=null)
				{
					model.marketPrice=row["marketPrice"].ToString();
				}
				if(row["express"]!=null)
				{
					model.express=row["express"].ToString();
				}
				if(row["orderDesc"]!=null)
				{
					model.orderDesc=row["orderDesc"].ToString();
				}
				if(row["priceStartDate"]!=null && row["priceStartDate"].ToString()!="")
				{
					model.priceStartDate=DateTime.Parse(row["priceStartDate"].ToString());
				}
				if(row["priceEndDate"]!=null && row["priceEndDate"].ToString()!="")
				{
					model.priceEndDate=DateTime.Parse(row["priceEndDate"].ToString());
				}
				if(row["viewName"]!=null)
				{
					model.viewName=row["viewName"].ToString();
				}
				if(row["viewLongitude"]!=null)
				{
					model.viewLongitude=row["viewLongitude"].ToString();
				}
				if(row["viewLatitude"]!=null)
				{
					model.viewLatitude=row["viewLatitude"].ToString();
				}
				if(row["viewAddress"]!=null)
				{
					model.viewAddress=row["viewAddress"].ToString();
				}
				if(row["viewId"]!=null)
				{
					model.viewId=row["viewId"].ToString();
				}
				if(row["interfaceId"]!=null)
				{
					model.interfaceId=row["interfaceId"].ToString();
				}
				if(row["interfaceProdId"]!=null)
				{
					model.interfaceProdId=row["interfaceProdId"].ToString();
				}
				if(row["state"]!=null && row["state"].ToString()!="")
				{
					model.state=int.Parse(row["state"].ToString());
				}
				if(row["ProductState"]!=null && row["ProductState"].ToString()!="")
				{
					model.ProductState=int.Parse(row["ProductState"].ToString());
				}
				if(row["updateDate"]!=null && row["updateDate"].ToString()!="")
				{
					model.updateDate=DateTime.Parse(row["updateDate"].ToString());
				}
				if(row["is_single"]!=null && row["is_single"].ToString()!="")
				{
					model.is_single=int.Parse(row["is_single"].ToString());
				}
				if(row["isTaoBaoCode"]!=null && row["isTaoBaoCode"].ToString()!="")
				{
					model.isTaoBaoCode=int.Parse(row["isTaoBaoCode"].ToString());
				}
				if(row["SettlementPrice"]!=null)
				{
					model.SettlementPrice=row["SettlementPrice"].ToString();
				}
				if(row["StartNum"]!=null && row["StartNum"].ToString()!="")
				{
					model.StartNum=int.Parse(row["StartNum"].ToString());
				}
				if(row["custFiled"]!=null)
				{
					model.custFiled=row["custFiled"].ToString();
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
			strSql.Append("select id,productNo,productName,img,isOnlinepay,ticketCount,salePrice,cityName,cutPrice,marketPrice,express,orderDesc,priceStartDate,priceEndDate,viewName,viewLongitude,viewLatitude,viewAddress,viewId,interfaceId,interfaceProdId,state,ProductState,updateDate,is_single,isTaoBaoCode,SettlementPrice,StartNum,custFiled ");
			strSql.Append(" FROM ProductList ");
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
			strSql.Append(" id,productNo,productName,img,isOnlinepay,ticketCount,salePrice,cityName,cutPrice,marketPrice,express,orderDesc,priceStartDate,priceEndDate,viewName,viewLongitude,viewLatitude,viewAddress,viewId,interfaceId,interfaceProdId,state,ProductState,updateDate,is_single,isTaoBaoCode,SettlementPrice,StartNum,custFiled ");
			strSql.Append(" FROM ProductList ");
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
			strSql.Append("select count(1) FROM ProductList ");
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
			strSql.Append(")AS Row, T.*  from ProductList T ");
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
			parameters[0].Value = "ProductList";
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
        public int SetProductState()
        {
            string sql = "update ProductList set ProductState=0";
            try
            {
                return DbHelperSQL.ExecuteSql(sql);
            }
            catch
            {
                return 0;
            }
        }

        public int GetProductId(int id)
        {
            string sql = "select id from ProductList where ProductNo=" + id;
            try
            {
                return Convert.ToInt32(DbHelperSQL.GetSingle(sql));
            }
            catch
            {
                return -1;
            }
        }
		#endregion  ExtensionMethod
	}
}

