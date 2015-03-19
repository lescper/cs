/**  版本信息模板在安装目录下，可自行修改。
* YFPWProducts.cs
*
* 功 能： N/A
* 类 名： YFPWProducts
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015-03-19 16:19:30   N/A    初版
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
	/// 数据访问类:YFPWProducts
	/// </summary>
	public partial class YFPWProducts
	{
		public YFPWProducts()
		{}
		#region  BasicMethod



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(FxProductMonitor.Model.YFPWProducts model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into YFPWProducts(");
			strSql.Append("product_id,product_name,product_facevalue,product_platformvalue,provide_salePrice,product_start,product_end,product_Introduction,product_instructions,scenic_name,scenic_tel,scenic_address,scenic_bus,scenic_drivingroute,product_Abstract,product_realnamestatus,product_limitstatus,product_maxpople,product_limittime,product_limitday,product_imgs,updated)");
			strSql.Append(" values (");
			strSql.Append("@product_id,@product_name,@product_facevalue,@product_platformvalue,@provide_salePrice,@product_start,@product_end,@product_Introduction,@product_instructions,@scenic_name,@scenic_tel,@scenic_address,@scenic_bus,@scenic_drivingroute,@product_Abstract,@product_realnamestatus,@product_limitstatus,@product_maxpople,@product_limittime,@product_limitday,@product_imgs,@updated)");
			SqlParameter[] parameters = {
					new SqlParameter("@product_id", SqlDbType.Int,4),
					new SqlParameter("@product_name", SqlDbType.VarChar,255),
					new SqlParameter("@product_facevalue", SqlDbType.VarChar,50),
					new SqlParameter("@product_platformvalue", SqlDbType.VarChar,50),
					new SqlParameter("@provide_salePrice", SqlDbType.VarChar,50),
					new SqlParameter("@product_start", SqlDbType.VarChar,50),
					new SqlParameter("@product_end", SqlDbType.VarChar,50),
					new SqlParameter("@product_Introduction", SqlDbType.VarChar,50),
					new SqlParameter("@product_instructions", SqlDbType.VarChar,50),
					new SqlParameter("@scenic_name", SqlDbType.VarChar,255),
					new SqlParameter("@scenic_tel", SqlDbType.VarChar,50),
					new SqlParameter("@scenic_address", SqlDbType.VarChar,50),
					new SqlParameter("@scenic_bus", SqlDbType.VarChar,255),
					new SqlParameter("@scenic_drivingroute", SqlDbType.VarChar,255),
					new SqlParameter("@product_Abstract", SqlDbType.VarChar,255),
					new SqlParameter("@product_realnamestatus", SqlDbType.VarChar,50),
					new SqlParameter("@product_limitstatus", SqlDbType.VarChar,50),
					new SqlParameter("@product_maxpople", SqlDbType.VarChar,50),
					new SqlParameter("@product_limittime", SqlDbType.VarChar,50),
					new SqlParameter("@product_limitday", SqlDbType.VarChar,50),
					new SqlParameter("@product_imgs", SqlDbType.VarChar,2000),
					new SqlParameter("@updated", SqlDbType.Int,4)};
			parameters[0].Value = model.product_id;
			parameters[1].Value = model.product_name;
			parameters[2].Value = model.product_facevalue;
			parameters[3].Value = model.product_platformvalue;
			parameters[4].Value = model.provide_salePrice;
			parameters[5].Value = model.product_start;
			parameters[6].Value = model.product_end;
			parameters[7].Value = model.product_Introduction;
			parameters[8].Value = model.product_instructions;
			parameters[9].Value = model.scenic_name;
			parameters[10].Value = model.scenic_tel;
			parameters[11].Value = model.scenic_address;
			parameters[12].Value = model.scenic_bus;
			parameters[13].Value = model.scenic_drivingroute;
			parameters[14].Value = model.product_Abstract;
			parameters[15].Value = model.product_realnamestatus;
			parameters[16].Value = model.product_limitstatus;
			parameters[17].Value = model.product_maxpople;
			parameters[18].Value = model.product_limittime;
			parameters[19].Value = model.product_limitday;
			parameters[20].Value = model.product_imgs;
			parameters[21].Value = model.updated;

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
		public bool Update(FxProductMonitor.Model.YFPWProducts model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update YFPWProducts set ");
			strSql.Append("product_id=@product_id,");
			strSql.Append("product_name=@product_name,");
			strSql.Append("product_facevalue=@product_facevalue,");
			strSql.Append("product_platformvalue=@product_platformvalue,");
			strSql.Append("provide_salePrice=@provide_salePrice,");
			strSql.Append("product_start=@product_start,");
			strSql.Append("product_end=@product_end,");
			strSql.Append("product_Introduction=@product_Introduction,");
			strSql.Append("product_instructions=@product_instructions,");
			strSql.Append("scenic_name=@scenic_name,");
			strSql.Append("scenic_tel=@scenic_tel,");
			strSql.Append("scenic_address=@scenic_address,");
			strSql.Append("scenic_bus=@scenic_bus,");
			strSql.Append("scenic_drivingroute=@scenic_drivingroute,");
			strSql.Append("product_Abstract=@product_Abstract,");
			strSql.Append("product_realnamestatus=@product_realnamestatus,");
			strSql.Append("product_limitstatus=@product_limitstatus,");
			strSql.Append("product_maxpople=@product_maxpople,");
			strSql.Append("product_limittime=@product_limittime,");
			strSql.Append("product_limitday=@product_limitday,");
			strSql.Append("product_imgs=@product_imgs,");
			strSql.Append("updated=@updated");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
					new SqlParameter("@product_id", SqlDbType.Int,4),
					new SqlParameter("@product_name", SqlDbType.VarChar,255),
					new SqlParameter("@product_facevalue", SqlDbType.VarChar,50),
					new SqlParameter("@product_platformvalue", SqlDbType.VarChar,50),
					new SqlParameter("@provide_salePrice", SqlDbType.VarChar,50),
					new SqlParameter("@product_start", SqlDbType.VarChar,50),
					new SqlParameter("@product_end", SqlDbType.VarChar,50),
					new SqlParameter("@product_Introduction", SqlDbType.VarChar,50),
					new SqlParameter("@product_instructions", SqlDbType.VarChar,50),
					new SqlParameter("@scenic_name", SqlDbType.VarChar,255),
					new SqlParameter("@scenic_tel", SqlDbType.VarChar,50),
					new SqlParameter("@scenic_address", SqlDbType.VarChar,50),
					new SqlParameter("@scenic_bus", SqlDbType.VarChar,255),
					new SqlParameter("@scenic_drivingroute", SqlDbType.VarChar,255),
					new SqlParameter("@product_Abstract", SqlDbType.VarChar,255),
					new SqlParameter("@product_realnamestatus", SqlDbType.VarChar,50),
					new SqlParameter("@product_limitstatus", SqlDbType.VarChar,50),
					new SqlParameter("@product_maxpople", SqlDbType.VarChar,50),
					new SqlParameter("@product_limittime", SqlDbType.VarChar,50),
					new SqlParameter("@product_limitday", SqlDbType.VarChar,50),
					new SqlParameter("@product_imgs", SqlDbType.VarChar,2000),
					new SqlParameter("@updated", SqlDbType.Int,4)};
			parameters[0].Value = model.product_id;
			parameters[1].Value = model.product_name;
			parameters[2].Value = model.product_facevalue;
			parameters[3].Value = model.product_platformvalue;
			parameters[4].Value = model.provide_salePrice;
			parameters[5].Value = model.product_start;
			parameters[6].Value = model.product_end;
			parameters[7].Value = model.product_Introduction;
			parameters[8].Value = model.product_instructions;
			parameters[9].Value = model.scenic_name;
			parameters[10].Value = model.scenic_tel;
			parameters[11].Value = model.scenic_address;
			parameters[12].Value = model.scenic_bus;
			parameters[13].Value = model.scenic_drivingroute;
			parameters[14].Value = model.product_Abstract;
			parameters[15].Value = model.product_realnamestatus;
			parameters[16].Value = model.product_limitstatus;
			parameters[17].Value = model.product_maxpople;
			parameters[18].Value = model.product_limittime;
			parameters[19].Value = model.product_limitday;
			parameters[20].Value = model.product_imgs;
			parameters[21].Value = model.updated;

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
			strSql.Append("delete from YFPWProducts ");
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
		public FxProductMonitor.Model.YFPWProducts GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 product_id,product_name,product_facevalue,product_platformvalue,provide_salePrice,product_start,product_end,product_Introduction,product_instructions,scenic_name,scenic_tel,scenic_address,scenic_bus,scenic_drivingroute,product_Abstract,product_realnamestatus,product_limitstatus,product_maxpople,product_limittime,product_limitday,product_imgs,updated from YFPWProducts ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
			};

			FxProductMonitor.Model.YFPWProducts model=new FxProductMonitor.Model.YFPWProducts();
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
		public FxProductMonitor.Model.YFPWProducts DataRowToModel(DataRow row)
		{
			FxProductMonitor.Model.YFPWProducts model=new FxProductMonitor.Model.YFPWProducts();
			if (row != null)
			{
				if(row["product_id"]!=null && row["product_id"].ToString()!="")
				{
					model.product_id=int.Parse(row["product_id"].ToString());
				}
				if(row["product_name"]!=null)
				{
					model.product_name=row["product_name"].ToString();
				}
				if(row["product_facevalue"]!=null)
				{
					model.product_facevalue=row["product_facevalue"].ToString();
				}
				if(row["product_platformvalue"]!=null)
				{
					model.product_platformvalue=row["product_platformvalue"].ToString();
				}
				if(row["provide_salePrice"]!=null)
				{
					model.provide_salePrice=row["provide_salePrice"].ToString();
				}
				if(row["product_start"]!=null)
				{
					model.product_start=row["product_start"].ToString();
				}
				if(row["product_end"]!=null)
				{
					model.product_end=row["product_end"].ToString();
				}
				if(row["product_Introduction"]!=null)
				{
					model.product_Introduction=row["product_Introduction"].ToString();
				}
				if(row["product_instructions"]!=null)
				{
					model.product_instructions=row["product_instructions"].ToString();
				}
				if(row["scenic_name"]!=null)
				{
					model.scenic_name=row["scenic_name"].ToString();
				}
				if(row["scenic_tel"]!=null)
				{
					model.scenic_tel=row["scenic_tel"].ToString();
				}
				if(row["scenic_address"]!=null)
				{
					model.scenic_address=row["scenic_address"].ToString();
				}
				if(row["scenic_bus"]!=null)
				{
					model.scenic_bus=row["scenic_bus"].ToString();
				}
				if(row["scenic_drivingroute"]!=null)
				{
					model.scenic_drivingroute=row["scenic_drivingroute"].ToString();
				}
				if(row["product_Abstract"]!=null)
				{
					model.product_Abstract=row["product_Abstract"].ToString();
				}
				if(row["product_realnamestatus"]!=null)
				{
					model.product_realnamestatus=row["product_realnamestatus"].ToString();
				}
				if(row["product_limitstatus"]!=null)
				{
					model.product_limitstatus=row["product_limitstatus"].ToString();
				}
				if(row["product_maxpople"]!=null)
				{
					model.product_maxpople=row["product_maxpople"].ToString();
				}
				if(row["product_limittime"]!=null)
				{
					model.product_limittime=row["product_limittime"].ToString();
				}
				if(row["product_limitday"]!=null)
				{
					model.product_limitday=row["product_limitday"].ToString();
				}
				if(row["product_imgs"]!=null)
				{
					model.product_imgs=row["product_imgs"].ToString();
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
			strSql.Append("select product_id,product_name,product_facevalue,product_platformvalue,provide_salePrice,product_start,product_end,product_Introduction,product_instructions,scenic_name,scenic_tel,scenic_address,scenic_bus,scenic_drivingroute,product_Abstract,product_realnamestatus,product_limitstatus,product_maxpople,product_limittime,product_limitday,product_imgs,updated ");
			strSql.Append(" FROM YFPWProducts ");
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
			strSql.Append(" product_id,product_name,product_facevalue,product_platformvalue,provide_salePrice,product_start,product_end,product_Introduction,product_instructions,scenic_name,scenic_tel,scenic_address,scenic_bus,scenic_drivingroute,product_Abstract,product_realnamestatus,product_limitstatus,product_maxpople,product_limittime,product_limitday,product_imgs,updated ");
			strSql.Append(" FROM YFPWProducts ");
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
			strSql.Append("select count(1) FROM YFPWProducts ");
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
			strSql.Append(")AS Row, T.*  from YFPWProducts T ");
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
			parameters[0].Value = "YFPWProducts";
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

