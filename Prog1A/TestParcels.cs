// Program 1B
// CIS 200-01/76
// Fall 2016
// Due: 10/17/2016
// By: Andrew L. Wright (students use Grading ID)
// By: C6181
// File: TestParcels.cs
// This is a simple, console application designed to exercise the Parcel hierarchy.
// It creates several different Parcels and prints them.
//
//Description: This program uses LINQ to produce reports on the parcels
//and displays the list sorted accordingly to the console 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prog1
{
    class TestParcels
    {
        // Precondition:  None
        // Postcondition: Parcels have been created and displayed
        static void Main(string[] args)
        {
            // Test Data - Magic Numbers OK
            Address a1 = new Address("John Smith", "123 Any St.", "Apt. 45",
                "Louisville", "KY", 40202); // Test Address 1
            Address a2 = new Address("Jane Doe", "987 Main St.", "",
                "Beverly Hills", "CA", 90210); // Test Address 2
            Address a3 = new Address("James Kirk", "654 Roddenberry Way", "Suite 321",
                "El Paso", "TX", 79901); // Test Address 3
            Address a4 = new Address("John Crichton", "678 Pau Place", "Apt. 7",
                "Portland", "ME", 04101); // Test Address 4
            Address a5 = new Address("Luke Sky", "698 Force St.", "Apt. 3",
                          "Las Vegas", "NV", 30202); // Test Address 5
            Address a6 = new Address("Frodo Bag", "124 Bagshot St.", "",
                "San Francisco", "CA", 80410); // Test Address 6
            Address a7 = new Address("Walt Whit", "621 Blue Way", "Suite 001",
                "Albuquerque", "New Mexico", 59901); // Test Address 7
            Address a8 = new Address("John Dorian", "678 Sacred Heart", "Apt. 8",
                "Phoenix", "AZ", 18101); // Test Address 8

            Letter letter1 = new Letter(a1, a2, 3.95M);                            // Letter test object
            GroundPackage gp1 = new GroundPackage(a3, a4, 14, 10, 5, 12.5);        // Ground test object
            NextDayAirPackage ndap1 = new NextDayAirPackage(a1, a3, 25, 15, 15,    // Next Day test object
                85, 7.50M);
            TwoDayAirPackage tdap1 = new TwoDayAirPackage(a4, a1, 46.5, 39.5, 28.0, // Two Day test object
                80.5, TwoDayAirPackage.Delivery.Saver);
            TwoDayAirPackage tdap2 = new TwoDayAirPackage(a6, a8, 80, 20, 24.0, // Two Day test object
                92.5, TwoDayAirPackage.Delivery.Early);
            GroundPackage gp2 = new GroundPackage(a7, a5, 12, 15, 8, 10);   // Ground test object
            Letter letter2 = new Letter(a1, a8, 5.80M);
            NextDayAirPackage ndap2 = new NextDayAirPackage(a7, a6, 28, 11, 14,    // Next Day test object
                105, 10.75M);
            List<Parcel> parcels;      // List of test parcels


            parcels = new List<Parcel>();

            // Add all the objects  to the parcels list
            parcels.Add(letter1); // Populate list
            parcels.Add(gp1);
            parcels.Add(ndap1);
            parcels.Add(tdap1);
            parcels.Add(letter2); 
            parcels.Add(gp2);
            parcels.Add(ndap2);
            parcels.Add(tdap2);
            Console.WriteLine("Original List:");
            Console.WriteLine("====================");
            foreach (Parcel p in parcels)
            {
                Console.WriteLine(p);
                Console.WriteLine("====================");
            }

            // create Linq to sort parcels by destination
            var sortDestination =
               from p in parcels
               orderby p.DestinationAddress.Zip descending   // destination zip from highest to lowest
               select p;
            // Display list sorted by destination zip
            Console.WriteLine("\nSorted Data by Destination Zip\n");      // header
            Console.WriteLine("----- -------------------- ----- ------"); // line
            foreach (var p in sortDestination)  // for each p in the query
            {
                Console.WriteLine(p.DestinationAddress.Zip);           // display destinationzip
                Console.WriteLine();
            }
            Pause(); // press enter to continue
            // create query to sort the list data by cost
            var sortCost =
               from p in parcels
               orderby p.CalcCost() 
               select p;
            // Display list sorted by cost
            Console.WriteLine("\nSorted Data by Cost\n"); // header
            Console.WriteLine("----- -------------------- ----- ------"); // line
            // loop to display each object in the list sorted by the query
            foreach (var p in sortCost)
            {
                Console.WriteLine(p.CalcCost().ToString("C")); // display Cost in currency format
                Console.WriteLine();
            }
            Pause(); // press enter to continue
            // Create query that sorts the list by package type and cost
            var sortDeliveryAndCost =
              from p in parcels
              orderby p.GetType().ToString(), p.CalcCost() descending // sort by type then by cost
              select p;
            // Display list sorted by type and cost
            Console.WriteLine("\nSorted Data by DeliveryType and Cost\n"); // header
            Console.WriteLine("----- -------------------- ----- ------");  // line
            // loop to display each object in the list sorted by deliverytype and cost
            foreach (var p in sortDeliveryAndCost)
            {
                Console.Write(p.GetType().ToString());        // only show delivery type
                Console.WriteLine(p.CalcCost().ToString("C"));// and cost
                Console.WriteLine();                          // new line
            }
            Pause(); // press enter to continue
            // sorts the list by only displaying heavy airpackages
            var heavyAirPackage =
            from p in parcels
            let package = p as AirPackage            // uses as to cast variable package equal to p as AirPackage
            where (package!=null)&&package.IsHeavy() // if cast works and not null select where air package is heavy
            orderby package.Weight descending        // can now access weight through package
            select package;

            // Display list sorted by heavy air packages
            Console.WriteLine("\nSorted Data by Heavy Objects and Weight\n"); // header
                Console.WriteLine("----- -------------------- ----- ------"); // line
            // loop that steps through each object and display its' weight 
                foreach (var p in heavyAirPackage)
                {
                    Console.Write(p.IsHeavy()); // display heavy
                Console.WriteLine(p.Weight);    // display weight
                    Console.WriteLine();
                }
                Pause(); // press enter to continue    
        }

        // Precondition:  None
        // Postcondition: Pauses program execution until user presses Enter and
        //                then clears the screen
        public static void Pause()
        {
            Console.WriteLine("Press Enter to Continue...");
            Console.ReadLine();

            Console.Clear(); // Clear screen
        }
        
    }
}
