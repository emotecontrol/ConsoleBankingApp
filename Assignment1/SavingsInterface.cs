using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment1
{
    class SavingsInterface
    {
        List<SavingsAccount> customerList;

        
        static void Main(string[] args)
        {
            // this is just some tests for the address converter.  It'll get removed eventually.
            StreetAddressConverter lookup = new StreetAddressConverter();
            string alley = lookup.convertToAbbr("alley");
            Console.WriteLine(alley);
            
            string ct = lookup.convertToAddress("ct");
            Console.WriteLine(ct);

            string crossing = "crossing";
            Console.WriteLine(lookup.convertToAddress(crossing));
            Console.WriteLine(lookup.convertToAbbr(crossing));
            Console.WriteLine(lookup.convertToAddress("notinthelist"));
            Console.WriteLine(lookup.convertToAbbr("northis"));
            Console.WriteLine(lookup.convertToAbbr("av"));
            Console.WriteLine(lookup.convertToAddress("pl"));
            Console.ReadKey();
        }

        /*SavingsAccount*/ void newCustomer()
        {
            SavingsAccount newCust = new SavingsAccount(); 
            Console.WriteLine("Welcome to Customer Creation.  To create a new customer entry, please follow the instructions.");
            
            bool first = false;
            string firstName = null;
            while (first == false)
            {
                Console.Write("Please enter the customer's first name: ");
                try
                {
                    firstName = Console.ReadLine();
                    newCust.FirstName = firstName.Trim();
                    bool askYN = false;
                    Console.WriteLine("\nYou entered {0}.  Is this correct? Y/N", newCust.FirstName);
                    while (askYN == false)
                    {
                        ConsoleKeyInfo keyYN = Console.ReadKey();
                        string strYN = keyYN.ToString();
                        int getYN;
                        int.TryParse(strYN, out getYN);
                        if (getYN == 89 || getYN == 121)
                        {
                            askYN = true;
                            first = true;
                        }
                        if (getYN == 78 || getYN == 110)
                        {
                            askYN = true;
                        }
                        
                    }


                }
                finally
                {
                    if (firstName == null)
                    {
                        Console.WriteLine("Unable to read input.  Please try again.");
                    }
                    
                }
                
            }
            bool last = false;
            string lastName = null;
            while (last == false)
            {
                Console.Write("Please enter the customer's last name: ");
                try
                {
                    lastName = Console.ReadLine();
                    newCust.LastName = lastName.Trim();
                    bool askYN = false;
                    Console.WriteLine("\nYou entered {0}.  Is this correct? Y/N", newCust.LastName);
                    while (askYN == false)
                    {
                        ConsoleKeyInfo keyYN = Console.ReadKey();
                        string strYN = keyYN.ToString();
                        int getYN;
                        int.TryParse(strYN, out getYN);
                        if (getYN == 89 || getYN == 121)
                        {
                            askYN = true;
                            last = true;
                        }
                        if (getYN == 78 || getYN == 110)
                        {
                            askYN = true;
                        }

                    }


                }
                finally
                {
                    if (lastName == null)
                    {
                        Console.WriteLine("Unable to read input.  Please try again.");
                    }

                }
                
            }
            bool strNum = false;
            bool strName = false;
            bool strType = false;
            int streetNumber = 0;
            string streetName = null;
            string streetType = null;
            Console.WriteLine("Please enter the customer's address.  Example: 34 Belmont Dr.");
            while (strNum == false || strName == false || strType == false)
            {
                
            }
        
        
        
        }
    }
}
