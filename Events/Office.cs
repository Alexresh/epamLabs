using System;
using System.Collections.Generic;

namespace Events
{
    delegate void SayHello(Person p, DateTime time);
    delegate void SayBye(Person p);
    class Office
    {
        private SayHello allHello;
        private SayBye allBye;

        public Office(List<Person> persons)
        {
            foreach (var p in persons)
            {
                p.OnJoin += OnCameHandler;
                p.OnLeave += OnLeaveHandler;
            }
        }

        private void OnCameHandler(Person p, DateTime time)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("--Сотрудник " + p.Name + " пришёл");
            allHello?.Invoke(p, time);

            allHello += p.SayHello;
            allBye += p.SayBye;
        }

        private void OnLeaveHandler(Person p)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("--Сотрудник " + p.Name + " ушёл");
            allHello -= p.SayHello;
            allBye -= p.SayBye;
            allBye?.Invoke(p);
        }
    }
}
