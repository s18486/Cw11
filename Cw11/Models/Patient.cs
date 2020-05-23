using System.Collections.Generic;

namespace Cw11.Models
{
    public class Patient
    {
        public Patient()
        {
            Prescriptions = new HashSet<Prescription>();
        }
        public int IdPatient { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public DateTime Birthdate { set; get; }

        public virtual ICollection<Prescription> Prescriptions { set; get; }
    }
}
