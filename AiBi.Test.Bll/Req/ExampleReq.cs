using System;
using System.Collections.Generic;
using AiBi.Test.Dal.Enum;
using AiBi.Test.Dal.Model;

namespace AiBi.Test.Bll
{
    public partial class ExampleReq
    {
        public class Page :PageReq{ 
            public int? ClassifyId { get; set; }
            public int? SubClassifyId { get; set; }

            public EnumExampleStatus? Status { get; set; }
        }
        
    }
}
