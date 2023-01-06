﻿using EmployeeManagement_AspNetCore.Models;
using System.Collections.Generic;

namespace EmployeeManagement_AspNetCore.Interface
{
    public interface IEmployeeRepository
    {
        Employee GetEmployee(int id);
        IEnumerable<Employee> GetAllEmployee();


    }
}
