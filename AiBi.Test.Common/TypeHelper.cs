using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Web;
using System.Collections;

namespace AiBi.Test.Common
{
    /// <summary>
    /// 反射帮助类
    /// </summary>
    public static class TypeHelper
    {
        public const BindingFlags __flags = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.SetProperty;
        public static PropertyInfo[] GetProperties<T>()
        {
            return typeof(T).GetProperties(__flags);
        }
        public static bool HasProperty<T>(string name)
        {
            return GetProperty<T>(name) != null;
        }
        public static PropertyInfo GetProperty<T>(string name)
        {
            return typeof(T).GetProperty(name, __flags);
        }
        public static void SetPropertyValue<T>(this T obj,string name, object value)
        {
            if (obj == null)
            {
                return;
            }
            var prop = GetProperty<T>(name);
            if (prop == null)
            {
                return;
            }
            prop.SetValue(obj, value);
        }
        public static T GetPropertyValue<T>(this T obj,string name)
        {
            var prop = GetProperty<T>(name);
            if (prop == null)
            {
                return default;
            }
            var val = prop.GetValue(obj);
            if (!(val is T))
            {
                return default;
            }
            return (T)val;
        }
    }

}