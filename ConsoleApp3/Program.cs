using Newtonsoft.Json;
using System.Collections;
using System.Collections.Concurrent;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Text.Unicode;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace ConsoleApp3
{
    internal class Program
    {
        public static List<Employee> empList = new List<Employee>();
        public static List<Employee> GetEmp(string path)
        {
            List<Employee>? empList;

            using (StreamReader sr = File.OpenText(path))
            {
                JsonSerializer serializer = new JsonSerializer();

                empList = (List<Employee>)serializer.Deserialize(sr, typeof(List<Employee>));
            }

            return empList;
        }
        static void Main(string[] args)
        {
            Employee em1;

            Employee emp = new Employee(new Person("Вареник Жопкин", 50), 5, "Ростов на Дону", 45000);

            List<Employee> listEmp = new List<Employee>()
            {
                new Employee(new Person("Антипов Николай", 35), 5, "Ростов-на-Дону", 20000),
                new Employee(new Person("Варламов Вадим", 22), 5, "Ростов-на-Дону", 12200),
                new Employee(new Person("Никифорова Алина", 23), 5, "Москва", 35600),
                new Employee(new Person("Ким Сергей", 45), 5, "Ростов-на-Дону", 70000),
                new Employee(new Person("Ян Армен", 50), 5, "Екатеренбург", 75000),
                new Employee(new Person("Гаврюшина Екатерина", 33), 5, "Москва", 41000),
                new Employee(new Person("Палкин Михаил", 40), 5, "Ростов-на-Дону", 45000),
            };

            //string json = JsonSerializer.Serialize(listEmp, new JsonSerializerOptions() { WriteIndented = true, Encoder = JavaScriptEncoder.Create(UnicodeRanges.All), PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            //File.WriteAllText(@"C:\resume.json", json);

            var serializer = new JsonSerializer();

            serializer.Formatting = Formatting.Indented;

            using (StreamWriter sw = new StreamWriter(@"c:\persons.json"))
            {
                using (var jsonTextWriter = new JsonTextWriter(sw))
                {
                    serializer.Serialize(sw, listEmp);
                }
            }

            var a = GetEmp(@"c:\persons.json");

            return;

                


                return;
            // Задаем 10к элементов
            var list = GetRandomList(-5000);

            #region Task1
            // Тут коллекция, в которой только уникальные значения
            var unique = Task1(list);
            // Переменная для проверки
            var count = unique.Count();
            #endregion

            #region Task2
            // Возвращает список возрастающих последовательностей по каждому элементу списка
            var subsequence = Task2(list);

            // Максимальная последовательность по возврастанию
            var subsequence_max = subsequence.Max();
            #endregion

            #region Task2_1
            // Возвращает список возрастающих последовательностей по каждому элементу списка
            var subsequencePos = Task2Pos(list);

            // Максимальная последовательность по возврастанию
            var subsequence_maxPos = subsequencePos.Max();
            #endregion
        }


        /// <summary>
        /// Создает числовой список в хаотичном порядке
        /// </summary>
        /// <param name="minNumber">минимальное число</param>
        /// <returns>Возвращает List<int></returns>
        public static List<int> GetRandomList(int minNumber)
        {
            List<int> values = new List<int>();

            var list = Enumerable.Range(minNumber, (minNumber * (-2))).ToList();

            Parallel.For(0, list.Count, (x) =>
            {
                var a = new Random().Next(0, list.Count);

                values.Add(list[a]);

                Console.WriteLine(list[a]);
            });

            return values;
        }
        /// <summary>
        /// Метод вычесляет уникальные элементы в списке
        /// </summary>
        /// <param name="list">Список, в котором будет поиск уникальных элементов</param>
        /// <returns>Список с уникальными элементами</returns>
        public static IEnumerable<int> Task1(List<int> list)
        {
            var unique = list.AsParallel().AsQueryable().Distinct();

            return unique;
        }
        /// <summary>
        /// Возвращает список возврастающих последовательностей
        /// </summary>
        /// <param name="list">Список в котом искать</param>
        /// <returns>список последовательностей по возврастанию</returns>
        public static List<int> Task2(List<int> list)
        {
            List<int> buffer = new List<int>();

            list.AsParallel().ForAll(x =>
            {
                buffer.Add(Task2_01(list, x));
            });

            return buffer;
        }
        /// <summary>
        /// Возвращает список возврастающих последовательностей положиетльных чисел
        /// </summary>
        /// <param name="list">Список в котом искать</param>
        /// <returns>список последовательностей по возврастанию</returns>
        public static List<int> Task2Pos(List<int> list)
        {
            List<int> buffer = new List<int>();

            list.AsParallel().ForAll(x =>
            {
                buffer.Add(Task2_1Positive(list, x));
            });

            return buffer;
        }
        /// <summary>
        /// Вернет число последовательности из списка
        /// </summary>
        /// <param name="list">Список в которои ищем</param>
        /// <param name="num">значение, с которого начать поиск</param>
        /// <returns>Список длин возврастающих последовательностей</returns>
        public static int Task2_01(List<int> list, int num)
        {
            int count = 0;

            list.AsParallel().ForAll(x =>
            {
                if (x <= num)
                {
                    count++;

                    num = x;
                }
            });

            return count;
        }
        /// <summary>
        /// Вернет число последовательности из списка положительных чисел
        /// </summary>
        /// <param name="list">Список в которои ищем</param>
        /// <param name="num">значение, с которого начать поиск</param>
        /// <returns>Список длин возврастающих последовательностей</returns>
        public static int Task2_1Positive(List<int> list, int num)
        {
            int count = 0;

            if (num > 0)
            {

                list.AsParallel().ForAll(x =>
                {
                    if (x <= num)
                    {
                        count++;

                        num = x;
                    }
                });

            }

            return count;
        }
    }
}