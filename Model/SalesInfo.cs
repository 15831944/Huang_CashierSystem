﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 销售表
    /// </summary>
   public  class SalesInfo:BaseModel
    { 
        /// <summary>
        /// 商品信息
        /// </summary>
        public int GoodsId { get; set; }
        /// <summary>
        /// 个数(数量)
        /// </summary>
        public  double  Count { get; set; }
        /// <summary>
        /// 折扣(钱)
        /// </summary>
        public decimal DisCount { get; set; }
        /// <summary>
        /// 总价钱
        /// </summary>
        public  decimal Prices { get; set; }
        /// <summary>
        /// 是否支付完成
        /// </summary>
        public  bool IsPay { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public  DateTime CreateTime { get; set; }           
      

        public GoodsInfo GoodsInfo { get; set; }

       public  SalesInfo()
        {
            ModelName = "SalesInfo";
            GoodsInfo = null;
        }

        public override string GetSql()
        {
            return "([GoodsId],[Count],[DisCount],[Prices],[IsPay],[CreateTime],[Remark],[DelFlag]) ";
        }

        public override string GetAddSql()
        {
            string sql = " Values ('" + this.GoodsId + "','"
                + this.Count + "','" + this.DisCount + "','"
                  + this.Prices + "'," + this.IsPay + ",'"
                  + this.CreateTime + "','" + this.Remark + "',"
                  +  this.DelFlag + ")";

            return sql;
        }
        /// <summary>
        /// 修改是否支付和备注
        /// </summary>
        /// <returns></returns>
        public override string GetEditSql()
        {
            string sql = " Set [Remark]='" + this.Remark + "',"+ "[IsPay] = " + this.IsPay + "  Where [ID]=" + this.Id;

            return sql;
        }

        public override List<BaseModel> GetList(DataTable dataTable)
        {

            DataRow dr = null;
            SalesInfo salesInfo = new SalesInfo();
            List<BaseModel> Entitys = new List<BaseModel>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                dr = dataTable.Rows[i];
                salesInfo.Id = Convert.ToInt32(dr["ID"]);
                salesInfo.GoodsId = Convert.ToInt32(dr["GoodsId"]);
                salesInfo.Count = Convert.ToDouble(dr["Count"]);
                salesInfo.DisCount = Convert.ToDecimal(dr["DisCount"]);
                salesInfo.Prices = Convert.ToDecimal(dr["Prices"]);
                salesInfo.IsPay = Convert.ToBoolean(dr["IsPay"]);
                salesInfo.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
                salesInfo.Remark = Convert.ToString(dr["Remark"]);                
                Entitys.Add(salesInfo);
            }

            return Entitys;
        }

    }
}
