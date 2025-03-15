using System.ComponentModel.DataAnnotations.Schema;

namespace API_FEB.Models
{
    [NotMapped]  // ✅ Prevents Entity Framework from mapping this class to the database
    public class FilterDescriptor
    {
        public string Filter { get; set; } = string.Empty;
        public int FilterScope { get; set; }
    }
}
