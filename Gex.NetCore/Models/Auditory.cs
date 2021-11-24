namespace Gex.NetCore.Models
{
    public class Auditory
    {
        public Usuario LastModificationBy { get; set; }

        public DateTime LastModificationDate { get; set; } = DateTime.Now;

        public Usuario CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }


    }
}
