using ProductCase.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCase.Data.Entity
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
