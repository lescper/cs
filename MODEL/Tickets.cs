/**  版本信息模板在安装目录下，可自行修改。
* Tickets.cs
*
* 功 能： N/A
* 类 名： Tickets
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015-03-03 11:29:45   N/A    初版
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
	/// Tickets:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Tickets
	{
		public Tickets()
		{}
		#region Model
		private int _id;
		private int? _ticketpriceid;
		private string _ticketname;
		private string _effectivebegindate;
		private string _effectiveenddate;
		private string _startselldate;
		private string _stopselldate;
		private string _exceptdate;
		private string _sceneryid;
		private string _sceneryname;
		private string _marketprice;
		private string _tcamountprice;
		private string _agentprice;
		private string _isrealname;
		private string _isneedidcard;
		private int? _minsaleqty;
		private int? _maxsaleqty;
		private int? _reservebeforedays;
		private string _reservebeforetime;
		private int? _reservetype;
		private int? _reservetimes;
		private int? _reservetotaltickets;
		private int? _reservedayslimit;
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
		/// 产品Id
		/// </summary>
		public int? ticketPriceId
		{
			set{ _ticketpriceid=value;}
			get{return _ticketpriceid;}
		}
		/// <summary>
		/// 产品名称
		/// </summary>
		public string ticketName
		{
			set{ _ticketname=value;}
			get{return _ticketname;}
		}
		/// <summary>
		/// 票型有效期开始时间
		/// </summary>
		public string effectiveBeginDate
		{
			set{ _effectivebegindate=value;}
			get{return _effectivebegindate;}
		}
		/// <summary>
		/// 票型有效期结束时间
		/// </summary>
		public string effectiveEndDate
		{
			set{ _effectiveenddate=value;}
			get{return _effectiveenddate;}
		}
		/// <summary>
		/// 票型开始售卖日期
		/// </summary>
		public string startSellDate
		{
			set{ _startselldate=value;}
			get{return _startselldate;}
		}
		/// <summary>
		/// 票型结束售卖日期
		/// </summary>
		public string stopSellDate
		{
			set{ _stopselldate=value;}
			get{return _stopselldate;}
		}
		/// <summary>
		/// 屏蔽日期
		/// </summary>
		public string exceptDate
		{
			set{ _exceptdate=value;}
			get{return _exceptdate;}
		}
		/// <summary>
		/// 景区Id
		/// </summary>
		public string sceneryId
		{
			set{ _sceneryid=value;}
			get{return _sceneryid;}
		}
		/// <summary>
		/// 景区名称
		/// </summary>
		public string sceneryName
		{
			set{ _sceneryname=value;}
			get{return _sceneryname;}
		}
		/// <summary>
		/// 门市价
		/// </summary>
		public string marketPrice
		{
			set{ _marketprice=value;}
			get{return _marketprice;}
		}
		/// <summary>
		/// 同程价
		/// </summary>
		public string tcAmountPrice
		{
			set{ _tcamountprice=value;}
			get{return _tcamountprice;}
		}
		/// <summary>
		/// 分销商结算价
		/// </summary>
		public string agentPrice
		{
			set{ _agentprice=value;}
			get{return _agentprice;}
		}
		/// <summary>
		/// 是否是实名制
		/// </summary>
		public string isRealName
		{
			set{ _isrealname=value;}
			get{return _isrealname;}
		}
		/// <summary>
		/// 是否需要填写身份证
		/// </summary>
		public string isNeedIdCard
		{
			set{ _isneedidcard=value;}
			get{return _isneedidcard;}
		}
		/// <summary>
		/// 单次购买最小量
		/// </summary>
		public int? minSaleQty
		{
			set{ _minsaleqty=value;}
			get{return _minsaleqty;}
		}
		/// <summary>
		/// 单次购买最大量
		/// </summary>
		public int? maxSaleQty
		{
			set{ _maxsaleqty=value;}
			get{return _maxsaleqty;}
		}
		/// <summary>
		/// 下单提前时间
		/// </summary>
		public int? reserveBeforeDays
		{
			set{ _reservebeforedays=value;}
			get{return _reservebeforedays;}
		}
		/// <summary>
		/// 几点之前下
		/// </summary>
		public string reserveBeforeTime
		{
			set{ _reservebeforetime=value;}
			get{return _reservebeforetime;}
		}
		/// <summary>
		/// 预定频次限制(0：无，1：日；2：周；3：月)
		/// </summary>
		public int? reserveType
		{
			set{ _reservetype=value;}
			get{return _reservetype;}
		}
		/// <summary>
		/// 下单次数
		/// </summary>
		public int? reserveTimes
		{
			set{ _reservetimes=value;}
			get{return _reservetimes;}
		}
		/// <summary>
		/// 总票数
		/// </summary>
		public int? reserveTotalTickets
		{
			set{ _reservetotaltickets=value;}
			get{return _reservetotaltickets;}
		}
		/// <summary>
		/// 限制购买天数
		/// </summary>
		public int? reserveDaysLimit
		{
			set{ _reservedayslimit=value;}
			get{return _reservedayslimit;}
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

