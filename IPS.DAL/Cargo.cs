using IPS.DAL.BASE;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPS.DAL
{
    public class Cargo : NameUntity
    {
        [Column(TypeName = "decimal(18,6)")]
        public decimal Weight { get; set; }

        [Column(TypeName = "decimal(18,6)")]
        public decimal Density { get; set; }
        public int Type { get; set; }
        public ICollection<IPS2Cargo> IPS2Cargoes { get; set; } = new HashSet<IPS2Cargo>();
    }
    
}