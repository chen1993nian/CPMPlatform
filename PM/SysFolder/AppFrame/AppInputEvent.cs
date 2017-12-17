using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace EIS.Web.SysFolder.AppFrame
{
    public class AppInputEvent
    {
        /// <summary>
        /// 增加、修改立项信息，保存后的事件
        /// </summary>
        /// <param name="tblName">表名</param>
        /// <param name="mainRow">主表数据</param>
        /// <param name="flag">1为新建，2为修改</param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public string SaveAfter(string tblName, DataRow mainRow, int flag, DbTransaction tran)
        {

            return("");
        }
    }
}