/**  版本信息模板在安装目录下，可自行修改。
* logs.cs
*
* 功 能： N/A
* 类 名： logs
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015-03-15 15:04:43   N/A    初版
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
	/// logs:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class logs
	{
		public logs()
		{}
		#region Model
		private int _id;
		private string _related_product;
		private string _log_text;
		private DateTime? _log_date;
		private int? _log_category;
		private int? _log_state;
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
		public string related_product
		{
			set{ _related_product=value;}
			get{return _related_product;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string log_text
		{
			set{ _log_text=value;}
			get{return _log_text;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? log_date
		{
			set{ _log_date=value;}
			get{return _log_date;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? log_category
		{
			set{ _log_category=value;}
			get{return _log_category;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? log_state
		{
			set{ _log_state=value;}
			get{return _log_state;}
		}
		#endregion Model

	}
}

