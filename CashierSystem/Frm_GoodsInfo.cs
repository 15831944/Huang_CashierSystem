﻿using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CashierSystem
{
    public partial class Frm_GoodsInfo : Form
    {
        public Frm_GoodsInfo()
        {
            InitializeComponent();
        }
        public static int entityId = int.MaxValue;

        private static Frm_GoodsInfo _form;
        public static Frm_GoodsInfo Create(int id = int.MaxValue)
        {

            if (_form == null)
            {
                _form = new Frm_GoodsInfo();
                entityId = id;


            }
            else
            {
                entityId = id;

            }
            return _form;
        }

        private void Frm_GoodsInfo_Load(object sender, EventArgs e)
        {
            Init();
        }

        public void Init()
        {
            this.Tag = false;
            //cbx 初始化
            try
            {
                var unitList = DataManager.UnitInfoBLL.GetList();
                if (unitList == null)
                {
                    unitList = new List<UnitInfo>();
                }
                var sortList = DataManager.SortInfoBLL.GetList();
                if (sortList == null)
                {
                    sortList = new List<SortInfo>();
                }
                var wholeSalerList = DataManager.WholeSalerInfoBLL.GetList();
                if (wholeSalerList == null)
                {
                    wholeSalerList = new List<WholeSalerInfo>();
                }
                cbxGood_Sort.ValueMember = "Id";
                cbxGood_Sort.DisplayMember = "SortName";
                cbxGood_Sort.DataSource = sortList;
                cbxGoods_Unit.ValueMember = "Id";
                cbxGoods_Unit.DisplayMember = "UnitName";
                cbxGoods_Unit.DataSource = unitList;
                cbxGood_WholerSaler.ValueMember = "Id";
                cbxGood_WholerSaler.DisplayMember = "SupName";
                cbxGood_WholerSaler.DataSource = wholeSalerList;
                //编辑
                if (entityId != int.MaxValue)
                {
                    var goodsInfo = DataManager.GoodsInfoBLL.GetEntityById(entityId);
                    cbxGoods_Unit.SelectedValue = goodsInfo.UnitId;
                    cbxGood_Sort.SelectedValue = goodsInfo.SortId;
                    cbxGood_WholerSaler.SelectedValue = goodsInfo.WholeSalerId;
                    txtGoods_Name.Text = goodsInfo.GoodsName;
                    txtGoods_Payprice.Text = goodsInfo.PayPrice.ToString();
                    txtGoods_Purprice.Text = goodsInfo.PurPrice.ToString();
                    txtGoods_Remark.Text = goodsInfo.Remark;
                    txtGoos_Type.Text = goodsInfo.GoodsType;
                    txtGoods_Total.Text = goodsInfo.Total.ToString();
                    txtSalesCount.Text = goodsInfo.SalesCount.ToString();
                    txtSurplusCount.Text = goodsInfo.SurplusCount.ToString();

                }
                //添加
                else
                {
                    cbxGoods_Unit.SelectedIndex = 0;
                    cbxGood_Sort.SelectedIndex = 0;
                    cbxGood_WholerSaler.SelectedIndex = 0;
                   
                }
            }
            catch (Exception)
            {

               var result= MessageBox.Show("操作异常,请检查商品表,商品单位表,供货商表数据是否合格!!!","错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
                if (result==DialogResult.OK)
                {
                    this.Close();
                }
            }
           
        }
        private void btnGoodsEnter_Click(object sender, EventArgs e)
        {
            try
            {

                
                if (entityId != int.MaxValue)
                {
                    var Oldgoodsinfo = DataManager.GoodsInfoBLL.GetEntityById(entityId);

                    if (CheckData())
                    {
                        
                        Oldgoodsinfo.Total = Convert.ToDouble(txtGoods_Total.Text.Trim());
                        //更新库存
                        Oldgoodsinfo.SurplusCount = Convert.ToDouble(txtSurplusCount.Text.ToString());
                        Oldgoodsinfo.SalesCount = Convert.ToDouble(txtSalesCount.Text.ToString());
                        Oldgoodsinfo.PurPrice = Convert.ToDecimal(txtGoods_Purprice.Text.Trim());
                        Oldgoodsinfo.PayPrice = Convert.ToDecimal(txtGoods_Payprice.Text.Trim());
                        Oldgoodsinfo.GoodsName = txtGoods_Name.Text.Trim();
                        Oldgoodsinfo.UnitId = Convert.ToInt32(cbxGoods_Unit.SelectedValue.ToString().Trim());
                        Oldgoodsinfo.SortId = Convert.ToInt32(cbxGood_Sort.SelectedValue.ToString().Trim());
                        Oldgoodsinfo.WholeSalerId = Convert.ToInt32(cbxGood_WholerSaler.SelectedValue.ToString().Trim());
                        Oldgoodsinfo.GoodsType = txtGoos_Type.Text.Trim();
                        Oldgoodsinfo.Remark = txtGoods_Remark.Text;

                        if ((Oldgoodsinfo.SalesCount + Oldgoodsinfo.SurplusCount) != Oldgoodsinfo.Total)
                        {
                            MessageBox.Show("商品数量出错!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        var isSuccess = DataManager.GoodsInfoBLL.Edit(Oldgoodsinfo);
                        if (isSuccess)
                        {
                            this.Tag = true;
                            MessageBox.Show("修改商品信息成功!", "通知", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Clear();
                            this.Close();
                        }
                    }
                }
                else
                {
                    if (CheckData())
                    {
                        GoodsInfo goodsInfo = new GoodsInfo();
                        goodsInfo.GoodsName = txtGoods_Name.Text.Trim();
                        goodsInfo.UnitId = Convert.ToInt32(cbxGoods_Unit.SelectedValue.ToString().Trim());
                        goodsInfo.SortId = Convert.ToInt32(cbxGood_Sort.SelectedValue.ToString().Trim());
                        goodsInfo.WholeSalerId = Convert.ToInt32(cbxGood_WholerSaler.SelectedValue.ToString().Trim());
                        goodsInfo.GoodsType = txtGoos_Type.Text.Trim();
                        goodsInfo.Remark = txtGoods_Remark.Text;
                        goodsInfo.SalesCount = Convert.ToDouble(txtSalesCount.Text.Trim());
                        goodsInfo.SurplusCount = Convert.ToDouble(txtSurplusCount.Text.Trim());
                        goodsInfo.Total = Convert.ToDouble(txtGoods_Total.Text.Trim());
                        goodsInfo.PurPrice = Convert.ToDecimal(txtGoods_Purprice.Text.Trim());
                        goodsInfo.PayPrice = Convert.ToDecimal(txtGoods_Payprice.Text.Trim());
                        if ((goodsInfo.SalesCount + goodsInfo.SurplusCount)!= goodsInfo.Total)
                        {
                            MessageBox.Show("商品数量出错!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        var isSuccess = DataManager.GoodsInfoBLL.Add(goodsInfo);
                        if (isSuccess)
                        {
                            this.Tag = true;
                            
                            MessageBox.Show("添加商品信息成功!", "通知", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Clear();
                            this.Close();
                        }
                    }
                }
            }
            catch 
            {

                MessageBox.Show("操作失败,请检查信息是否正确!!");
                return;
            }
           
        }

        public bool CheckData()
        {
            bool isTrue = true;
            lblTips.Visible = false;
            var total = txtGoods_Total.Text.Trim();//
            var purprice = txtGoods_Purprice.Text.Trim();
            var payPrice = txtGoods_Payprice.Text.Trim();
            if (!double.TryParse(total, out double a))
            {
                lblTips.Visible = true;
                lblTips.Text = "商品总量请填写数字!!!";
                isTrue = false;
            }
            if (!decimal.TryParse(purprice, out decimal b))
            {
                lblTips.Visible = true;
                lblTips.Text = "商品进货价请填写数字!!!";
                isTrue = false;
            }
            if (!decimal.TryParse(payPrice, out decimal c))
            {
                lblTips.Visible = true;
                lblTips.Text = "商品售货价请填写数字!!!";
                isTrue = false;
            }
            return isTrue;
        }

        private void btnGoodsCancel_Click(object sender, EventArgs e)
        {
            Clear();//单例模式 所以需要清除
            this.Close();
        }

        public void Clear()
        {
            txtGoods_Name.Text = "";
            txtGoods_Payprice.Text = "";
            txtGoods_Purprice.Text = "";
            txtGoods_Remark.Text = "";
            txtGoods_Total.Text = "";
            txtGoos_Type.Text = "";
            cbxGoods_Unit.SelectedIndex = 0;
            cbxGood_Sort.SelectedIndex = 0;
            cbxGood_WholerSaler.SelectedIndex = 0;
        }

       
    }
}
