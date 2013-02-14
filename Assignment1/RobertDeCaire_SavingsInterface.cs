﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Assignment1
{
    // version 1.01 09/02/2013

    class RobertDeCaire_SavingsInterface
    {        
        static List<RobertDeCaire_SavingsAccount> customerList = new List<RobertDeCaire_SavingsAccount>();
        static TextInfo ti = new CultureInfo("en-CA", false).TextInfo;
        
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            displayMenu();
        }

        static void addFunds(RobertDeCaire_SavingsAccount currentCust)
        {
            bool quitNow = false;
            while (!quitNow)
            {
                string depositInput = Console.ReadLine();
                double deposit;
                bool gotDeposit = double.TryParse(depositInput, out deposit);

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

                        quitNow = true;
                    }
                }
            }
        }

        private static int compareByLastName(RobertDeCaire_SavingsAccount name1, RobertDeCaire_SavingsAccount name2) // adapted from the msdn page on List<T>.Sort
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

        static void depositFunds()
        {
            if (customerList.Count == 0)
            {
                Console.WriteLine("There are no customers in the system.\nPlease create a customer or load saved customers.\n");
            }
            else
            {
                RobertDeCaire_SavingsAccount currentCust = new RobertDeCaire_SavingsAccount();
                showCustomers(true);
                bool quitNow = false;
                while (!quitNow)
                {


                    Console.WriteLine("Please choose the account to which you would like to make a deposit.");
                    int custIndex = inputIndex();
                    if (custIndex < 1 || custIndex > customerList.Count)
                    {
                        Console.Clear();
                        showCustomers(true);
                        Console.WriteLine("That is not a valid customer.  Please try again.\n");

                    }
                    else
                    {
                        currentCust = customerList[custIndex - 1];
                        Console.Clear();
                        displayCustomer(currentCust);

                        Console.WriteLine("How much would you like to deposit?");
                        addFunds(currentCust);
                        Console.WriteLine("Deposit made successfully.");
                        Console.WriteLine("The current balance in this account is {0:C}.", currentCust.Balance);
                        Console.WriteLine("The balance in 1 year, compounded monthly at {0:p} will be {1:C}", currentCust.Interest, currentCust.FutureBalance);
                        quitNow = true;
                    }
                }
            }
        }
        
        static void displayCustomer(RobertDeCaire_SavingsAccount cust)
        {
            Console.WriteLine("*****************************************************************");
            Console.WriteLine("* {0, -10} * {1, -10} * {2, -9} * {3, -23} *", "Last Name", "First Name", "SIN", "Address");
            Console.WriteLine("* {0, -10} * {1, -10} * {2, -9} * {3, -23} *", "", "", "", "");
            Console.WriteLine("* {0, -10} * {1, -10} * {2, -9} * {3, -23} *", cust.LastName, cust.FirstName, cust.SIN, cust.FullAddress);
            Console.WriteLine("*****************************************************************");

            
            Console.WriteLine("* {0, -10} * {1, -12} *                                   *", "City", "Postal Code");
            Console.WriteLine("* {0, -10} * {1, -12} *                                   *", cust.City, cust.Postal);
            Console.WriteLine("*****************************************************************");

            Console.WriteLine("* {0,-14} * {1, -10} * {2, -8} * {3,-21}*", "Phone #", "Balance", "Interest", "Closing Balance");
            Console.WriteLine("* {0,-14} * {1, -10} * {2, -8} * {3, 21}*", "", "", "", "");
            Console.WriteLine("* {0,-14} * {1, -10:c} * {2, -8:p2} * {3,-21:c}*", cust.Phone, cust.Balance, cust.Interest, cust.FutureBalance);
            Console.WriteLine("*****************************************************************");
        }

        static void displayMenu()
        {
            bool leaveMenu = false;
            while (!leaveMenu)
            {
                Console.Clear();
                Console.WriteLine("*********************************************");
                Console.WriteLine("*      Welcome to George Brown Banking      *");
                Console.WriteLine("*      Branch Manager: Robert DeCaire       *");
                Console.WriteLine("*********************************************");
                Console.WriteLine("*          Please Choose An Option          *");
                Console.WriteLine("*********************************************");
                Console.WriteLine("* (C)reate a customer * (M)odify a customer *");
                Console.WriteLine("* (R)emove a customer * (S)how customers    *");
                Console.WriteLine("* (D)eposit funds     * (W)ithdraw funds    *");
                Console.WriteLine("* (L)oad customers    * Save and (Q)uit     *");
                Console.WriteLine("*********************************************");
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
                        modifyCustomer();
                        customerList.Sort(compareByLastName);
                        break;
                    case "r":
                        Console.Clear();
                        removeCustomer();
                        break;
                    case "s":
                        Console.Clear();
                        showCustomers(false);
                        break;
                    case "d":
                        Console.Clear();
                        depositFunds();
                        Console.WriteLine("Press any key to return to the menu...");
                        Console.ReadKey();
                        break;
                    case "w":
                        Console.Clear();
                        withdrawFunds();
                        Console.WriteLine("Press any key to return to the menu...");
                        Console.ReadKey();
                        break;
                    case "l":
                        Console.Clear();
                        loadCustomers();
                        customerList.Sort(compareByLastName);
                        break;
                    case "q":
                        Console.Clear();
                        if (saveAndQuit())
                        {
                            leaveMenu = true;
                        }
                        break;
                    default:
                        break;
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

        static int inputIndex() // some of this code was found at http://stackoverflow.com/questions/648555/is-there-a-good-way-to-use-console-readkey-for-choosing-between-values-without-d
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

        static void loadCustomers()
        {

            Console.WriteLine("Are you sure you want to load a saved customer list?\nThis will delete the current list. Y/N");
            if (confirm())
            {
                Console.WriteLine("Loading...\n");
                try
                {
                    RobertDeCaire_SaveAndLoad load = new RobertDeCaire_SaveAndLoad("customer_list.sav");
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

        static void modifyCustomer()
        {
            if (customerList.Count == 0)
            {
                Console.WriteLine("There are no customers in the system.\nPlease create a customer or load saved customers.\n");
            }
            else
            {

                int custNumber = 0;
                bool quit = true;
                showCustomers(true);
                Console.WriteLine("\nPlease choose the customer you would like to modify.");
                Console.WriteLine("Otherwise hit 'Return' to return to the menu.");
                bool gotNumber = int.TryParse(Console.ReadLine(), out custNumber);
                if (gotNumber && custNumber > 0 && custNumber <= customerList.Count)
                {
                    quit = false;
                }
                while (!quit)
                {
                    Console.Clear();
                    RobertDeCaire_SavingsAccount workingCustomer = customerList[custNumber - 1];
                    displayCustomer(workingCustomer);
                    Console.WriteLine("");
                    Console.WriteLine("***********************************************");
                    Console.WriteLine("*           Please Select an Option           *");
                    Console.WriteLine("***********************************************");
                    Console.WriteLine("* Change (F)irst Name   * Change (L)ast Name  *");
                    Console.WriteLine("* Change (A)ddress      * Change (P)hone      *");
                    Console.WriteLine("* Change (S)IN          * (R)eturn to Menu    *");
                    Console.WriteLine("***********************************************");
                    string choice = getMenuChoice();
                    switch (choice)
                    {
                        case "f":
                            setFirstName(workingCustomer);
                            Console.WriteLine("First name changed successfully. Hit any key to continue.");
                            Console.ReadKey();
                            break;
                        case "l":
                            setLastName(workingCustomer);
                            Console.WriteLine("Last name changed successfully. Hit any key to continue.");
                            Console.ReadKey();
                            break;
                        case "a":
                            setAddress(workingCustomer);
                            Console.WriteLine("Address changed successfully. Hit any key to continue.");
                            Console.ReadKey();
                            break;

                        case "s":
                            setSIN(workingCustomer);
                            Console.WriteLine("SIN changed successfully. Hit any key to continue.");
                            Console.ReadKey();
                            break;
                        case "p":
                            setPhone(workingCustomer);
                            Console.WriteLine("Phone number changed successfully. Hit any key to continue.");
                            Console.ReadKey();
                            break;
                        case "r":
                            quit = true;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        static RobertDeCaire_SavingsAccount newCustomer()
        {
            RobertDeCaire_SavingsAccount newCust = new RobertDeCaire_SavingsAccount();
            Console.WriteLine("Welcome to Customer Creation.\nTo create a new customer entry, please follow the instructions.\n");
            setFirstName(newCust);
            setLastName(newCust);
            setAddress(newCust);
            setPhone(newCust);

            setOpeningBalance(newCust);
            setSIN(newCust);
            Console.Write("Customer created successfully.  Press any key to return to the menu...");
            Console.ReadKey();
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
        
        static void removeCustomer()
        {
            int custNumber = 0;
            showCustomers(true);
            Console.WriteLine("\nPlease choose a customer to delete.");
            Console.WriteLine("Warning!  This cannot be undone!");
            Console.WriteLine("Hit 'return' to exit.");
            bool gotNumber = int.TryParse(Console.ReadLine(), out custNumber);
            if (gotNumber && custNumber > 0 && custNumber <= customerList.Count)
            {
                Console.WriteLine("Would you really like to delete {0} {1}? Y/N", ti.ToTitleCase(customerList[custNumber - 1].FirstName), ti.ToTitleCase(customerList[custNumber - 1].LastName));
                if (confirm())
                {
                    customerList.Remove(customerList[custNumber - 1]);
                }
            }
        }

        static bool saveAndQuit()
        {
            Console.WriteLine("Are you sure you want to save your customer list? Y/N");
            if (confirm())
            {
                Console.WriteLine("Saving...\n");
                try
                {
                    RobertDeCaire_SaveAndLoad save = new RobertDeCaire_SaveAndLoad("customer_list.sav");
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
            return confirm();
        }

        static void setAddress(RobertDeCaire_SavingsAccount customer)
        {
            bool strAddress = false;
            bool cityAddress = false;
            bool postCode = false;

            string addressInput = null;
            string[] splitAddress = new string[3];
            string[] splitRR = new string[2];

            while (strAddress == false)
            {
                bool successInput = false;
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
                                customer.StreetNumber = splitAddress[0];
                                customer.StreetName = splitAddress[1];
                                RobertDeCaire_StreetAddressConverter convert = new RobertDeCaire_StreetAddressConverter();

                                customer.StreetType = convert.convertToAbbr(splitAddress[2]);
                                customer.Rroute = splitRR[0];
                                customer.RrStation = splitRR[1];
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

                                customer.Rroute = splitRR[0];
                                customer.RrStation = splitRR[1];
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
                            customer.StreetNumber = splitAddress[0];
                            customer.StreetName = splitAddress[1];

                            RobertDeCaire_StreetAddressConverter convert = new RobertDeCaire_StreetAddressConverter();

                            string convertme = splitAddress[2];
                            string streetAbbr = convert.convertToAbbr(convertme);
                            customer.StreetType = streetAbbr;
                            successInput = true;
                        }
                        else
                        {
                            Console.WriteLine("There was a problem with your input.  Please try again.");
                        }
                    }


                    if (successInput == false)
                    {
                        //Console.WriteLine("No address was entered.  Please try again.");
                    }
                    else if (customer.Rroute == null) // there is no rr
                    {
                        Console.Write("\nYou entered {0} {1} {2}.  Is this correct? Y/N\n", customer.StreetNumber, ti.ToTitleCase(customer.StreetName), ti.ToTitleCase(customer.StreetType));
                        strAddress = confirm();
                    }
                    else if (customer.StreetName == null) // there is no street address
                    {
                        Console.Write("\nYou entered {0} {1}.  Is this correct? Y/N\n", ti.ToTitleCase(customer.Rroute), ti.ToTitleCase(customer.RrStation));
                        strAddress = confirm();
                    }
                    else // there is both a street address and a rr
                    {
                        Console.Write("\nYou entered {0} {1} {2} {3} {4}.  Is this correct? Y/N\n", customer.StreetNumber, ti.ToTitleCase(customer.StreetName), ti.ToTitleCase(customer.StreetType), ti.ToTitleCase(customer.Rroute), ti.ToTitleCase(customer.RrStation));
                        strAddress = confirm();

                    }

                }
            }

            string inputCity = null;
            string inputPostCode = null;

            while (cityAddress == false)
            {
                try
                {
                    Console.WriteLine("Please enter the customer's city.");
                    inputCity = Console.ReadLine().Trim();
                    customer.City = inputCity;
                }
                finally
                {
                    
                    Console.WriteLine("\nYou entered {0}.  Is this correct? Y/N", customer.City);
                    cityAddress = confirm();
                }
            }

            while (postCode == false)
            {
                try
                {
                    Console.WriteLine("Please enter the customer's postal code.  Example: M1N 2P3");
                    inputPostCode = Console.ReadLine();
                }
                finally
                {
                    if (Regex.IsMatch(inputPostCode, @"\b([ABCEGHJKLMNPRSTVXYabceghjklmnprstvxy][0-9][A-Za-z]) ?([0-9][A-Za-z][0-9])\b"))
                    {
                        string[] post = new string[2];
                        Match match = Regex.Match(inputPostCode, @"\b([ABCEGHJKLMNPRSTVXYabceghjklmnprstvxy][0-9][A-Za-z]) ?([0-9][A-Za-z][0-9])\b");
                        if (match.Success)
                        {
                        
                            customer.Postal= match.Groups[1].Value.ToUpper() + " " + match.Groups[2].Value.ToUpper();
                            Console.WriteLine("You entered {0}.  Is that correct? Y/N", customer.Postal);
                            postCode=confirm();
                        }
                        else
                        {
                            Console.WriteLine("There is something wrong with your input.  Please try again.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("That is not a valid Canadian postal code.  Please try again.");
                    }
                }
            }

        }

        static void setFirstName(RobertDeCaire_SavingsAccount customer)
        {
            bool first = false;
            string firstName = null;
            while (first == false)
            {
                Console.Write("Please enter the customer's first name: ");
                try
                {
                    firstName = Console.ReadLine();
                    customer.FirstName = firstName.Trim();
                    Console.Write("You entered {0}.  Is this correct? Y/N\n", customer.FirstName);
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
        }

        static void setLastName(RobertDeCaire_SavingsAccount customer)
        {

            bool last = false;
            string lastName = null;
            while (last == false)
            {
                Console.Write("Please enter the customer's last name: ");
                try
                {
                    lastName = Console.ReadLine();
                    customer.LastName = lastName.Trim();
                    Console.Write("You entered {0}.  Is this correct? Y/N\n", customer.LastName);
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
        }

        static void setOpeningBalance(RobertDeCaire_SavingsAccount customer)
        {
            Console.WriteLine("Please enter the customer's opening balance.");
            addFunds(customer);
        }

        static void setPhone(RobertDeCaire_SavingsAccount customer)
        {
            string phoneInput = null;
            string[] phoneArray = new string[3];
            bool isPhone = false;
            while (!isPhone)
            {
                Console.WriteLine("Please enter the customer's phone #");
                try
                {
                    phoneInput = Console.ReadLine();
                    Match match = Regex.Match(phoneInput, @"\(?\b([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})\b");
                    if (match.Success)
                    {
                        phoneArray[0] = match.Groups[1].Value;
                        phoneArray[1] = match.Groups[2].Value;
                        phoneArray[2] = match.Groups[3].Value;
                        customer.Phone = ("(" + phoneArray[0] + ") " + phoneArray[1] + " " + phoneArray[2]);
                        Console.WriteLine("You entered ({0}) {1}-{2}.  Is that correct? Y/N", phoneArray[0], phoneArray[1], phoneArray[2]);
                        isPhone = confirm();
                    }
                    else
                    {
                        Console.WriteLine("That is not a valid phone number. Please try again.");
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid input.  Please try again.");

                }      
            }
        }
        
        static void setSIN(RobertDeCaire_SavingsAccount customer)
        {
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

                        foreach (RobertDeCaire_SavingsAccount cust in customerList)
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
                        customer.SIN = socialInsurance;
                        isSIN = true;
                    }
                }

            }
        }

        static void showCustomers(bool justDisplay)
        {

            if (customerList.Count > 0)
            {
                Console.WriteLine("*******************************************************************************");
                Console.WriteLine("*                             Customer Information                            *");
                Console.WriteLine("*******************************************************************************");
                Console.WriteLine("* {0,-2} * {1,-10} * {2,-10} * {3,-26} * {4, -15} *", "#", "Last Name", "First Name", "Address", "Balance");
                Console.WriteLine("*******************************************************************************");

                int custNumber = 0;
                int index = 0;
                foreach (RobertDeCaire_SavingsAccount cust in customerList)
                {
                    index++;
                    Console.WriteLine("* {0, -2} * {1,-10} * {2,-10} * {3,-26} * {4, -15:c} *", index, cust.LastName, cust.FirstName, cust.FullAddress, cust.Balance);
                    Console.WriteLine("*******************************************************************************");
                }
                if (!justDisplay)
                {
                    bool quit = false;
                    while (!quit)
                    {
                        Console.WriteLine("\nTo view more details of a customer, enter that customer's number.");
                        Console.WriteLine("Otherwise hit 'Return' to return to the menu.");
                        bool gotNumber = int.TryParse(Console.ReadLine(), out custNumber);
                        if (gotNumber && custNumber > 0 && custNumber <= customerList.Count)
                        {
                            Console.Clear();
                            displayCustomer(customerList[custNumber - 1]);
                            Console.WriteLine("Press any key to return to the menu...");
                            Console.ReadKey();
                            quit = true;
                        }
                        else if (custNumber == 0)
                        {
                            quit = true;
                        }
                        else
                        {
                            Console.Clear();
                            showCustomers(true);
                            Console.WriteLine("That is not a valid customer.  Please try again.\n");
                            custNumber = 0;

                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("There are no customers in the system.\nPlease create a customer or load saved customers.\n");
                Console.WriteLine("Press any key to return to the menu...");
                Console.ReadKey();

            }

        }

        static void withdrawFunds()
        {
            if (customerList.Count == 0)
            {
                Console.WriteLine("There are no customers in the system.\nPlease create a customer or load saved customers.\n");
            }
            else
            {
                RobertDeCaire_SavingsAccount currentCust = new RobertDeCaire_SavingsAccount();
                showCustomers(true);
                bool quitNow = false;
                while (!quitNow)
                {
                    
                    
                    Console.WriteLine("Please choose the account from which you would like to make a withdrawal.");
                    int custIndex = inputIndex();
                    if (custIndex < 1 || custIndex > customerList.Count)
                    {
                        Console.Clear();
                        showCustomers(true);
                        Console.WriteLine("That is not a valid customer.  Please try again.\n");

                    }
                    else if (customerList[custIndex - 1].Balance == 0)
                    {
                        Console.WriteLine("There are no funds in that account to withdraw.");
                        quitNow = true;
                    }
                    else
                    {
                        currentCust = customerList[custIndex - 1];
                        Console.Clear();
                        displayCustomer(currentCust);
                        bool gotWithdrawal = false;
                        double withdrawal;
                        while (!gotWithdrawal)
                        {
                            Console.WriteLine("How much would you like to withdraw?");
                            string withdrawalInput = Console.ReadLine();
                            gotWithdrawal = double.TryParse(withdrawalInput, out withdrawal);
                            if (!gotWithdrawal)
                            {
                                Console.WriteLine("That is not a valid value.  Please try again.");
                            }
                            
                            else
                            {
                                Console.WriteLine("You entered {0:c}.  Is this correct? Y/N", withdrawal);
                                bool withdrawalCorrect = confirm();
                                if (!withdrawalCorrect)
                                {
                                    gotWithdrawal = false;
                                }
                                else if (withdrawal > currentCust.Balance)
                                {
                                    Console.WriteLine("The withdrawal amount is greater than the account balance.  Please try again.");
                                    gotWithdrawal = false;
                                }
                                else
                                {
                                    currentCust.makeWithdrawal(withdrawal);
                                    currentCust.updateBalance();
                                    Console.WriteLine("Withdrawal made successfully.");
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
    }
}