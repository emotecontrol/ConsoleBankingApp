using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Assignment1
{
    class RobertDeCaire_SavingsAccount
    {
        string firstName,
            lastName,
            streetName,
            streetType,
            rroute,
            rrStation,
            phone,
            streetNumber;
        int socialInsurance;
        const double interest = 0.055;
        double balance,
            deposit,
            oldBalance,
            withdrawal,
            futureBalance;

        public RobertDeCaire_SavingsAccount()
        {
            
             
        }

        

        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
            }
        }
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
            }
        }
        public string StreetName
        {
            get
            {
                return streetName;
            }
            set
            {
                streetName = value;
            }
        }
        public string StreetType
        {
            get
            {
                return streetType;
            }
            set
            {
                streetType = value;
            }
        }
        public string Phone
        {
            get
            {
                return phone;
            }
            set
            {
                phone = value;
            }
        }
        public int SIN
        {
            get
            {
                return socialInsurance;
            }
            set
            {
                socialInsurance = value;
            }
        }
        public string StreetNumber
        {
            get
            {
                return streetNumber;
            }
            set
            {
                streetNumber = value;
            }
        }

        public string Rroute
        {
            get
            {
                return rroute;
            }
            set
            {
                rroute = value;
            }
        }

        public string RrStation
        {
            get
            {
                return rrStation;
            }
            set
            {
                rrStation = value;
            }
        }

        public double Interest
        {
            get
            {
                return interest;
            }
            
        }
        public double Balance
        {
            get
            {
                return balance;
            }
            set
            {
                balance = value;
            }
        }

        public string FullAddress
        {
            get
            {
                TextInfo ti = new CultureInfo("en-CA", false).TextInfo;
                return streetNumber + " " + ti.ToTitleCase(streetName) + " " + ti.ToTitleCase(streetType);
            }
        }

        public double FutureBalance
        {
            get
            {
                return futureBalance;
            }
            set
            {
                futureBalance = value * Math.Pow((1 + interest / 12), 12);
                
            }
        }


        public void makeDeposit(double newDeposit)
        {
            deposit += newDeposit;
            // remember that the balance does not get automatically updated!
        }
        
        public void makeWithdrawal(double newWithdrawal)
        {
            withdrawal -= newWithdrawal;
            // remember that the balance does not get automatically updated!
        }
        
        
        public void updateBalance()
        // updates the balance to reflect the deposits and withdrawls being made.
        {
            oldBalance = balance;
            balance += (deposit + withdrawal);
            this.FutureBalance = balance;
            deposit = 0;
            withdrawal = 0;
            
        }
    }
}
