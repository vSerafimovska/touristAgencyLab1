using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace prva_zadaca
{
    class Program
    {
        static void Main(string[] args)
        {
            World agency = new World(4);

            int service = -1;
            string menu = "Мени на услуги:\n" +
                "1 - Услужи го клиентот\n" +
                "2 - Продадени карти на шалтерот\n" +
                "3 - Вкупен промет на шалтерот\n" +
                "4 - Сите продадени карти на  генцијата, организирани по шалтер\n" +
                "5 - Вкупниот промет на агенцијата\n" +
                "6 - Успешност на агенцијата\n" +
                "0 - Излез\n";
            Console.WriteLine(menu);

            

            do
            {
                Console.Write("Num of Service: ");
                service = int.Parse(Console.ReadLine());
                if (!(service <= 6 && service >= 0))
                {
                    Console.WriteLine("Valid numbers of services are from 0 to 6. Try AGAIN!");
                    continue;
                }
                else if (service == 0)
                {
                    break;
                }
                else if (service == 1)
                {
                    Console.WriteLine("Insert Name, Surname, Age, Destination, Counter");
                    agency.ServeTheClient();
                }
                else if (service == 2)
                {
                    Console.WriteLine("Insert Counter number");
                    int counter = int.Parse(Console.ReadLine());
                    Console.WriteLine("Im going to take default DateTime.");
                    DateTime start = DateTime.Today.Subtract(new TimeSpan(24, 0, 0));
                    DateTime end = DateTime.Today.Add(new TimeSpan(24, 0, 0));
                    agency.OrderedTickets(counter, start, end);
                }
                else if (service == 3)
                {
                    Console.WriteLine("Insert Counter number");
                    int counter = int.Parse(Console.ReadLine());
                    Console.WriteLine("Im going to take default DateTime.");
                    DateTime start = DateTime.Today.Subtract(new TimeSpan(24, 0, 0));
                    DateTime end = DateTime.Today.Add(new TimeSpan(24, 0, 0));
                    agency.TotalProfitOfCounter(counter, start, end);
                }
                else if (service == 4)
                {
                    Console.WriteLine("Im going to take default DateTime.");
                    DateTime start = DateTime.Today.Subtract(new TimeSpan(24, 0, 0));
                    DateTime end = DateTime.Today.Add(new TimeSpan(24, 0, 0));
                    agency.OrderedTicketsPerCounterInInterval(start, end);
                }
                else if (service == 5)
                {
                    agency.TotalProfitOfAgency();
                }
                else
                {
                    agency.AgencySuccess();
                }
                //WriteLine($"Result: {service}");
            } while (true);
            Console.ReadKey();
        }
    }
    class Counter
    {
        private List<Ticket> orderedTickets;
        public Counter(int num)
        {
            orderedTickets = new List<Ticket>();
            Number = num;
        }
        public int Number
        {
            get; set;
        }
        public void OrderTicket(Ticket ticket)
        {
            orderedTickets.Add(ticket);
        }

        public void OrderedTicketsInInterval(DateTime start, DateTime end)
        {
            List<Ticket> ticketsInInterval = GetTicketsInInterval(start, end);
            Console.WriteLine($"Total number of ordered tickets from Counter {Number}:\t {ticketsInInterval.Count}");
            foreach (Ticket ticket in ticketsInInterval)
            {
                Console.WriteLine($"\t{ticket}");
            }
        }
        public void TotalProfitPerCounter(DateTime start, DateTime end)
        {
            long total = 0;
            List<Ticket> tickets = GetTicketsInInterval(start, end);
            foreach (Ticket ticket in tickets)
            {
                total += ticket.Price;
            }
            Console.WriteLine($"Total Profit from Counter {Number} is:\t{total}");
        }

        public List<Ticket> GetTicketsInInterval(DateTime start, DateTime end)
        {
            List<Ticket> tickets = new List<Ticket>();
            foreach (Ticket ticket in orderedTickets)
            {
                if (ticket.Time.CompareTo(start) >= 0 && ticket.Time.CompareTo(end) <= 0)
                    tickets.Add(ticket);
            }
            return tickets;
        }

    }

    class Destination : IEquatable<Destination>
    {
        public Destination(string name)
        {
            this.Name = name.ToUpper();
            this.Price = this.Name.Length * 2000;
        }
        public string Name { get; set; }
        public int Price { get; set; }
        public bool Equals(Destination other)
        {
            if (other == null) return false;
            return (this.Name.Equals(other.Name));
        }
        public override string ToString()
        {
            return $"{Name} {Price}";
        }
    }

    class Ticket
    {
        private string nameClient;
        private string surnameClient;
        private int age;

        public Ticket(string nameClient, string surnameClient, int age, Destination destination)
        {
            this.nameClient = nameClient;
            this.surnameClient = surnameClient;
            this.age = age;
            this.Dest = destination;
            this.Time = DateTime.Now;
            this.Price = Dest.Price;
        }

        public DateTime Time
        {
            get; set;
        }

        public int Price
        {
            get; set;
        }
        public Destination Dest
        {
            get; set;
        }
        public override string ToString()
        {
            return $"{nameClient} {surnameClient} {age} {Dest} {Time} {Price}";
        }
    }

    public class World
    {
        private int numCounters;
        private List<Counter> counters;
        private List<Destination> destinations;
        private int totalClients;
        private int servedClients;
        private long totalProfitOfAgency;

        public World(int numCounters)
        {
            this.numCounters = numCounters;

            this.counters = new List<Counter>();

            for (int i = 0; i < numCounters; ++i) { this.counters.Add(new Counter(i + 1)); }

            totalClients = 0;
            servedClients = 0;

            destinations = new List<Destination>();
            destinations.Add(new Destination("Rim"));
            destinations.Add(new Destination("London"));
            destinations.Add(new Destination("Tokio"));
            destinations.Add(new Destination("Sofija"));
            destinations.Add(new Destination("Istanbul"));
            destinations.Add(new Destination("Toronto"));
            destinations.Add(new Destination("Majami"));
            destinations.Add(new Destination("Zagreb"));
            destinations.Add(new Destination("Belgrad"));
            destinations.Add(new Destination("Moskva"));
        }

        public void AddDestination(string destination)
        {
            destinations.Add(new Destination(destination));
        }

        public void AddCounter(string counter)
        {
            counters.Add(new Counter(numCounters));
            numCounters++;
        }

        public void PrintDestinations()
        {
            foreach (Destination d in destinations)
            {
                Console.WriteLine(d.Name);
            }
        }

        public bool DestinationExists(string destination)
        {
            return destinations.Contains(new Destination(destination));
        }

        public void ServeTheClient()
        {
            totalClients++;
            //WriteLine("Serving a client...");
            //WriteLine("Name: ");
            string name = Console.ReadLine();
            //WriteLine("Surname: ");
            string surname = Console.ReadLine();
            //WriteLine("Age: ");
            int age = int.Parse(Console.ReadLine());

            //WriteLine("Destination: ");
            string dest = Console.ReadLine();
            int counter = int.Parse(Console.ReadLine());
            if (!DestinationExists(dest))
            {
                Console.WriteLine($"Destination {dest} not included!");
                return;
            }
            Destination d = new Destination(dest);
            counters[counter - 1].OrderTicket(new Ticket(name, surname, age, d));
            totalProfitOfAgency += d.Price;
            Console.WriteLine("####### TICKET ORDERED. ########");
            servedClients++;
        }

        public void OrderedTickets(int counter, DateTime start, DateTime end)
        {
            counters[counter - 1].OrderedTicketsInInterval(start, end);
        }

        public void TotalProfitOfCounter(int counter, DateTime start, DateTime end)
        {
            counters[counter - 1].TotalProfitPerCounter(start, end);
        }

        public void OrderedTicketsPerCounterInInterval(DateTime start, DateTime end)
        {
            foreach (Counter counter in counters)
            {
                Console.WriteLine($"Counter {counter.Number}:");
                foreach (Ticket ticket in counter.GetTicketsInInterval(start, end))
                {
                    Console.WriteLine($"\t{ticket}");
                }
            }
        }
        public void TotalProfitOfAgency()
        {
            Console.WriteLine($"Total profit of Agency is: {totalProfitOfAgency}");
        }
        public void AgencySuccess()
        {
            double success = servedClients * 1.0 / totalClients;
            Console.WriteLine($"Success of Agency: {Math.Round(success, 2)}");
        }
    }
}