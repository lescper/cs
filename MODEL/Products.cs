/**  版本信息模板在安装目录下，可自行修改。
* Products.cs
*
* 功 能： N/A
* 类 名： Products
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015-03-05 14:10:41   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace FxProductMonitor.Model
{
	/// <summary>
	/// Products:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Products
	{
		public Products()
		{}
		#region Model
		private int _id;
		private int? _prod_id;
		private string _prod_state;
		private int? _interface_id;
		private string _interface_prod_id;
		private DateTime? _update_date;
		/// <summary>
		/// 
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? prod_id
		{
			set{ _prod_id=value;}
			get{return _prod_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string prod_state
		{
			set{ _prod_state=value;}
			get{return _prod_state;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? interface_id
		{
			set{ _interface_id=value;}
			get{return _interface_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string interface_prod_id
		{
			set{ _interface_prod_id=value;}
			get{return _interface_prod_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? update_date
		{
			set{ _update_date=value;}
			get{return _update_date;}
		}
		#endregion Model

	}
}

