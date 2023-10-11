using IPS.DAL.BASE;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IPS.DAL
{
    public class IPS : NameUntity
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal Square { get; set; }

        public ICollection<IPS2Cargo> IPS2Cargoes { get; set; } = new HashSet<IPS2Cargo>();

    }

}
