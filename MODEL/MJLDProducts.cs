/**  版本信息模板在安装目录下，可自行修改。
* MJLDProducts.cs
*
* 功 能： N/A
* 类 名： MJLDProducts
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015-03-16 16:08:09   N/A    初版
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
	/// MJLDProducts:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MJLDProducts
	{
		public MJLDProducts()
		{}
		#region Model
		private int _mj_id;
		private int? _id;
		private string _name;
		private int? _producttype;
		private int? _saletype;
		private string _areaname;
		private string _themename;
		private int? _stock;
		private string _groupname;
		private string _isreserve;
		private string _actdate;
		private string _requiredate;
		private string _salesprice;
		private string _retailprice;
		private string _settlementprice;
		private string _image;
		private string _details;
		private int? _updated;
		/// <summary>
		/// 
		/// </summary>
		public int mj_id
		{
			set{ _mj_id=value;}
			get{return _mj_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ProductType
		{
			set{ _producttype=value;}
			get{return _producttype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? SaleType
		{
			set{ _saletype=value;}
			get{return _saletype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AreaName
		{
			set{ _areaname=value;}
			get{return _areaname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ThemeName
		{
			set{ _themename=value;}
			get{return _themename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Stock
		{
			set{ _stock=value;}
			get{return _stock;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string GroupName
		{
			set{ _groupname=value;}
			get{return _groupname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IsReserve
		{
			set{ _isreserve=value;}
			get{return _isreserve;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ActDate
		{
			set{ _actdate=value;}
			get{return _actdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RequireDate
		{
			set{ _requiredate=value;}
			get{return _requiredate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SalesPrice
		{
			set{ _salesprice=value;}
			get{return _salesprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RetailPrice
		{
			set{ _retailprice=value;}
			get{return _retailprice;}
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
		public string Image
		{
			set{ _image=value;}
			get{return _image;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Details
		{
			set{ _details=value;}
			get{return _details;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Updated
		{
			set{ _updated=value;}
			get{return _updated;}
		}

        public string Area { get; set; }
		#endregion Model

	}
}

