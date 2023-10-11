namespace IPS.DAL
{
    public class IPS2Cargo
    {
        public int IPSId { get; set; }
        public IPS IPS { get; set; }

        public int CargoId { get; set; }
        public Cargo Cargo { get; set; }
    }

}
