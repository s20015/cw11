using cw11.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using Wyklad10Sample.Models;

namespace cw11.Services
{
    public class SqlServerDoctorsDbService: IDoctorsDbService
    {
        private DoctorsDbContext _context;
        public SqlServerDoctorsDbService(DoctorsDbContext context)
        {
            _context = context;
        }
        public ICollection<Doctor> ListDoctors()
        {
            return _context.Doctor.ToList();
        }

        public Doctor GetDoctor(int id)
        {
            var result = _context.Doctor.Where(d => d.IdDoctor == id).FirstOrDefault();
            if (result == null)
            {
                throw new DoctorNotFoundException();
            }

            return result;
        }

        public Doctor CreateDoctor(Doctor doctor)
        {
            try
            {
                _context.Doctor.Add(doctor);
                _context.SaveChanges();
                return doctor;
            }
            catch (DbEntityValidationException exc)
            {
                // TODO should have details
                throw new DoctorValidationException();
            }
        }

        public Doctor UpdateDoctor(int id, Doctor doctor)
        {
            var result = _context.Doctor.Where(d => d.IdDoctor == id).FirstOrDefault();
            if (result == null)
            {
                throw new DoctorNotFoundException();
            }

            try
            {
                _context.Entry(result).CurrentValues.SetValues(doctor);
                _context.SaveChanges();
                return doctor;
            }
            catch (DbEntityValidationException exc)
            {
                // TODO should have details
                throw new DoctorValidationException();
            }
        }

        public void DeleteDoctor(int id)
        {
            var result = _context.Doctor.Where(d => d.IdDoctor == id).FirstOrDefault();
            if (result == null)
            {
                throw new DoctorNotFoundException();
            }

            _context.Doctor.Remove(result);
            _context.SaveChanges();
        }

        public void GenerateSampleDate()
        {
            var doctor1 = new Doctor();
            doctor1.IdDoctor = 1;
            doctor1.FirstName = "Radosław";
            doctor1.LastName = "Nowak";
            doctor1.Email = "rn@gmail.com";

            var doctor2 = new Doctor();
            doctor1.IdDoctor = 2;
            doctor1.FirstName = "Marek";
            doctor1.LastName = "Nowak";
            doctor1.Email = "mn@gmail.com";

            var patient1 = new Patient();
            patient1.IdPatient = 1;
            patient1.FirstName = "Alek";
            patient1.LastName = "Radolak";
            patient1.Birthdate = DateTime.Now;

            var patient2 = new Patient();
            patient2.IdPatient = 2;
            patient2.FirstName = "Kuba";
            patient2.LastName = "Radolak";
            patient2.Birthdate = DateTime.Now;

            var medicament1 = new Medicament();
            medicament1.IdMedicament = 1;
            medicament1.Type = "Vitamin";
            medicament1.Name = "Vitamin C";
            medicament1.Description = "good vitamin very good";

            var pres1 = new Prescription();
            pres1.IdPrescription = 1;
            pres1.Date = DateTime.Now;
            pres1.DueDate = DateTime.Now;
            pres1.IdDoctorNavigation = doctor1;
            pres1.IdPatientNavigation = patient1;

            var pres1med = new PrescriptionMedicament();
            pres1med.Dose = 9000;
            pres1med.Details = "not too much";
            pres1med.IdMedicamentNavigation = medicament1;
            pres1med.IdPrescriptionNavigation = pres1;

            var pres2 = new Prescription();
            pres2.IdPrescription = 1;
            pres2.Date = DateTime.Now;
            pres2.DueDate = DateTime.Now;
            pres2.IdDoctorNavigation = doctor2;
            pres2.IdPatientNavigation = patient2;

            var pres2med = new PrescriptionMedicament();
            pres1med.Dose = 420;
            pres1med.Details = "666 if needed";
            pres1med.IdMedicamentNavigation = medicament1;
            pres1med.IdPrescriptionNavigation = pres2;

            _context.Add(doctor1);
            _context.Add(doctor2);
            _context.Add(patient1);
            _context.Add(patient2);
            _context.Add(medicament1);
            _context.Add(pres1);
            _context.Add(pres1med);
            _context.Add(pres2);
            _context.Add(pres2med);

            _context.SaveChanges();
        }
    }
}
