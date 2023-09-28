using System;
using System.Collections.Generic;
using AiBi.Test.Dal.Model;

namespace AiBi.Test.Bll
{
    public class BaseReq 
    {
        
    }
    public class PageReq:BaseReq
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;

        public string keyword { get; set; }

        public Dictionary<string, string> Where { get; set; }
        public Dictionary<string,bool> Sort { get; set; }

        public object Tag { get; set; }
    }
    public class EditPartsReq : BaseReq
    {
        public Dictionary<string, string> Properties { get; set; }
    }
}
