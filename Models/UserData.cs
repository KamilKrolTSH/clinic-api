    using System.ComponentModel.DataAnnotations;

    namespace ClinicApi.Models
    {
        public class UserData
        {
            public long Id { get; set; }

            public string UserName {get; set;}

            [Required]
            public int Pregnancies { get; set; }
            
            [Required]
            public int Glucose { get; set; }

            [Required]
            public int BloodPressure { get; set; }

            [Required]
            public float DiabetesPedigreeFunction { get; set; }

            [Required]
            public int Insulin { get; set; }

            [Required]
            public float Bmi { get; set; }

            [Required]
            public int Age { get; set; }

            [Required]
            public int SkinThickness { get; set; }
        }
    }