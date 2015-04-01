/**  版本信息模板在安装目录下，可自行修改。
* XieProducts.cs
*
* 功 能： N/A
* 类 名： XieProducts
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015-03-24 13:49:30   N/A    初版
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
	/// XieProducts:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class XieProducts
	{
		public XieProducts()
		{}
		#region Model
		private int _id;
		private int? _merchid;
		private string _merchname;
		private string _isrealname;
		private string _isyuyueday;
		private string _sortid;
		private string _sellprice;
		private string _validtime;
		private string _merchdowntime;
		private string _address;
		private string _servicetel;
		private string _costprice;
		private string _adviseprice;
		private string _buyprompt;
		private string _productdetail;
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
		public int? MerchID
		{
			set{ _merchid=value;}
			get{return _merchid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MerchName
		{
			set{ _merchname=value;}
			get{return _merchname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IsRealName
		{
			set{ _isrealname=value;}
			get{return _isrealname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IsYuyueDay
		{
			set{ _isyuyueday=value;}
			get{return _isyuyueday;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SortID
		{
			set{ _sortid=value;}
			get{return _sortid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SellPrice
		{
			set{ _sellprice=value;}
			get{return _sellprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ValidTime
		{
			set{ _validtime=value;}
			get{return _validtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MerchDownTime
		{
			set{ _merchdowntime=value;}
			get{return _merchdowntime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Address
		{
			set{ _address=value;}
			get{return _address;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ServiceTel
		{
			set{ _servicetel=value;}
			get{return _servicetel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CostPrice
		{
			set{ _costprice=value;}
			get{return _costprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AdvisePrice
		{
			set{ _adviseprice=value;}
			get{return _adviseprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BuyPrompt
		{
			set{ _buyprompt=value;}
			get{return _buyprompt;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProductDetail
		{
			set{ _productdetail=value;}
			get{return _productdetail;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Updated
		{
			set{ _updated=value;}
			get{return _updated;}
		}
		#endregion Model

	}
}

