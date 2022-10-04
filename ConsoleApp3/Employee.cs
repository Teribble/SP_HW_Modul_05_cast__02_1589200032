using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    [Serializable]
    public class Employee
    {
        public Person? Person { get; set;  }

        public int WorkExperience { get; set; }

        public string? City { get; set; }

        public decimal Salary { get; set; }

        public Employee(Person? person, int workExperience, string? city, decimal salary)
        {
            Person = person;

            WorkExperience = workExperience;

            City = city;

            Salary = salary;
        }
    }
}
