using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SectionG.Code.Pocos
{
    [PetaPoco.TableName("_sg_Borough")]
    [PetaPoco.PrimaryKey("Id")]
    public class Borough
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}