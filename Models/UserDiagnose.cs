using System.ComponentModel.DataAnnotations;

namespace ClinicApi.Models
{
    public class UserDiagnose
    {
        public long Id { get; set; }

        public string UserName {get; set;}

        [Required]
        public bool Value { get; set; }
        
    }
}