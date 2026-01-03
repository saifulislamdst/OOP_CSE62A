using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TrainTicketManagementSystem
{
    // ========================= INTERFACE =========================
    // Demonstrates INTERFACE
    // Any class that takes payment must implement fare calculation
    interface IPayable
    {
        double CalculateFare();
    }

    // ========================= BASE CLASS =========================
    // Demonstrates INHERITANCE (base class)
    class Person
    {
        public string Name;

        // Constructor
        public Person(string name)
        {
            Name = name;
        }
    }

    // ========================= PASSENGER =========================
    // Inherits Person
    class PassengerUser : Person
    {
        public string Username;
        public string Password;

        // Constructor
        public PassengerUser(string name, string username, string password)
            : base(name)
        {
            Username = username;
            Password = password;
        }
    }

    // ========================= ADMIN =========================
    // Inherits Person
    class Employee : Person
    {
        public string Username;
        public string Password;

        public Employee(string name, string username, string password)
            : base(name)
        {
            Username = username;
            Password = password;
        }
    }

    // ========================= TRAIN =========================
    class Train
    {
        public int TrainNo;
        public string TrainName;
        public string Source;
        public string Destination;
        public string Time;
        public double Fare;
        public int TotalSeats;
        public int SeatsLeft;

        // Stores all seat numbers
        public List<string> Seats = new List<string>();

        // Prevents duplicate seat booking
        public HashSet<string> BookedSeats = new HashSet<string>();

        // Constructor
        public Train(int no, string name, string src, string dest,
                     string time, double fare, int seats)
        {
            TrainNo = no;
            TrainName = name;
            Source = src;
            Destination = dest;
            Time = time;
            Fare = fare;
            TotalSeats = seats;
            SeatsLeft = seats;
            GenerateSeats();
        }

        // COPY CONSTRUCTOR (OOP requirement)
        public Train(Train t)
        {
            TrainNo = t.TrainNo;
            TrainName = t.TrainName;
            Source = t.Source;
            Destination = t.Destination;
            Time = t.Time;
            Fare = t.Fare;
        }

        // Auto-generate seat numbers
        void GenerateSeats()
        {
            string[] tags = { "LW", "RW", "FR", "MID", "BK" };
            int num = 1;

            while (Seats.Count < TotalSeats)
            {
                foreach (string tag in tags)
                {
                    if (Seats.Count >= TotalSeats) break;
                    Seats.Add(tag + num);
                }
                num++;
            }
        }

        // Show seat layout clearly
        public void ShowSeatLayout()
        {
            Console.WriteLine("\nSeat Guide:");
            Console.WriteLine("LW = Left Window | RW = Right Window | FR = Front");
            Console.WriteLine("MID = Middle | BK = Back | X = Booked\n");

            foreach (string seat in Seats)
            {
                if (BookedSeats.Contains(seat))
                    Console.Write($"[{seat}-X] ");
                else
                    Console.Write($"[{seat}] ");
            }
            Console.WriteLine();
        }

        public bool IsSeatValid(string seat)
        {
            return Seats.Contains(seat) && !BookedSeats.Contains(seat);
        }

        public void BookSeat(string seat)
        {
            BookedSeats.Add(seat);
            SeatsLeft--;
        }

        public void CancelSeat(string seat)
        {
            BookedSeats.Remove(seat);
            SeatsLeft++;
        }
    }

    // ========================= PASSENGER INFO =========================
    class Passenger
    {
        public string Name;
        public string Seat;

        public Passenger(string name, string seat)
        {
            Name = name;
            Seat = seat;
        }
    }

    // ========================= TICKET =========================
    class Ticket : IPayable
    {
        // STATIC FIELD
        public static int PnrCounter = 1001;

        public int PNR;
        public Train TrainInfo;
        public Passenger PassengerInfo;
        public string Username;
        public string TransactionID;

        // Constructor
        public Ticket(Train train, Passenger passenger,
                      string username, string transactionId)
        {
            PNR = PnrCounter++;
            TrainInfo = new Train(train); // COPY CONSTRUCTOR used
            PassengerInfo = passenger;
            Username = username;
            TransactionID = transactionId;
        }

        // INTERFACE METHOD
        public double CalculateFare()
        {
            return TrainInfo.Fare;
        }

        // OPERATOR OVERLOADING
        public static bool operator ==(Ticket a, Ticket b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;
            return a.PNR == b.PNR;
        }
        public static bool operator !=(Ticket a, Ticket b)
        {
            return !(a == b);
        }
        public override bool Equals(object obj) => base.Equals(obj);
        public override int GetHashCode() => base.GetHashCode();
    }

    // ========================= SYSTEM STORAGE =========================
    class RailwaySystem
    {
        public static List<Train> Trains = new List<Train>();
        public static List<Ticket> Tickets = new List<Ticket>();
        public static List<PassengerUser> Users = new List<PassengerUser>();

        // STATIC METHOD
        public static void ShowTrains()
        {
            Console.WriteLine("\nAvailable Trains:");
            foreach (Train t in Trains)
            {
                Console.WriteLine(
                    $"{t.TrainNo} | {t.TrainName} | {t.Source} → {t.Destination} | " +
                    $"{t.Time} | Fare:{t.Fare} | Seats:{t.SeatsLeft}/{t.TotalSeats}");
            }
        }

        // FUNCTION OVERLOADING
        public static void ShowTrains(string src, string dest)
        {
            foreach (Train t in Trains)
                if (t.Source == src && t.Destination == dest)
                    Console.WriteLine($"{t.TrainNo} | {t.TrainName} | {t.Time}");
        }
    }

    // ========================= MAIN PROGRAM =========================
    class Program
    {
        static void Main()
        {
            // Default admin
            Employee admin = new Employee("Admin", "admin", "admin123");

            // Default passenger
            RailwaySystem.Users.Add(
                new PassengerUser("Default Passenger", "passenger", "1234"));

            // Bangladesh train routes
            RailwaySystem.Trains.Add(new Train(101, "InterCity", "Dhaka", "Chittagong", "08:00 AM", 500, 20));
            RailwaySystem.Trains.Add(new Train(102, "Silk City", "Dhaka", "Rajshahi", "07:30 AM", 550, 18));
            RailwaySystem.Trains.Add(new Train(103, "Parabat", "Dhaka", "Sylhet", "06:45 AM", 600, 16));

            // ========================= MAIN LOOP =========================
            while (true)
            {
                Console.WriteLine("\nMAIN MENU");
                Console.WriteLine("1. Admin Login");
                Console.WriteLine("2. Passenger Login");
                Console.WriteLine("3. Passenger Registration");
                Console.WriteLine("4. Exit");
                Console.Write("Enter Choice (number): ");

                int choice = int.Parse(Console.ReadLine());

                // EXIT
                if (choice == 4)
                {
                    Console.WriteLine("Thank you for using Railway Ticket Management System.");
                    break;
                }

                // ========================= ADMIN LOGIN =========================
                if (choice == 1)
                {
                    Console.Write("Admin Username: ");
                    string u = Console.ReadLine();
                    Console.Write("Admin Password: ");
                    string p = Console.ReadLine();

                    if (u != admin.Username || p != admin.Password)
                    {
                        Console.WriteLine("Invalid Admin Login.");
                        continue;
                    }

                    int ac;
                    do
                    {
                        Console.WriteLine("\nADMIN DASHBOARD");
                        Console.WriteLine("1. View Trains");
                        Console.WriteLine("2. View All Tickets");
                        Console.WriteLine("3. View Total Sales");
                        Console.WriteLine("4. Logout");
                        Console.Write("Enter Choice (number): ");

                        ac = int.Parse(Console.ReadLine());

                        if (ac == 1)
                            RailwaySystem.ShowTrains();

                        else if (ac == 2)
                        {
                            if (RailwaySystem.Tickets.Count == 0)
                                Console.WriteLine("No tickets booked yet.");
                            else
                                foreach (Ticket t in RailwaySystem.Tickets)
                                    Console.WriteLine($"PNR:{t.PNR} | Passenger:{t.PassengerInfo.Name} | Seat:{t.PassengerInfo.Seat} | Txn:{t.TransactionID}");
                        }

                        else if (ac == 3)
                        {
                            double total = 0;
                            foreach (Ticket t in RailwaySystem.Tickets)
                                total += t.CalculateFare();

                            Console.WriteLine($"Total Tickets Sold: {RailwaySystem.Tickets.Count}");
                            Console.WriteLine($"Total Sales Amount: {total} BDT");
                        }

                    } while (ac != 4);
                }

                // ========================= PASSENGER LOGIN =========================
                else if (choice == 2)
                {
                    Console.Write("Username: ");
                    string u = Console.ReadLine();
                    Console.Write("Password: ");
                    string p = Console.ReadLine();

                    PassengerUser user = RailwaySystem.Users
                        .Find(x => x.Username == u && x.Password == p);

                    if (user == null)
                    {
                        Console.WriteLine("Invalid Passenger Login.");
                        continue;
                    }

                    int pc;
                    do
                    {
                        Console.WriteLine($"\nPASSENGER DASHBOARD ({user.Username})");
                        Console.WriteLine("1. View Trains");
                        Console.WriteLine("2. Book Ticket");
                        Console.WriteLine("3. View My Ticket");
                        Console.WriteLine("4. Cancel Ticket");
                        Console.WriteLine("5. Logout");
                        Console.Write("Enter Choice (number): ");

                        pc = int.Parse(Console.ReadLine());

                        if (pc == 1)
                            RailwaySystem.ShowTrains();

                        else if (pc == 2)
                        {
                            RailwaySystem.ShowTrains();
                            Console.Write("Enter Train No: ");
                            int no = int.Parse(Console.ReadLine());

                            Train tr = RailwaySystem.Trains.Find(t => t.TrainNo == no);
                            if (tr == null || tr.SeatsLeft == 0)
                            {
                                Console.WriteLine("Invalid train or no seats left.");
                                continue;
                            }

                            tr.ShowSeatLayout();
                            Console.Write("Enter Seat (example LW1): ");
                            string seat = Console.ReadLine();

                            if (!Regex.IsMatch(seat, @"^(LW|RW|FR|MID|BK)\d+$") || !tr.IsSeatValid(seat))
                            {
                                Console.WriteLine("Invalid or already booked seat.");
                                continue;
                            }

                            Console.Write("Enter Transaction ID: ");
                            string txn = Console.ReadLine();

                            tr.BookSeat(seat);
                            Ticket tk = new Ticket(tr,
                                new Passenger(user.Name, seat),
                                user.Username,
                                txn);

                            RailwaySystem.Tickets.Add(tk);

                            Console.WriteLine("\nTicket Booked Successfully!");
                            Console.WriteLine($"Your PNR: {tk.PNR}");
                            Console.WriteLine("Please collect your PNR for future reference.");
                        }

                        else if (pc == 3)
                        {
                            bool found = false;
                            foreach (Ticket t in RailwaySystem.Tickets)
                            {
                                if (t.Username == user.Username)
                                {
                                    found = true;
                                    Console.WriteLine($"PNR:{t.PNR} | {t.TrainInfo.Source} → {t.TrainInfo.Destination} | Seat:{t.PassengerInfo.Seat}");
                                }
                            }
                            if (!found)
                                Console.WriteLine("You have not booked any ticket yet.");
                        }

                        else if (pc == 4)
                        {
                            Console.Write("Enter PNR: ");
                            int pnr = int.Parse(Console.ReadLine());

                            Ticket tk = RailwaySystem.Tickets
                                .Find(t => t.PNR == pnr && t.Username == user.Username);

                            if (tk == null)
                            {
                                Console.WriteLine("Ticket not found.");
                                continue;
                            }

                            Train tr = RailwaySystem.Trains
                                .Find(x => x.TrainNo == tk.TrainInfo.TrainNo);

                            tr.CancelSeat(tk.PassengerInfo.Seat);
                            RailwaySystem.Tickets.Remove(tk);

                            Console.WriteLine("Ticket Cancelled Successfully.");
                        }

                    } while (pc != 5);
                }

                // ========================= REGISTRATION =========================
                else if (choice == 3)
                {
                    Console.Write("Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Username: ");
                    string u = Console.ReadLine();
                    Console.Write("Password: ");
                    string p = Console.ReadLine();

                    RailwaySystem.Users.Add(new PassengerUser(name, u, p));
                    Console.WriteLine("Passenger Registered Successfully.");
                }
            }
        }
    }
}

