using System;
using System.Collections.Generic;
using AiBi.Test.Dal.Model;

namespace AiBi.Test.Bll
{
    public abstract class BaseReq 
    {
        public string OutMsg { get; set; }
        public string OutCode { get; set; }
    }
    public abstract class PageReq:BaseReq
    {
        public int Page { get; set; }
        public int Size { get; set; }

        public string keyword { get; set; }

        public Dictionary<string,bool> Sort { get; set; }

        public int OutCount { get; set; }
    }
}
