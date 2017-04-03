using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YunWen.Models;

namespace YunWen.Controllers
{
    public class ApiController : Controller
    {
        public ActionResult GetInfo()
        {
            var suppliers = new List<SupplierGeoModel>();
            suppliers.Add(new SupplierGeoModel()
            {
                SupplierName = "测试数据商家名称1",
                Address = "测试数据商家地址1",
                La = "120.228275",
                Lo = "30.259889",
            });
            suppliers.Add(new SupplierGeoModel()
            {
                SupplierName = "测试数据商家名称2",
                Address = "测试数据商家地址2",
                La = "120.212968",
                Lo = "30.25939",
            });
            suppliers.Add(new SupplierGeoModel()
            {
                SupplierName = "测试数据商家名称3",
                Address = "测试数据商家地址3",
                La = "120.212681",
                Lo = "30.263757",
            });
            var result = new ResultModel();
            result.Success = true;
            result.Obj = suppliers;
            return Json(result,JsonRequestBehavior.AllowGet);
        }
    }
}