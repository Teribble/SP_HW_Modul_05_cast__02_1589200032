using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    [Serializable]
    public class Person
    {
        public string? FullName { get; set; }

        public int Age { get; set; }

        public Person(string? fullName, int age)
        {
            FullName = fullName;

            Age = age;
        }
    }
}
