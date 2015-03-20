/**  版本信息模板在安装目录下，可自行修改。
* DadongProducts.cs
*
* 功 能： N/A
* 类 名： DadongProducts
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015-03-20 17:04:55   N/A    初版
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
	/// 数据访问类:DadongProducts
	/// </summary>
	public partial class DadongProducts
	{
		public DadongProducts()
		{}
		#region  BasicMethod



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(FxProductMonitor.Model.DadongProducts model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into DadongProducts(");
			strSql.Append("id,product_id,category,product_name,product_faceValue,product_webValue,product_platformValue,product_ticketExplain,provide_address,product_image,product_expireTime,product_isSubscribe,product_payType,product_ifPassenger,product_passengerInfoNum,product_everyShareMany,product_needReserve,product_preTimeLimitType,product_hasMaxMobileLimit,product_refundSet,product_refundType,product_refundPoundageType,product_refundPoundage,whenTimePre)");
			strSql.Append(" values (");
			strSql.Append("@id,@product_id,@category,@product_name,@product_faceValue,@product_webValue,@product_platformValue,@product_ticketExplain,@provide_address,@product_image,@product_expireTime,@product_isSubscribe,@product_payType,@product_ifPassenger,@product_passengerInfoNum,@product_everyShareMany,@product_needReserve,@product_preTimeLimitType,@product_hasMaxMobileLimit,@product_refundSet,@product_refundType,@product_refundPoundageType,@product_refundPoundage,@whenTimePre)");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@product_id", SqlDbType.Int,4),
					new SqlParameter("@category", SqlDbType.VarChar,50),
					new SqlParameter("@product_name", SqlDbType.VarChar,1000),
					new SqlParameter("@product_faceValue", SqlDbType.VarChar,50),
					new SqlParameter("@product_webValue", SqlDbType.VarChar,50),
					new SqlParameter("@product_platformValue", SqlDbType.VarChar,50),
					new SqlParameter("@product_ticketExplain", SqlDbType.VarChar,2000),
					new SqlParameter("@provide_address", SqlDbType.VarChar,2000),
					new SqlParameter("@product_image", SqlDbType.VarChar,1000),
					new SqlParameter("@product_expireTime", SqlDbType.VarChar,50),
					new SqlParameter("@product_isSubscribe", SqlDbType.VarChar,50),
					new SqlParameter("@product_payType", SqlDbType.Int,4),
					new SqlParameter("@product_ifPassenger", SqlDbType.Int,4),
					new SqlParameter("@product_passengerInfoNum", SqlDbType.Int,4),
					new SqlParameter("@product_everyShareMany", SqlDbType.Int,4),
					new SqlParameter("@product_needReserve", SqlDbType.Int,4),
					new SqlParameter("@product_preTimeLimitType", SqlDbType.Int,4),
					new SqlParameter("@product_hasMaxMobileLimit", SqlDbType.Int,4),
					new SqlParameter("@product_refundSet", SqlDbType.Int,4),
					new SqlParameter("@product_refundType", SqlDbType.Int,4),
					new SqlParameter("@product_refundPoundageType", SqlDbType.VarChar,50),
					new SqlParameter("@product_refundPoundage", SqlDbType.VarChar,50),
					new SqlParameter("@whenTimePre", SqlDbType.VarChar,50)};
			parameters[0].Value = model.id;
			parameters[1].Value = model.product_id;
			parameters[2].Value = model.category;
			parameters[3].Value = model.product_name;
			parameters[4].Value = model.product_faceValue;
			parameters[5].Value = model.product_webValue;
			parameters[6].Value = model.product_platformValue;
			parameters[7].Value = model.product_ticketExplain;
			parameters[8].Value = model.provide_address;
			parameters[9].Value = model.product_image;
			parameters[10].Value = model.product_expireTime;
			parameters[11].Value = model.product_isSubscribe;
			parameters[12].Value = model.product_payType;
			parameters[13].Value = model.product_ifPassenger;
			parameters[14].Value = model.product_passengerInfoNum;
			parameters[15].Value = model.product_everyShareMany;
			parameters[16].Value = model.product_needReserve;
			parameters[17].Value = model.product_preTimeLimitType;
			parameters[18].Value = model.product_hasMaxMobileLimit;
			parameters[19].Value = model.product_refundSet;
			parameters[20].Value = model.product_refundType;
			parameters[21].Value = model.product_refundPoundageType;
			parameters[22].Value = model.product_refundPoundage;
			parameters[23].Value = model.whenTimePre;

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
		/// 更新一条数据
		/// </summary>
		public bool Update(FxProductMonitor.Model.DadongProducts model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update DadongProducts set ");
			strSql.Append("id=@id,");
			strSql.Append("product_id=@product_id,");
			strSql.Append("category=@category,");
			strSql.Append("product_name=@product_name,");
			strSql.Append("product_faceValue=@product_faceValue,");
			strSql.Append("product_webValue=@product_webValue,");
			strSql.Append("product_platformValue=@product_platformValue,");
			strSql.Append("product_ticketExplain=@product_ticketExplain,");
			strSql.Append("provide_address=@provide_address,");
			strSql.Append("product_image=@product_image,");
			strSql.Append("product_expireTime=@product_expireTime,");
			strSql.Append("product_isSubscribe=@product_isSubscribe,");
			strSql.Append("product_payType=@product_payType,");
			strSql.Append("product_ifPassenger=@product_ifPassenger,");
			strSql.Append("product_passengerInfoNum=@product_passengerInfoNum,");
			strSql.Append("product_everyShareMany=@product_everyShareMany,");
			strSql.Append("product_needReserve=@product_needReserve,");
			strSql.Append("product_preTimeLimitType=@product_preTimeLimitType,");
			strSql.Append("product_hasMaxMobileLimit=@product_hasMaxMobileLimit,");
			strSql.Append("product_refundSet=@product_refundSet,");
			strSql.Append("product_refundType=@product_refundType,");
			strSql.Append("product_refundPoundageType=@product_refundPoundageType,");
			strSql.Append("product_refundPoundage=@product_refundPoundage,");
			strSql.Append("whenTimePre=@whenTimePre");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@product_id", SqlDbType.Int,4),
					new SqlParameter("@category", SqlDbType.VarChar,50),
					new SqlParameter("@product_name", SqlDbType.VarChar,1000),
					new SqlParameter("@product_faceValue", SqlDbType.VarChar,50),
					new SqlParameter("@product_webValue", SqlDbType.VarChar,50),
					new SqlParameter("@product_platformValue", SqlDbType.VarChar,50),
					new SqlParameter("@product_ticketExplain", SqlDbType.VarChar,2000),
					new SqlParameter("@provide_address", SqlDbType.VarChar,2000),
					new SqlParameter("@product_image", SqlDbType.VarChar,1000),
					new SqlParameter("@product_expireTime", SqlDbType.VarChar,50),
					new SqlParameter("@product_isSubscribe", SqlDbType.VarChar,50),
					new SqlParameter("@product_payType", SqlDbType.Int,4),
					new SqlParameter("@product_ifPassenger", SqlDbType.Int,4),
					new SqlParameter("@product_passengerInfoNum", SqlDbType.Int,4),
					new SqlParameter("@product_everyShareMany", SqlDbType.Int,4),
					new SqlParameter("@product_needReserve", SqlDbType.Int,4),
					new SqlParameter("@product_preTimeLimitType", SqlDbType.Int,4),
					new SqlParameter("@product_hasMaxMobileLimit", SqlDbType.Int,4),
					new SqlParameter("@product_refundSet", SqlDbType.Int,4),
					new SqlParameter("@product_refundType", SqlDbType.Int,4),
					new SqlParameter("@product_refundPoundageType", SqlDbType.VarChar,50),
					new SqlParameter("@product_refundPoundage", SqlDbType.VarChar,50),
					new SqlParameter("@whenTimePre", SqlDbType.VarChar,50)};
			parameters[0].Value = model.id;
			parameters[1].Value = model.product_id;
			parameters[2].Value = model.category;
			parameters[3].Value = model.product_name;
			parameters[4].Value = model.product_faceValue;
			parameters[5].Value = model.product_webValue;
			parameters[6].Value = model.product_platformValue;
			parameters[7].Value = model.product_ticketExplain;
			parameters[8].Value = model.provide_address;
			parameters[9].Value = model.product_image;
			parameters[10].Value = model.product_expireTime;
			parameters[11].Value = model.product_isSubscribe;
			parameters[12].Value = model.product_payType;
			parameters[13].Value = model.product_ifPassenger;
			parameters[14].Value = model.product_passengerInfoNum;
			parameters[15].Value = model.product_everyShareMany;
			parameters[16].Value = model.product_needReserve;
			parameters[17].Value = model.product_preTimeLimitType;
			parameters[18].Value = model.product_hasMaxMobileLimit;
			parameters[19].Value = model.product_refundSet;
			parameters[20].Value = model.product_refundType;
			parameters[21].Value = model.product_refundPoundageType;
			parameters[22].Value = model.product_refundPoundage;
			parameters[23].Value = model.whenTimePre;

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
		public bool Delete()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from DadongProducts ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
			};

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
		/// 得到一个对象实体
		/// </summary>
		public FxProductMonitor.Model.DadongProducts GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,product_id,category,product_name,product_faceValue,product_webValue,product_platformValue,product_ticketExplain,provide_address,product_image,product_expireTime,product_isSubscribe,product_payType,product_ifPassenger,product_passengerInfoNum,product_everyShareMany,product_needReserve,product_preTimeLimitType,product_hasMaxMobileLimit,product_refundSet,product_refundType,product_refundPoundageType,product_refundPoundage,whenTimePre from DadongProducts ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
			};

			FxProductMonitor.Model.DadongProducts model=new FxProductMonitor.Model.DadongProducts();
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
		public FxProductMonitor.Model.DadongProducts DataRowToModel(DataRow row)
		{
			FxProductMonitor.Model.DadongProducts model=new FxProductMonitor.Model.DadongProducts();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["product_id"]!=null && row["product_id"].ToString()!="")
				{
					model.product_id=int.Parse(row["product_id"].ToString());
				}
				if(row["category"]!=null)
				{
					model.category=row["category"].ToString();
				}
				if(row["product_name"]!=null)
				{
					model.product_name=row["product_name"].ToString();
				}
				if(row["product_faceValue"]!=null)
				{
					model.product_faceValue=row["product_faceValue"].ToString();
				}
				if(row["product_webValue"]!=null)
				{
					model.product_webValue=row["product_webValue"].ToString();
				}
				if(row["product_platformValue"]!=null)
				{
					model.product_platformValue=row["product_platformValue"].ToString();
				}
				if(row["product_ticketExplain"]!=null)
				{
					model.product_ticketExplain=row["product_ticketExplain"].ToString();
				}
				if(row["provide_address"]!=null)
				{
					model.provide_address=row["provide_address"].ToString();
				}
				if(row["product_image"]!=null)
				{
					model.product_image=row["product_image"].ToString();
				}
				if(row["product_expireTime"]!=null)
				{
					model.product_expireTime=row["product_expireTime"].ToString();
				}
				if(row["product_isSubscribe"]!=null)
				{
					model.product_isSubscribe=row["product_isSubscribe"].ToString();
				}
				if(row["product_payType"]!=null && row["product_payType"].ToString()!="")
				{
					model.product_payType=int.Parse(row["product_payType"].ToString());
				}
				if(row["product_ifPassenger"]!=null && row["product_ifPassenger"].ToString()!="")
				{
					model.product_ifPassenger=int.Parse(row["product_ifPassenger"].ToString());
				}
				if(row["product_passengerInfoNum"]!=null && row["product_passengerInfoNum"].ToString()!="")
				{
					model.product_passengerInfoNum=int.Parse(row["product_passengerInfoNum"].ToString());
				}
				if(row["product_everyShareMany"]!=null && row["product_everyShareMany"].ToString()!="")
				{
					model.product_everyShareMany=int.Parse(row["product_everyShareMany"].ToString());
				}
				if(row["product_needReserve"]!=null && row["product_needReserve"].ToString()!="")
				{
					model.product_needReserve=int.Parse(row["product_needReserve"].ToString());
				}
				if(row["product_preTimeLimitType"]!=null && row["product_preTimeLimitType"].ToString()!="")
				{
					model.product_preTimeLimitType=int.Parse(row["product_preTimeLimitType"].ToString());
				}
				if(row["product_hasMaxMobileLimit"]!=null && row["product_hasMaxMobileLimit"].ToString()!="")
				{
					model.product_hasMaxMobileLimit=int.Parse(row["product_hasMaxMobileLimit"].ToString());
				}
				if(row["product_refundSet"]!=null && row["product_refundSet"].ToString()!="")
				{
					model.product_refundSet=int.Parse(row["product_refundSet"].ToString());
				}
				if(row["product_refundType"]!=null && row["product_refundType"].ToString()!="")
				{
					model.product_refundType=int.Parse(row["product_refundType"].ToString());
				}
				if(row["product_refundPoundageType"]!=null)
				{
					model.product_refundPoundageType=row["product_refundPoundageType"].ToString();
				}
				if(row["product_refundPoundage"]!=null)
				{
					model.product_refundPoundage=row["product_refundPoundage"].ToString();
				}
				if(row["whenTimePre"]!=null)
				{
					model.whenTimePre=row["whenTimePre"].ToString();
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
			strSql.Append("select id,product_id,category,product_name,product_faceValue,product_webValue,product_platformValue,product_ticketExplain,provide_address,product_image,product_expireTime,product_isSubscribe,product_payType,product_ifPassenger,product_passengerInfoNum,product_everyShareMany,product_needReserve,product_preTimeLimitType,product_hasMaxMobileLimit,product_refundSet,product_refundType,product_refundPoundageType,product_refundPoundage,whenTimePre ");
			strSql.Append(" FROM DadongProducts ");
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
			strSql.Append(" id,product_id,category,product_name,product_faceValue,product_webValue,product_platformValue,product_ticketExplain,provide_address,product_image,product_expireTime,product_isSubscribe,product_payType,product_ifPassenger,product_passengerInfoNum,product_everyShareMany,product_needReserve,product_preTimeLimitType,product_hasMaxMobileLimit,product_refundSet,product_refundType,product_refundPoundageType,product_refundPoundage,whenTimePre ");
			strSql.Append(" FROM DadongProducts ");
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
			strSql.Append("select count(1) FROM DadongProducts ");
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
				strSql.Append("order by T. desc");
			}
			strSql.Append(")AS Row, T.*  from DadongProducts T ");
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
			parameters[0].Value = "DadongProducts";
			parameters[1].Value = "";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

