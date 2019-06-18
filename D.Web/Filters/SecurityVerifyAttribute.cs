using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using D.Extend;
using D.Data;

namespace D.Web
{
    public class SecurityVerifyAttribute:ActionFilterAttribute
    {
        /// <summary>
        /// 执行方法之前的操作
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var verifyList = actionContext.Request.Headers;

            //获取传递的参数
            if(actionContext.ActionArguments == null || actionContext.ActionArguments.Count == 0)
            {
                HttpContext.Current.Response.Write("权限验证失败");
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            else
            {
                
            }
            base.OnActionExecuting(actionContext);
        }

        /// <summary>
        /// 判断验证信息是否正确
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="timestamp"></param>
        /// <param name="signature"></param>
        /// <param name="nonce">随机数</param>
        /// <returns> -1：超时 0：验证失败 1：验证成功 </returns>
        private int Verify(string appKey,string timestamp,string signature,string nonce)
        {
            //判断请求是否超时
            var now = Utils.GetTimeStamp();
            double ts = Utils.GetTimeDiff(timestamp, DateTime.Now);
            if(ts > (15 * 60))
            {//15min为时限
                return -1;
            }
            //判断验证是否正确
            return 0;
        }

        /// <summary>
        /// 执行方法之后的操作
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}