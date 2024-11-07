using System;
using System.Collections.Generic;

namespace HotelManagementSystem
{
    class Customer
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public float PaymentAdvance { get; set; }
        public int BookingId { get; set; }
    }

    class Room
    {
        public char Type { get; set; }
        public char SizeType { get; set; }
        public char AcType { get; set; }
        public int RoomNumber { get; set; }
        public int Rent { get; set; }
        public bool Status { get; set; }
        public Customer Cust { get; set; }

        public Room AddRoom(int roomNumber)
        {
            Room room = new Room { RoomNumber = roomNumber };
            Console.Write("\nType AC/Non-AC (A for AC, N for Non-AC): ");
            room.AcType = Convert.ToChar(Console.ReadLine().ToUpper());

            Console.Write("\nType Comfort (S for Superior, N for Normal): ");
            room.Type = Convert.ToChar(Console.ReadLine().ToUpper());

            Console.Write("\nType Size (B for Big, S for Small): ");
            room.SizeType = Convert.ToChar(Console.ReadLine().ToUpper());

            Console.Write("\nDaily Rent: ");
            room.Rent = Convert.ToInt32(Console.ReadLine());

            room.Status = false;
            Console.WriteLine("\nRoom Added Successfully!");
            return room;
        }

        public void DisplayRoom(Room room)
        {
            Console.WriteLine($"\nRoom Number: {room.RoomNumber}");
            Console.WriteLine($"Type AC/Non-AC: {room.AcType}");
            Console.WriteLine($"Type Comfort: {room.Type}");
            Console.WriteLine($"Type Size: {room.SizeType}");
            Console.WriteLine($"Daily Room Rent: {room.Rent}");
        }
    }

    class HotelManagement : Room
    {
        private List<Room> rooms = new List<Room>();

        public void CheckIn()
        {
            Console.Write("\nEnter Room Number: ");
            int roomNumber = Convert.ToInt32(Console.ReadLine());

            Room room = rooms.Find(r => r.RoomNumber == roomNumber);

            if (room == null)
            {
                Console.WriteLine("Room not found.");
                return;
            }

            if (room.Status)
            {
                Console.WriteLine("\nRoom is already Booked");
                return;
            }

            room.Cust = new Customer();

            Console.Write("\nEnter Booking ID: ");
            room.Cust.BookingId = Convert.ToInt32(Console.ReadLine());

            Console.Write("\nEnter Customer Name: ");
            room.Cust.Name = Console.ReadLine();

            Console.Write("\nEnter Address (only city): ");
            room.Cust.Address = Console.ReadLine();

            Console.Write("\nEnter Phone: ");
            room.Cust.Phone = Console.ReadLine();

            Console.Write("\nEnter From Date: ");
            room.Cust.FromDate = Console.ReadLine();

            Console.Write("\nEnter To Date: ");
            room.Cust.ToDate = Console.ReadLine();

            Console.Write("\nEnter Advance Payment: ");
            room.Cust.PaymentAdvance = Convert.ToSingle(Console.ReadLine());

            room.Status = true;
            Console.WriteLine("\nCustomer Checked-in Successfully...");
        }

        public void GetAvailableRooms()
        {
            bool found = false;
            foreach (Room room in rooms)
            {
                if (!room.Status)
                {
                    DisplayRoom(room);
                    found = true;
                    Console.WriteLine("\nPress enter for the next room...");
                    Console.ReadLine();
                }
            }

            if (!found)
            {
                Console.WriteLine("\nAll rooms are reserved.");
            }
        }

        public void CheckOut(int roomNumber)
        {
            Room room = rooms.Find(r => r.RoomNumber == roomNumber);

            if (room == null || !room.Status)
            {
                Console.WriteLine("Room is not booked or not found.");
                return;
            }

            Console.Write("\nEnter Number of Days: ");
            int days = Convert.ToInt32(Console.ReadLine());

            float billAmount = days * room.Rent;
            Console.WriteLine("\n######## CheckOut Details ########");
            Console.WriteLine($"Customer Name: {room.Cust.Name}");
            Console.WriteLine($"Room Number: {room.RoomNumber}");
            Console.WriteLine($"Address: {room.Cust.Address}");
            Console.WriteLine($"Phone: {room.Cust.Phone}");
            Console.WriteLine($"Total Amount Due: {billAmount}");
            Console.WriteLine($"Advance Paid: {room.Cust.PaymentAdvance}");
            Console.WriteLine($"Total Payable: {billAmount - room.Cust.PaymentAdvance}");

            room.Status = false;
            room.Cust = null; 
            Console.WriteLine("\nCheck-out completed. Room is now available.");
        }

        public void GuestSummaryReport()
        {
            foreach (Room room in rooms)
            {
                if (room.Status && room.Cust != null)
                {
                    Console.WriteLine("\n######## Guest Summary Report ########");
                    Console.WriteLine($"Customer Name: {room.Cust.Name}");
                    Console.WriteLine($"Room Number: {room.RoomNumber}");
                    Console.WriteLine($"Address: {room.Cust.Address}");
                    Console.WriteLine($"Phone: {room.Cust.Phone}");
                    Console.WriteLine("---------------------------------------");
                }
            }
        }

        public void SearchCustomer(string name)
        {
            bool found = false;
            foreach (Room room in rooms)
            {
                if (room.Status && room.Cust != null && room.Cust.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"\nCustomer Name: {room.Cust.Name}");
                    Console.WriteLine($"Room Number: {room.RoomNumber}");
                    Console.WriteLine($"Address: {room.Cust.Address}");
                    Console.WriteLine($"Phone: {room.Cust.Phone}");
                    Console.WriteLine("---------------------------------------");
                    found = true;
                }
            }
            if (!found)
            {
                Console.WriteLine("Person not found.");
            }
        }

        public void ManageRooms()
        {
            while (true)
            {
                Console.WriteLine("\n### Manage Rooms ###");
                Console.WriteLine("1. Add Room");
                Console.WriteLine("2. Search Room");
                Console.WriteLine("3. Back to Main Menu");
                Console.Write("\nEnter Option: ");
                int option = Convert.ToInt32(Console.ReadLine());

                if (option == 3) break;

                switch (option)
                {
                    case 1:
                        Console.Write("\nEnter Room Number: ");
                        int roomNumber = Convert.ToInt32(Console.ReadLine());
                        if (rooms.Exists(r => r.RoomNumber == roomNumber))
                        {
                            Console.WriteLine("\nRoom Number is already present. Please enter a unique number.");
                        }
                        else
                        {
                            rooms.Add(AddRoom(roomNumber));
                        }
                        break;

                    case 2:
                        Console.Write("\nEnter Room Number: ");
                        int searchNumber = Convert.ToInt32(Console.ReadLine());
                        Room room = rooms.Find(r => r.RoomNumber == searchNumber);
                        if (room != null)
                        {
                            Console.WriteLine(room.Status ? "\nRoom is Reserved" : "\nRoom is Available");
                            DisplayRoom(room);
                        }
                        else
                        {
                            Console.WriteLine("Room not found.");
                        }
                        break;

                    default:
                        Console.WriteLine("Please enter a valid option.");
                        break;
                }
            }
        }
    }

    class Program
    {
        static void Main()
        {
            HotelManagement hotel = new HotelManagement();

            while (true)
            {
                Console.WriteLine("\n######## Hotel Management System #########");
                Console.WriteLine("1. Manage Rooms");
                Console.WriteLine("2. Check-In Room");
                Console.WriteLine("3. Available Rooms");
                Console.WriteLine("4. Search Customer");
                Console.WriteLine("5. Check-Out Room");
                Console.WriteLine("7. Exit");
                Console.Write("\nEnter Option: ");
                int option = Convert.ToInt32(Console.ReadLine());

                if (option == 7) break;

                switch (option)
                {
                    case 1:
                        hotel.ManageRooms();
                        break;

                    case 2:
                        hotel.CheckIn();
                        break;

                    case 3:
                        hotel.GetAvailableRooms();
                        break;

                    case 4:
                        Console.Write("Enter Customer Name: ");
                        string name = Console.ReadLine();
                        hotel.SearchCustomer(name);
                        break;

                    case 5:
                        Console.Write("Enter Room Number: ");
                        int roomNumber = Convert.ToInt32(Console.ReadLine());
                        hotel.CheckOut(roomNumber);
                        break;

                    default:
                        Console.WriteLine("Please enter a valid option.");
                        break;
                }
            }

            Console.WriteLine("\nTHANK YOU!HotelManagement");
        }
    }
}