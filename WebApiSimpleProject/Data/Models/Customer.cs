using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiSimpleProject.Data.Models
{
    public partial class Customer
    {
        public int Id { get; set; }

        public string Adress { get; set; }

        public string Degree { get; set; }

        public bool Sex { get; set; }




        public virtual ProductCategory Category { get; set; }
    }
}
