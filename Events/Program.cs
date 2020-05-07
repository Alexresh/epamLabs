using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    class Program
    {
        public delegate void OnJoin(Person p, DateTime time);
        public delegate void OnLeave(Person p);
        static void Main(string[] args)
        {
            List<Person> listOfPersons = new List<Person>();
            listOfPersons.Add(new Person { Name = "Саня" });
            listOfPersons.Add(new Person { Name = "Ванес" });
            listOfPersons.Add(new Person { Name = "Илья" });
            listOfPersons.Add(new Person { Name = "Диман" });
            Office office = new Office(listOfPersons);
            foreach (var p in listOfPersons)
            {
                p.Join();
            }
            foreach (var p in listOfPersons)
            {
                p.Leave();
            }

            Console.ReadKey();

        }

    }
}
