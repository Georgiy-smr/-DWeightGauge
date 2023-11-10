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
        [Column(TypeName = "decimal(18,7)")]
        public decimal Square { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal LowLimit { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal MaxLimit { get; set; }

        [Column(TypeName = "decimal(18,7)")]
        public decimal Weight { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Density { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        public decimal AlfaCoefficient{ get; set; }

        [Column(TypeName = "decimal(18,3)")]
        public decimal BettaCoefficient { get; set; }
        public ICollection<IPS2Cargo> IPS2Cargoes { get; set; } = new HashSet<IPS2Cargo>();

    }

}
