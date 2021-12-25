    using System.ComponentModel.DataAnnotations;

    namespace ClinicApi.Models
    {
        public class UserData
        {
            public long Id { get; set; }

            public string UserName {get; set;}

            [Required]
            public float Pregnancies { get; set; }
            
            [Required]
            public float Glucose { get; set; }

            [Required]
            public float BloodPressure { get; set; }

            [Required]
            public float DiabetesPedigreeFunction { get; set; }

            [Required]
            public float Insulin { get; set; }

            [Required]
            public float Bmi { get; set; }

            [Required]
            public float Age { get; set; }
            

            [Required]
            public float SkinThickness { get; set; }
        }
    }