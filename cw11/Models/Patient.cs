﻿using System;
using System.Collections.Generic;

namespace Wyklad10Sample.Models
{
    public partial class Patient
    {
        public int IdPatient { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }

        public virtual ICollection<Prescription> Prescription { get; set; }

        public Patient()
        {
            Prescription = new HashSet<Prescription>();
        }

    }
}
