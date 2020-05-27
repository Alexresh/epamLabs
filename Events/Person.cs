using System;

namespace Events
{

    class Person
    {
        public string Name { get; set; }
        public event Program.OnJoin OnJoin;
        public event Program.OnLeave OnLeave;
        public void Join()
        {
            OnJoin?.Invoke(this, DateTime.Now);
        }
        public void Leave()
        {
            OnLeave?.Invoke(this);
        }
        public void SayHello(Person p, DateTime time)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            string dayPart;
            if (time.Hour < 12)
            {
                dayPart = "Доброе утро";
            }
            else if (time.Hour < 17)
            {
                dayPart = "Добрый день";
            }
            else
            {
                dayPart = "Добрый вечер";
            }

            Console.WriteLine($"[{this.Name}]:{dayPart}, {p.Name}");
        }
        public void SayBye(Person p)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Пока, {p.Name}, сказал {this.Name}");
        }
    }
}
