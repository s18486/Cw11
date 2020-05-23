using Cw11.DTO;
using Cw11.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw11.Services
{
    public interface IDbService
    {
        public IEnumerable<Doctor> GetDoctors();
        public bool DeleteDoctor(int IdDoctor);
        public bool AddDoctor(AddDoctorRequest request);
        public bool UpdateDoctor(UpdateDoctorRequest request);
    }
}
