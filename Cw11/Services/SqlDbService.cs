using Cw11.DTO;
using Cw11.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw11.Services
{
    public class SqlDbService : IDbService
    {
        private readonly CodeFirstContext dbContext;

        public SqlDbService(CodeFirstContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool AddDoctor(AddDoctorRequest request)
        {
            try
            {
                dbContext.Add(new Doctor { FirstName = request.FirstName, LastName = request.LastName, Email = request.Email });
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            }

        public bool DeleteDoctor(int IdDoctor)
        {
            try
            {
                var doctor = dbContext.Doctor.Where(d => d.IdDoctor == IdDoctor).FirstOrDefault();

                if (doctor == null)
                    return false;

                dbContext.Doctor.Remove(doctor);

                var prescriptions = dbContext.Prescription.Where(p => p.IdDoctor == doctor.IdDoctor).ToList();
                foreach (var element in prescriptions)
                {
                    dbContext.Prescription.Remove(element);
                    if (dbContext.PrescriptionMedicament.Any())
                    {
                        var PrescriptionsMedicaments = dbContext.PrescriptionMedicament.Where(e => e.IdPrescription == element.IdPrescription);
                        if (PrescriptionsMedicaments != null && PrescriptionsMedicaments.Count() > 0)
                        {
                            foreach (var prescMed in PrescriptionsMedicaments)
                                dbContext.Remove(prescMed);
                        }
                    }
                }
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Doctor> GetDoctors()
        {
            try
            {
                if (!dbContext.Doctor.Any())
                    return null;

                return dbContext.Doctor.ToList(); ;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool UpdateDoctor(UpdateDoctorRequest request)
        {
            try
            {
                var element = dbContext.Doctor.Where(e => e.IdDoctor == request.IdDoctor).FirstOrDefault();
                if (element == null)
                    return false;

                element.Email = request.Email;
                element.LastName = request.LastName;
                element.FirstName = request.FirstName;
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
