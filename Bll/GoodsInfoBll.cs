﻿using Dal;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bll
{
   public  class GoodsInfoBll:BaseBll<GoodsInfo>
    {
        GoodsInfoBll()
        {
            this.CurrentDal = new GoodsInfoDal();
        }

    }
}
