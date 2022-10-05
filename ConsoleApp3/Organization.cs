using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    public static class Organization
    {
        private static object _locker = new object();

        public static List<Employee> DeserializationFolder(string path)
        {
            List<Employee> employees = new List<Employee>();

            if (Directory.Exists(path))
            {
                var files = Directory.GetFiles(path);

                Parallel.ForEach(files, (file) =>
                {
                    lock (_locker)
                    {
                        employees.Add(DeserializationEmloyee(file));
                    }
                });
            }

            return employees;
        }

        public static Employee DeserializationEmloyee(string pathFile)
        {
            Employee emp = null;

            if (File.Exists(pathFile))
            {
                using (StreamReader sr = File.OpenText(pathFile))
                {
                    JsonSerializer serializer = new JsonSerializer() { Formatting = Formatting.Indented};

                    emp = (Employee)serializer.Deserialize(sr, typeof(Employee));
                }
            }

            return emp;
        }

        public static void Serialize(Employee employee, string pathFile)
        {
            if (!File.Exists(pathFile))
            {
                File.Create(pathFile).Close();
            }

            var serializer = new JsonSerializer() { Formatting = Formatting.Indented};

            using (StreamWriter sw = new StreamWriter(pathFile))
            {
                using (var jsonTextWriter = new JsonTextWriter(sw))
                {
                    serializer.Serialize(sw, employee);
                }
            }
        }

        public static void Serialize(List<Employee> employees, string pathFolder)
        {
            if (!Directory.Exists(pathFolder))
            {
                Directory.CreateDirectory(pathFolder);
            }

            var serializer = new JsonSerializer() { Formatting = Formatting.Indented };

            for (int i = 0; i < employees.Count; i++)
            {
                using (StreamWriter sw = new StreamWriter(Path.Combine(pathFolder, $"{i}.json")))
                {
                    if (!File.Exists(Path.Combine(pathFolder, $"{i}.json")))
                    {
                        File.Create(Path.Combine(pathFolder, $"{i}.json")).Close();
                    }

                    using (var jsonTextWriter = new JsonTextWriter(sw))
                    {
                        serializer.Serialize(sw, employees[i]);
                    }
                }
            }
        }
    }
}
