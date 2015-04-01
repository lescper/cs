/**  版本信息模板在安装目录下，可自行修改。
* DadongProducts.cs
*
* 功 能： N/A
* 类 名： DadongProducts
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015-03-23 14:04:37   N/A    初版
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
	/// DadongProducts:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class DadongProducts
	{
		public DadongProducts()
		{}
		#region Model
		private int _id;
		private int? _product_id;
		private string _category;
		private string _product_name;
		private string _product_facevalue;
		private string _product_webvalue;
		private string _product_platformvalue;
		private string _product_ticketexplain;
		private string _provide_address;
		private string _product_image;
		private string _product_expiretime;
		private string _product_issubscribe;
		private int? _product_paytype;
		private int? _product_ifpassenger;
		private int? _product_passengerinfonum;
		private int? _product_everysharemany;
		private int? _product_needreserve;
		private int? _product_pretimelimittype;
		private int? _product_hasmaxmobilelimit;
		private int? _product_refundset;
		private string _product_refundtype;
		private string _product_refundpoundagetype;
		private string _product_refundpoundage;
		private string _whentimepre;
		private int? _updated;
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
		public int? product_id
		{
			set{ _product_id=value;}
			get{return _product_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string category
		{
			set{ _category=value;}
			get{return _category;}
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
		public string product_faceValue
		{
			set{ _product_facevalue=value;}
			get{return _product_facevalue;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string product_webValue
		{
			set{ _product_webvalue=value;}
			get{return _product_webvalue;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string product_platformValue
		{
			set{ _product_platformvalue=value;}
			get{return _product_platformvalue;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string product_ticketExplain
		{
			set{ _product_ticketexplain=value;}
			get{return _product_ticketexplain;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string provide_address
		{
			set{ _provide_address=value;}
			get{return _provide_address;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string product_image
		{
			set{ _product_image=value;}
			get{return _product_image;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string product_expireTime
		{
			set{ _product_expiretime=value;}
			get{return _product_expiretime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string product_isSubscribe
		{
			set{ _product_issubscribe=value;}
			get{return _product_issubscribe;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? product_payType
		{
			set{ _product_paytype=value;}
			get{return _product_paytype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? product_ifPassenger
		{
			set{ _product_ifpassenger=value;}
			get{return _product_ifpassenger;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? product_passengerInfoNum
		{
			set{ _product_passengerinfonum=value;}
			get{return _product_passengerinfonum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? product_everyShareMany
		{
			set{ _product_everysharemany=value;}
			get{return _product_everysharemany;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? product_needReserve
		{
			set{ _product_needreserve=value;}
			get{return _product_needreserve;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? product_preTimeLimitType
		{
			set{ _product_pretimelimittype=value;}
			get{return _product_pretimelimittype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? product_hasMaxMobileLimit
		{
			set{ _product_hasmaxmobilelimit=value;}
			get{return _product_hasmaxmobilelimit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? product_refundSet
		{
			set{ _product_refundset=value;}
			get{return _product_refundset;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string product_refundType
		{
			set{ _product_refundtype=value;}
			get{return _product_refundtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string product_refundPoundageType
		{
			set{ _product_refundpoundagetype=value;}
			get{return _product_refundpoundagetype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string product_refundPoundage
		{
			set{ _product_refundpoundage=value;}
			get{return _product_refundpoundage;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string whenTimePre
		{
			set{ _whentimepre=value;}
			get{return _whentimepre;}
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

