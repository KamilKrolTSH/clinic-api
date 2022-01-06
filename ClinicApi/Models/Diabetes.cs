

    namespace ClinicApi.Models
    {
        public class Diabetes
        {
            public int Pregnancies { get; set; }    

            public int Glucose { get; set; }

            public int BloodPressure { get; set; }

            public float DiabetesPedigreeFunction { get; set; }

            public int Insulin { get; set; }

            public float Bmi { get; set; }

            public int Age { get; set; }
            
            public int SkinThickness { get; set; }

            public int Outcome {get; set;}

            public float Score {get; set;}
        }
    }