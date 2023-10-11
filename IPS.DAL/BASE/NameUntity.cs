using System.ComponentModel.DataAnnotations;

namespace IPS.DAL.BASE
{
    public abstract class NameUntity : Entity
    {
        [Required]
        public string Name { get; set; }
    }


}
