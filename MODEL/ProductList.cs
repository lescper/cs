/**  版本信息模板在安装目录下，可自行修改。
* ProductList.cs
*
* 功 能： N/A
* 类 名： ProductList
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015-05-22 10:52:51   N/A    初版
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
	/// ProductList:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ProductList
	{
		public ProductList()
		{}
		#region Model
		private int _id;
		private int? _productno;
		private string _productname;
		private string _img;
		private string _isonlinepay;
		private int? _ticketcount;
		private string _saleprice;
		private string _cityname;
		private string _cutprice;
		private string _marketprice;
		private string _express;
		private string _orderdesc;
		private DateTime? _pricestartdate;
		private DateTime? _priceenddate;
		private string _viewname;
		private string _viewlongitude;
		private string _viewlatitude;
		private string _viewaddress;
		private string _viewid;
		private string _interfaceid;
		private string _interfaceprodid;
		private int? _state;
		private int? _productstate;
		private DateTime? _updatedate;
		private int? _is_single;
		private int? _istaobaocode;
		private string _settlementprice;
		private int? _startnum;
		private string _custfiled;
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
		public int? productNo
		{
			set{ _productno=value;}
			get{return _productno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string productName
		{
			set{ _productname=value;}
			get{return _productname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string img
		{
			set{ _img=value;}
			get{return _img;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string isOnlinepay
		{
			set{ _isonlinepay=value;}
			get{return _isonlinepay;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ticketCount
		{
			set{ _ticketcount=value;}
			get{return _ticketcount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string salePrice
		{
			set{ _saleprice=value;}
			get{return _saleprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cityName
		{
			set{ _cityname=value;}
			get{return _cityname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cutPrice
		{
			set{ _cutprice=value;}
			get{return _cutprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string marketPrice
		{
			set{ _marketprice=value;}
			get{return _marketprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string express
		{
			set{ _express=value;}
			get{return _express;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string orderDesc
		{
			set{ _orderdesc=value;}
			get{return _orderdesc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? priceStartDate
		{
			set{ _pricestartdate=value;}
			get{return _pricestartdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? priceEndDate
		{
			set{ _priceenddate=value;}
			get{return _priceenddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string viewName
		{
			set{ _viewname=value;}
			get{return _viewname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string viewLongitude
		{
			set{ _viewlongitude=value;}
			get{return _viewlongitude;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string viewLatitude
		{
			set{ _viewlatitude=value;}
			get{return _viewlatitude;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string viewAddress
		{
			set{ _viewaddress=value;}
			get{return _viewaddress;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string viewId
		{
			set{ _viewid=value;}
			get{return _viewid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string interfaceId
		{
			set{ _interfaceid=value;}
			get{return _interfaceid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string interfaceProdId
		{
			set{ _interfaceprodid=value;}
			get{return _interfaceprodid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? state
		{
			set{ _state=value;}
			get{return _state;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ProductState
		{
			set{ _productstate=value;}
			get{return _productstate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? updateDate
		{
			set{ _updatedate=value;}
			get{return _updatedate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? is_single
		{
			set{ _is_single=value;}
			get{return _is_single;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? isTaoBaoCode
		{
			set{ _istaobaocode=value;}
			get{return _istaobaocode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SettlementPrice
		{
			set{ _settlementprice=value;}
			get{return _settlementprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? StartNum
		{
			set{ _startnum=value;}
			get{return _startnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string custFiled
		{
			set{ _custfiled=value;}
			get{return _custfiled;}
		}
		#endregion Model

	}
}

