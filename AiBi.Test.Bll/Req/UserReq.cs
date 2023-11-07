using System;
using System.Collections.Generic;
using System.IO;
using AiBi.Test.Dal.Enum;
using AiBi.Test.Dal.Model;

namespace AiBi.Test.Bll
{
    public partial class UserReq 
    {
        public class Page :PageReq{ 
            
        }
        public class Upload
        {
            public Stream Stream { get; set; }
            public EnumUserType UserType { get; set; }
            public bool IsMine { get; set; }
        }
    }
}
