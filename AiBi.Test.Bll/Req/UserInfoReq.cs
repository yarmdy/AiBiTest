using System;
using System.Collections.Generic;
using AiBi.Test.Dal.Enum;
using System.IO;
using AiBi.Test.Dal.Model;

namespace AiBi.Test.Bll
{
    public partial class UserInfoReq
    {
        public class Page :PageReq{ 
            public int? GroupId { get; set; }
        }
        public class Upload
        {
            public Stream Stream { get; set; }
        }
    }
}
