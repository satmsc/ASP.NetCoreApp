﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Models
{
    public class EmployeeEditModalView :EmployeeCreateModalView
    {
        public int Id { get; set; }

        public string ExistingImagePath { get; set; }
    }
}
