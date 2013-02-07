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
        StringComparer ordICCmp = StringComparer.OrdinalIgnoreCase;

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
                

            

            

            //var fonts = ConsoleHelper.ConsoleFonts;
            //for (int f = 0; f < fonts.Length; f++)
            //    Console.WriteLine("{0}: X={1}, Y={2}",
            //       fonts[f].Index, fonts[f].SizeX, fonts[f].SizeY);
            //ConsoleHelper.SetConsoleFont(13);
              
            
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
            
            //SavingsAccount testcustomer = new SavingsAccount();
            //testcustomer = newCustomer();
            //List<SavingsAccount> oldlist = new List<SavingsAccount>();
            //oldlist.Add(testcustomer);
            ////Console.WriteLine(testcustomer.LastName);
            ////Console.WriteLine(testcustomer.FirstName);
            
            ////Console.WriteLine("The address is {0} {1} {2}", testcustomer.StreetNumber, ti.ToTitleCase(testcustomer.StreetName), ti.ToTitleCase(testcustomer.StreetType));
            //SaveAndLoad save = new SaveAndLoad("save.txt");
            //save.Save(oldlist);
            //List<SavingsAccount> newlist = new List<SavingsAccount>();
            //newlist = save.Load();
            //SavingsAccount newguy = new SavingsAccount();
            //newguy = newlist[0];
            //Console.WriteLine(newguy.Interest);
            
            //Console.ReadKey();
            
            displayMenu();
        }

        static SavingsAccount newCustomer()
        {
            SavingsAccount newCust = new SavingsAccount(); 
            Console.WriteLine("Welcome to Customer Creation.\nTo create a new customer entry, please follow the instructions.\n");
            
            bool first = false;
            string firstName = null;
            while (first == false)
            {
                Console.Write("Please enter the customer's first name: ");
                try
                {
                    firstName = Console.ReadLine();
                    newCust.FirstName = firstName.Trim();
                    Console.Write("You entered {0}.  Is this correct? Y/N\n", newCust.FirstName);
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
                    Console.Write("You entered {0}.  Is this correct? Y/N\n", newCust.LastName);
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

            //string streetNumber = null;
            //string streetName = null;
            //string streetType = null;
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
                        Console.Write("\nYou entered {0} {1} {2}.  Is this correct? Y/N\n", newCust.StreetNumber, ti.ToTitleCase(newCust.StreetName), ti.ToTitleCase(newCust.StreetType));
                        strAddress = confirm();
                    }
                    else if (newCust.StreetName == null) // there is no street address
                    {
                        Console.Write("\nYou entered {0} {1}.  Is this correct? Y/N\n", ti.ToTitleCase(newCust.Rroute), ti.ToTitleCase(newCust.RrStation));
                        strAddress = confirm();
                    }
                    else // there is both a street address and a rr
                    {
                        Console.Write("\nYou entered {0} {1} {2} {3} {4}.  Is this correct? Y/N\n", newCust.StreetNumber, ti.ToTitleCase(newCust.StreetName), ti.ToTitleCase(newCust.StreetType), ti.ToTitleCase(newCust.Rroute), ti.ToTitleCase(newCust.RrStation));
                        strAddress = confirm();
                        
                    }
                    
                }
            }

            string interestInput = null;
            double setInterest;
            bool isInterest = false;
            while (isInterest == false)
            { 
                Console.WriteLine("Please enter the interest on this customer's account.");
                try
                {
                    interestInput = Console.ReadLine();
                }
                finally
                {
                    double.TryParse(interestInput, out setInterest);
                    setInterest = setInterest / 100;
                    newCust.Interest = setInterest;
                }
                Console.Write("You entered {0:P}, is that correct? Y/N\n", setInterest);
                isInterest = confirm();

            }
            

            string sinInput = null;
            bool isSIN = false;
            bool unique = true;
            
            int socialInsurance;
            while (isSIN == false || unique == false)
            {
                unique = true;
                Console.Write("Please enter the customer's social insurance number.  E.g. 123456789: ");
                sinInput = Console.ReadLine();
                if (sinInput.Length != 9)
                {
                    Console.WriteLine("\nThat is not a valid SIN.  Please try again.");
                }
                else
                {

                    int.TryParse(sinInput, out socialInsurance);

                    if (customerList.Count > 0)
                    {

                        foreach (SavingsAccount cust in customerList)
                        {
                            if (cust.SIN == socialInsurance)
                            {
                                Console.WriteLine("That SIN is already assigned to another customer.  Please try again");
                                unique = false;
                                break;
                            }
                            
                                
                            
                        }
                        
                    }
                    else if (customerList.Count == 0)
                    {
                        unique = true;
                    }
                    

                    if (unique)
                    {
                        newCust.SIN = socialInsurance;
                        isSIN = true;
                    }
                }

            }
            Console.Write("Customer created successfully.  Press any key to return to the menu...");
            Console.ReadKey();
            return newCust;
        }



        static void displayMenu()
        {
            bool leaveMenu = false;
            while (!leaveMenu)
            {
            
                Console.Clear();
                Console.WriteLine("**********************************************");
                Console.WriteLine("*       Welcome to George Brown Banking      *");
                Console.WriteLine("*       Branch Manager: Robert DeCaire       *");
                Console.WriteLine("**********************************************");
                Console.WriteLine("*           Please Choose An Option          *");
                Console.WriteLine("**********************************************");
                Console.WriteLine("* (C)reate a customer * (M)odify a customer  *");
                Console.WriteLine("* (R)emove a customer * (S)how customers     *");
                Console.WriteLine("* (D)eposit funds     * (W)ithdraw funds     *");
                Console.WriteLine("* (L)oad customers    * Save and (Q)uit      *");    
                Console.WriteLine("**********************************************");
                string choice = getMenuChoice();
                switch (choice)
                {
                    case "c":
                        Console.Clear();
                        customerList.Add(newCustomer());
                        customerList.Sort(compareByLastName);
                        break;
                    case "m":
                        Console.Clear();
                    //    modifyCustomer();
                        
                        break;
                    case "r":
                        Console.Clear();
                    //    removeCustomer();
                        
                        break;
                    case "s":
                        Console.Clear();
                        showCustomers();
                        Console.WriteLine("Press any key to return to the menu...");
                        Console.ReadKey();
                        break;
                    case "d":
                        Console.Clear();
                        depositFunds();
                        Console.WriteLine("Press any key to return to the menu...");
                        Console.ReadKey();
                        break;
                    case "w":
                        Console.Clear();
                    //    withdrawFunds();
                        break;
                    case "l":
                        Console.Clear();
                        loadCustomers();
                        customerList.Sort(compareByLastName);
                        break;
                    case "q":
                        Console.Clear();
                        bool quit = saveAndQuit();
                        if (quit)
                        {
                            leaveMenu = true;
                        }
                        break;
                    default:
                        break;
                }
            }        
        }

        private static int compareByLastName(SavingsAccount name1, SavingsAccount name2) // adapted from the msdn page on List<T>.Sort
        {
            string x = name1.LastName;
            string y = name2.LastName;
            if (x == null)
            {
                if (y == null)
                {
                    // If x is null and y is null, they're 
                    // equal.  
                    return 0;
                }
                else
                {
                    // If x is null and y is not null, y 
                    // is greater.  
                    return -1;
                }
            }
            else
            {
                // If x is not null... 
                // 
                if (y == null)
                // ...and y is null, x is greater.
                {
                    return 1;
                }
                else
                {
                    return x.CompareTo(y);
                }
            }
        }

        public static string getMenuChoice()
        {
            ConsoleKeyInfo keyChoice;
            string strChoice = null;
            try
            {
               keyChoice = Console.ReadKey(true);
               strChoice = keyChoice.Key.ToString();
            }
            catch
            {
            }
            
           
            if (strChoice != null)
            {
                strChoice = strChoice.ToLower();
            }
            else
            {
                strChoice = "0";
            }
            return strChoice;
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

        static void depositFunds()
        {
            if (customerList.Count == 0)
            {
                Console.WriteLine("There are no customers in the system.\nPlease create a customer or load saved customers.\n");
            }
            else
            {
                SavingsAccount currentCust = new SavingsAccount();
                bool quitNow = false;
                while (!quitNow)
                {

                    showCustomers();
                    Console.WriteLine("Please choose the account to which you would like to make a deposit.");
                    int custIndex = inputIndex();
                    if (custIndex < 1 || custIndex > customerList.Count)
                    {
                        Console.WriteLine("That is not a valid customer.  Please try again.");
                    }
                    else
                    {
                        currentCust = customerList[custIndex - 1];
                        Console.Clear();
                        displayCustomer(currentCust);
                        bool gotDeposit = false;
                        double deposit;
                        while (!gotDeposit)
                        {
                            Console.WriteLine("How much would you like to deposit?");
                            string depositInput = Console.ReadLine();
                            gotDeposit = double.TryParse(depositInput, out deposit);
                            if (!gotDeposit)
                            {
                                Console.WriteLine("That is not a valid value.  Please try again.");
                            }
                            else
                            {
                                Console.WriteLine("You entered {0:c}.  Is this correct? Y/N", deposit);
                                bool depositCorrect = confirm();
                                if (!depositCorrect)
                                {
                                    gotDeposit = false;
                                }
                                else
                                {
                                    currentCust.makeDeposit(deposit);
                                    currentCust.updateBalance();
                                    Console.WriteLine("Deposit made successfully.");
                                    Console.WriteLine("The current balance in this account is {0:C}.", currentCust.Balance);
                                    Console.WriteLine("The balance in 1 year, compounded monthly at {0:p} will be {1:C}", currentCust.Interest, currentCust.FutureBalance);
                                }
                            }
                        }

                        quitNow = true;
                    }
                }
            }
        }

        static void displayCustomer(SavingsAccount cust)
        {
            Console.WriteLine("*****************************************************************");
            Console.WriteLine("* {0, -10} * {1, -10} * {2, -9} * {3, -20} *", "Last Name", "First Name", "SIN", "Address");
            Console.WriteLine("* {0, -10} * {1, -10} * {2, -9} * {3, -20} *", "", "", "", "");
            Console.WriteLine("* {0, -10} * {1, -10} * {2, -9} * {3, -20} *", cust.LastName, cust.FirstName, cust.SIN, cust.FullAddress);
            Console.WriteLine("*****************************************************************");
            
            Console.WriteLine("* {0, -15} * {1, -17} * {2,-21}*", "Balance", "Interest", "1 Year Balance");
            Console.WriteLine("* {0, -15} * {1, -17} * {2, 21}*", "", "", "");
            Console.WriteLine("* {0, -15} * {1, -17:p2} * {2,-21}*", cust.Balance, cust.Interest, cust.FutureBalance);
            Console.WriteLine("*****************************************************************");
        }


        static int inputIndex()
        {
            ConsoleKeyInfo keyChoice;
            int accountChoice = 0;
            try
            {
                keyChoice = Console.ReadKey(true);
                if (char.IsDigit(keyChoice.KeyChar))
                {
                    int answer = Convert.ToInt32(keyChoice.KeyChar);
                    accountChoice = answer - 48; //-48 because 0 is represented in unicode by 48 and 1 by 49 etc etc
                }
            }
            catch
            {
            }
               
            
            return accountChoice;
        
        }

        static void showCustomers()
        {
            if (customerList.Count > 0)
            {
                Console.WriteLine("****************************************************************************");
                Console.WriteLine("*                           Customer Information                           *");
                Console.WriteLine("****************************************************************************");
                Console.WriteLine("* {0,-2} * {1,-10} * {2,-10} * {3,-20} * {4, -7} * {5, -5} *", "#" , "Last Name", "First Name", "Address", "Balance", "Interest");
                Console.WriteLine("****************************************************************************");

                int index = 0;
                foreach (SavingsAccount cust in customerList)
                {
                    index++;
                    Console.WriteLine("* {0, -2} * {1,-10} * {2,-10} * {3,-20} * {4, -7:c} * {5, -8:p} *", index, cust.LastName, cust.FirstName, cust.FullAddress, cust.Balance, cust.Interest);
                    Console.WriteLine("****************************************************************************");
                }
                // need to add: way to look at a particular customer in more detail, view SIN, future balance, etc.
            }
            else
            {
                Console.WriteLine("There are no customers in the system.\nPlease create a customer or load saved customers.\n");
            }
            
        }

        static void loadCustomers()
        {
            
            Console.WriteLine("Are you sure you want to load a saved customer list? This will delete the current list. Y/N");
            bool loadOrNot = confirm();
            if (loadOrNot)
            {
                Console.WriteLine("Loading...\n");
                try
                {
                    SaveAndLoad load = new SaveAndLoad("customer_list.sav");
                    customerList = load.Load();
                    Console.WriteLine("Load successful!\n");
                    
                }
                catch
                {
                    Console.WriteLine("There does not seem to be a valid saved customer list.");
                }
            }
            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey();
            
        }

        static bool saveAndQuit()
        {
            Console.WriteLine("Are you sure you want to save your customer list? Y/N");
            bool saveOrNot = confirm();
            if (saveOrNot)
            {
                Console.WriteLine("Saving...\n");
                try
                {
                    SaveAndLoad save = new SaveAndLoad("customer_list.sav");
                    save.Save(customerList);
                    Console.WriteLine("Save successful!\n");
                }
                catch
                {
                    Console.WriteLine("Something went wrong.  Please try again.");
                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                    return false;
                }
            }
            Console.WriteLine("Are you sure you want to quit? Y/N");
            bool quitOrNot = confirm();
            if (quitOrNot)
            {
                return true;
            }
            else
            {
                return false;
            }
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
