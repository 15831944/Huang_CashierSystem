﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Model
{
   public  class UnitInfo:BaseModel
    {
        /// <summary>
        /// 单位
        /// </summary>
        public  string UnitName { get; set; }
      

       public  UnitInfo()
        {
            ModelName = "UnitInfo";           
           
        }

        public override string GetSql()
        {
            return "([UnitName],[Remark],[DelFlag]) ";
        }
        public override string GetAddSql()
        {
            string sql = " Values ('" + this.UnitName + "','" + this.Remark  + "'," + this.DelFlag + ")";

            return sql;
        }
        public override string GetEditSql()
        {
            string sql = " Set[UnitName]='" + this.UnitName + "', [Remark]='" + this.Remark  + "'  Where [ID]=" + this.Id;
            return sql;
        }

        public override List<BaseModel> GetList(DataTable dataTable)
        {

            DataRow dr = null;
            UnitInfo unitInfo= new UnitInfo();
            List<BaseModel> Entitys = new List<BaseModel>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                dr = dataTable.Rows[i];
                unitInfo.Id = Convert.ToInt32(dr["ID"]);
                unitInfo.UnitName = Convert.ToString(dr["UnitName"]);
                unitInfo.Remark = Convert.ToString(dr["Remark"]);               
                Entitys.Add(unitInfo);
            }

            return Entitys;
        }

    }
}
