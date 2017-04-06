using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YunWen.Models;
using YunWen.Helper;
using System.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace YunWen.Controllers
{
    public class ApiController : Controller
    {
        /// <summary>
        /// 获取未知附近商家信息
        /// </summary>
        /// <param name="longitude">经度</param>
        /// <param name="latitude">纬度</param>
        /// <returns></returns>
        public ActionResult GetInfo(string longitude, string latitude)
        {
            #region 通过经纬度获取地址信息
            var url = ConfigurationManager.AppSettings["GeoApiUrl"];
            var key = ConfigurationManager.AppSettings["BaiduMapKey"];
            var dic = new Dictionary<string, string>()
            {
                {"ak", key },
                {"location", latitude + "," + longitude },
                {"output","json" }
            };
            var addressInfo = HttpHelper.Get(url, dic);
            var address = "";
            try
            {
                var addressDes = (JObject)JsonConvert.DeserializeObject(addressInfo);
                address = addressDes["result"]["formatted_address"].ToString();
            }
            catch (Exception ex)
            {
            }
            #endregion

            #region 通过获取的地址，在数据库里面检索商家
            #endregion

            #region 再通过商家地址，反查GPS信息
            #endregion

            //将商家GPS，以及相关信息返回
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
            var result = new ResultModel<object>();
            result.Success = true;
            result.Obj = suppliers;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}