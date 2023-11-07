using NPOI.OpenXmlFormats.Dml.Diagram;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

using System.Text;

namespace AiBi.Test.Common
{
    /// <summary>
    /// 异常
    /// </summary>
    public static class IEnumerableExpress
    {
        public static void ForEach<T>(this IEnumerable<T> list, Action<T,int> action)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
            var index = 0;
            foreach ( var item in list)
            {
                action(item,index++);
            }
        }
    }
}
