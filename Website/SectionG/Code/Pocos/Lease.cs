using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SectionG.Code.Pocos
{
    [PetaPoco.TableName("_sg_Lease")]
    [PetaPoco.PrimaryKey("Id")]
    public class Lease
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public DateTime DateCreated { get; set; }
        public string IpAddress { get; set; }
    }
}