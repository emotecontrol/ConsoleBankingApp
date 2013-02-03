using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Assignment1
{
    class SavingsInterface
    {
        static List<SavingsAccount> customerList = new List<SavingsAccount>();
        static TextInfo ti = new CultureInfo("en-CA", false).TextInfo;
        
        static void Main(string[] args)
        {
            // this is just some tests for the address converter.  It'll get removed eventually.
            //StreetAddressConverter lookup = new StreetAddressConverter();
            //string alley = lookup.convertToAbbr("alley");
            //Console.WriteLine(alley);
            
            //string ct = lookup.convertToAddress("ct");
            //Console.WriteLine(ct);

            //string crossing = "crossing";
            //Console.WriteLine(lookup.convertToAddress("Boulevard"));
            //Console.WriteLine(lookup.convertToAbbr("Boulevard"));
            //Console.WriteLine(lookup.convertToAddress("notinthelist"));
            //Console.WriteLine(lookup.convertToAbbr("northis"));
            //Console.WriteLine(lookup.convertToAbbr("av"));
            //Console.WriteLine(lookup.convertToAddress("pl"));
            //string[] test = new string[3];
            //string teststring = "32 Blue Water Drive";
            //test = parseAddress(teststring);
            //Console.WriteLine(test[0]);
            //Console.WriteLine(test[1]);
            //Console.WriteLine(test[2]);
            
            SavingsAccount testcustomer = new SavingsAccount();
            testcustomer = newCustomer();
            List<SavingsAccount> oldlist = new List<SavingsAccount>();
            oldlist.Add(testcustomer);
            Console.WriteLine(testcustomer.LastName);
            Console.WriteLine(testcustomer.FirstName);
            
            //Console.WriteLine("The address is {0} {1} {2}", testcustomer.StreetNumber, ti.ToTitleCase(testcustomer.StreetName), ti.ToTitleCase(testcustomer.StreetType));
            SaveAndLoad save = new SaveAndLoad("save.txt");
            save.Save(oldlist);
            List<SavingsAccount> newlist = new List<SavingsAccount>();
            newlist = save.Load();
            SavingsAccount newguy = new SavingsAccount();
            newguy = newlist[0];
            Console.WriteLine(newguy.StreetName);
            
            Console.ReadKey();
        }

        static SavingsAccount newCustomer()
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
                    Console.WriteLine("\nYou entered {0}.  Is this correct? Y/N", newCust.FirstName);
                    first = confirm();
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
                    Console.WriteLine("\nYou entered {0}.  Is this correct? Y/N", newCust.LastName);
                    last = confirm();
                }
                finally
                {
                    if (lastName == null)
                    {
                        Console.WriteLine("Unable to read input.  Please try again.");
                    }

                }
                
            }
            bool strAddress = false;
            bool successInput = false;

            string streetNumber = null;
            string streetName = null;
            string streetType = null;
            string addressInput=null;
            string[] splitAddress = new string[3];
            string[] splitRR = new string[2];
            
            while (strAddress == false)
            {
                Console.WriteLine("Please enter the customer's street address.  Example: 34 Belmont Dr.");    
                try
                {
                    addressInput = Console.ReadLine(); 
                }
                finally
                {
                    

                    if (Regex.IsMatch(addressInput, @"[rR]\W?[rR]\W?.*\d"))  // test for a rural route address
                    {
                        if (Regex.IsMatch(addressInput, @"^([-\d]+) ([\w ']+) (\w+)\.?"))// test whether there is also a street address
                        {
                            splitAddress = parseAddress(addressInput); // extract street address
                            splitRR = parseRR(addressInput); // extract RR address (e.g. RR 6 Stn Main)
                            if (splitAddress[0] != null && splitAddress[1] != null && splitAddress[2] != null
                                && splitRR[0] != null && splitRR[1] != null)
                            {
                                // Assign address parts to the SavingsAccount object
                                newCust.StreetNumber = splitAddress[0];
                                newCust.StreetName = splitAddress[1];
                                StreetAddressConverter convert = new StreetAddressConverter();

                                newCust.StreetType = convert.convertToAbbr(splitAddress[2]);
                                newCust.Rroute = splitRR[0];
                                newCust.RrStation = splitRR[1];
                                successInput = true;
                            }
                            else
                            {
                                Console.WriteLine("There was a problem with your input.  Please try again.");
                            }
                        }
                        else // there is only a rr address
                        {
                            splitRR = parseRR(addressInput); // extract RR address (e.g. RR 6 Stn Main)
                            if (splitRR[0] != null && splitRR[1] != null)
                            {
                                // Assign address parts to the SavingsAccount object

                                newCust.Rroute = splitRR[0];
                                newCust.RrStation = splitRR[1];
                                successInput = true;
                            }
                            else
                            {
                                Console.WriteLine("There was a problem with your input.  Please try again.");
                            }

                        }
                         
                    }
                    else
                    {
                        //extract street address, divide into parts
                        splitAddress = parseAddress(addressInput);
                        
                        
                        if (splitAddress[0] != null && splitAddress[1] != null && splitAddress[2] != null)
                        {
                            newCust.StreetNumber = splitAddress[0];
                            newCust.StreetName = splitAddress[1];
                            newCust.StreetNumber = splitAddress[0];
                            newCust.StreetName = splitAddress[1];
                            StreetAddressConverter convert = new StreetAddressConverter();
                            Console.WriteLine(convert.convertToAbbr("Street"));
                            string convertme = splitAddress[2];
                            string streetAbbr = convert.convertToAbbr(convertme);
                            newCust.StreetType = streetAbbr;
                            successInput = true;
                        }
                        else
                        {
                            Console.WriteLine("There was a problem with your input.  Please try again.");
                        }
                    }
                
                    
                    if (successInput == false)
                    {
                        Console.WriteLine("No address was entered.  Please try again.");
                    }
                    else if (newCust.Rroute == null) // there is no rr
                    {
                        Console.WriteLine("\nYou entered {0} {1} {2}.  Is this correct? Y/N", newCust.StreetNumber, ti.ToTitleCase(newCust.StreetName), ti.ToTitleCase(newCust.StreetType));
                        strAddress = confirm();
                    }
                    else if (newCust.StreetName == null) // there is no street address
                    {
                        Console.WriteLine("\nYou entered {0} {1}.  Is this correct? Y/N", ti.ToTitleCase(newCust.Rroute), ti.ToTitleCase(newCust.RrStation));
                        strAddress = confirm();
                    }
                    else // there is both a street address and a rr
                    {
                        Console.WriteLine("\nYou entered {0} {1} {2} {3} {4}.  Is this correct? Y/N", newCust.StreetNumber, ti.ToTitleCase(newCust.StreetName), ti.ToTitleCase(newCust.StreetType), ti.ToTitleCase(newCust.Rroute), ti.ToTitleCase(newCust.RrStation));
                        strAddress = confirm();
                        
                    }
                    
                }
            }


            return newCust;
        }
        public static string[] parseAddress(string address) // address is an address of the type "36 Blueberry Lane"
        {
            string[] addressArray = new string[3];
            
            address = address.ToLower();
            Match match = Regex.Match(address, @"([-\d]+) ([\w ']+) (\w+)(?= [rR]\W?[rR]\W?\d|\.)|([-\d]+) ([\w ']+) (\w+)");
            if (match.Success)
            {
                if (match.Groups[1].Success) // if it's either a street address followed by a . or a street address
                                             // plus rr address
                {
                    addressArray[0] = match.Groups[1].Value;
                    addressArray[1] = match.Groups[2].Value;
                    addressArray[2] = match.Groups[3].Value;
                }
                else // it's a street address with no . at the end
                {
                    addressArray[0] = match.Groups[4].Value;
                    addressArray[1] = match.Groups[5].Value;
                    addressArray[2] = match.Groups[6].Value;
                    
                }
                

            }
            return addressArray;
            
        }
        public static string[] parseRR(string address) // address is an address of the type "RR 5 Station Main"
        {
            string[] addressArray = new string[2];

            address = address.ToLower();
            Match match = Regex.Match(address, @"(?:^|\W)([rR]\W?[rR]\W?.*\d+) ?([Ss]t\w+\s+\w+)");
            if (match.Success)
            {
                addressArray[0] = match.Groups[1].Value;
                addressArray[1] = match.Groups[2].Value;
                
            }
            return addressArray;
            
        }

        static bool confirm()
        {
            bool askYN = false;
            bool confirmation = false;
            while (askYN == false)
            {
                ConsoleKeyInfo keyYN = Console.ReadKey(true);
                string strYN = keyYN.Key.ToString();

                if (strYN == "y" || strYN == "Y")
                {
                    askYN = true;
                    confirmation = true;
                }
                if (strYN == "n" || strYN == "N")
                {
                    askYN = true;
                }

            }
            return confirmation;
        }
    }
}
