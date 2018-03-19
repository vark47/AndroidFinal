using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using SQLite;

namespace Billseed
{
    class Program
    {
        private static List<Bill> bills;

        static void Main(string[] args)
        {
            Createbills();

            using (var db = new SQLiteConnection("../../../BillList/Assets/bills.db"))
            {
                if (db.CreateTable<Bill>() == 0)
                {
                    db.DeleteAll<Bill>();
                }

                db.InsertAll(bills);

                var billnames = db.Table<Bill>().ToList().Select(d => d.Name);

                Console.WriteLine("The following Bills were seeded:");

                foreach (var name in billnames)
                    Console.WriteLine(name);
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        private static void Createbills()
        {
            bills = new List<Bill>();

            Bill b = new Bill
            {
                Name = "Food",
                DayDue = 5,
                Amount = 250.55
            };
            bills.Add(b);

            b = new Bill
            {
                Name = "Rent",
                DayDue = 7,
                Amount = 950.55
            };
            bills.Add(b);

            b = new Bill
            {
                Name = "Car Payment",
                DayDue = 23,
                Amount = 250.55
            };
            bills.Add(b);

            b = new Bill
            {
                Name = "Insurance",
                DayDue = 15,
                Amount = 350.55
            };
            bills.Add(b);

            b = new Bill
            {
                Name = "Child Support",
                DayDue = 7,
                Amount = 1150.55,
                
            };
            bills.Add(b);
            b = new Bill
            {
                Name = "Car Support",
                DayDue = 31,
                Amount = 1150.55,

            };
            bills.Add(b);

            b = new Bill
            {
                Name = "Fun Money",
                DayDue = 30,
                Amount = 1150.55,

            };
            bills.Add(b);



        }
    }
}
