using System.ComponentModel.DataAnnotations;

namespace ClinicApi.Models
{
    public class ApplicationUserDto
    {
        public float Id { get; set; }
        public float UserName { get; set; }
        public float Email { get; set; }   
    }
}