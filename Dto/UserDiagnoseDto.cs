using System.ComponentModel.DataAnnotations;

namespace ClinicApi.Models
{
    public class UserDiagnoseDto
    {
        public string userName { get; set; }
        public bool Diagnose { get; set; }
    }
}