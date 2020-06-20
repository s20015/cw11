using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wyklad10Sample.Models;

namespace cw11.Services
{
    public interface IDoctorsDbService
    {
        public ICollection<Doctor> ListDoctors();

        public Doctor GetDoctor(int id);

        public Doctor CreateDoctor(Doctor doctor);

        public Doctor UpdateDoctor(int id, Doctor doctor);

        public void DeleteDoctor(int id);

        public void GenerateSampleDate();
    }
}
