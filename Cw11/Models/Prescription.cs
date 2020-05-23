using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw11.Models
{
    public class Prescription
    {
        public int IdPrescription { set; get; }
        public DateTime Date { set; get; }
        public DateTime DueDate { set; get; }
        public int IdPatient { set; get; }
        public int IdDoctor { get; set; }


        public virtual Patient Patient { get; set; }
        public virtual ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
        public virtual Doctor Doctor { set; get; }

    }
}
