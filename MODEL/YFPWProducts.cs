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
namespace FxProductMonitor.Model
{
	/// <summary>
	/// YFPWProducts:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class YFPWProducts
	{
		public YFPWProducts()
		{}
		#region Model
		private int? _product_id;
		private string _product_name;
		private string _product_facevalue;
		private string _product_platformvalue;
		private string _provide_saleprice;
		private string _product_start;
		private string _product_end;
		private string _product_introduction;
		private string _product_instructions;
		private string _scenic_name;
		private string _scenic_tel;
		private string _scenic_address;
		private string _scenic_bus;
		private string _scenic_drivingroute;
		private string _product_abstract;
		private string _product_realnamestatus;
		private string _product_limitstatus;
		private string _product_maxpople;
		private string _product_limittime;
		private string _product_limitday;
		private string _product_imgs;
		private int? _updated;
		/// <summary>
		/// 
		/// </summary>
		public int? product_id
		{
			set{ _product_id=value;}
			get{return _product_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string product_name
		{
			set{ _product_name=value;}
			get{return _product_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string product_facevalue
		{
			set{ _product_facevalue=value;}
			get{return _product_facevalue;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string product_platformvalue
		{
			set{ _product_platformvalue=value;}
			get{return _product_platformvalue;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string provide_salePrice
		{
			set{ _provide_saleprice=value;}
			get{return _provide_saleprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string product_start
		{
			set{ _product_start=value;}
			get{return _product_start;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string product_end
		{
			set{ _product_end=value;}
			get{return _product_end;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string product_Introduction
		{
			set{ _product_introduction=value;}
			get{return _product_introduction;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string product_instructions
		{
			set{ _product_instructions=value;}
			get{return _product_instructions;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string scenic_name
		{
			set{ _scenic_name=value;}
			get{return _scenic_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string scenic_tel
		{
			set{ _scenic_tel=value;}
			get{return _scenic_tel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string scenic_address
		{
			set{ _scenic_address=value;}
			get{return _scenic_address;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string scenic_bus
		{
			set{ _scenic_bus=value;}
			get{return _scenic_bus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string scenic_drivingroute
		{
			set{ _scenic_drivingroute=value;}
			get{return _scenic_drivingroute;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string product_Abstract
		{
			set{ _product_abstract=value;}
			get{return _product_abstract;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string product_realnamestatus
		{
			set{ _product_realnamestatus=value;}
			get{return _product_realnamestatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string product_limitstatus
		{
			set{ _product_limitstatus=value;}
			get{return _product_limitstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string product_maxpople
		{
			set{ _product_maxpople=value;}
			get{return _product_maxpople;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string product_limittime
		{
			set{ _product_limittime=value;}
			get{return _product_limittime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string product_limitday
		{
			set{ _product_limitday=value;}
			get{return _product_limitday;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string product_imgs
		{
			set{ _product_imgs=value;}
			get{return _product_imgs;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? updated
		{
			set{ _updated=value;}
			get{return _updated;}
		}
		#endregion Model

	}
}

