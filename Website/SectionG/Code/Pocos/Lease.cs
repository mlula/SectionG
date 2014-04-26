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
        public string AddressNbr { get; set; }
        public string AppartmentNbr { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public Borough Borough { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Details { get; set; }
        public string Inclusions { get; set; }
    }
}