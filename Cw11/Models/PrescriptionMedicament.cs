namespace Cw11.Models
{
    public class PrescriptionMedicament
    {
        public int IdPresMed { set; get; }
        public int IdMedicament { get; set; }
        public int IdPrescription { get; set; }
        public int? Dose { get; set; }
        public string Details { get; set; }

        public virtual Medicament Medicament { get; set; }
        public virtual Prescription Prescription { get; set; }

    }
}
