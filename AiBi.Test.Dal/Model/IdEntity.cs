using System;
using System.Collections.Generic;

namespace AiBi.Test.Dal.Model
{
    public partial class IdEntity:BaseEntity
    {
        public IdEntity()
        {
            
        }
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }
    }
}
