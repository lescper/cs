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
using System.Data;
using System.Collections.Generic;
using FxProductMonitor.Model;
namespace FxProductMonitor.BLL
{
    /// <summary>
    /// MJLDProducts
    /// </summary>
    public partial class MJLDProducts
    {
        private readonly FxProductMonitor.DAL.MJLDProducts dal = new FxProductMonitor.DAL.MJLDProducts();
        public MJLDProducts()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int mj_id)
        {
            return dal.Exists(mj_id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(FxProductMonitor.Model.MJLDProducts model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(FxProductMonitor.Model.MJLDProducts model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int mj_id)
        {

            return dal.Delete(mj_id);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string mj_idlist)
        {
            return dal.DeleteList(mj_idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public FxProductMonitor.Model.MJLDProducts GetModel(int mj_id)
        {

            return dal.GetModel(mj_id);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<FxProductMonitor.Model.MJLDProducts> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<FxProductMonitor.Model.MJLDProducts> DataTableToList(DataTable dt)
        {
            List<FxProductMonitor.Model.MJLDProducts> modelList = new List<FxProductMonitor.Model.MJLDProducts>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                FxProductMonitor.Model.MJLDProducts model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        public void SetProductState()
        {
            dal.SetProductState();
        }

        public int GetProductId(string id)
        {
            return dal.GetProductId(id);
        }


        public FxProductMonitor.Model.MJLDProducts GetModelById(int mj_id)
        {

            return dal.GetModelById(mj_id);
        }
        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

