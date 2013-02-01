using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment1
{
    class SavingsAccount
    {
        string firstName,
            lastName,
            streetName,
            streetType,
            rroute,
            rrStation,
            streetNumber;
        int socialInsurance;
        double interest,
            balance,
            deposit,
            oldBalance,
            withdrawal;

        public SavingsAccount()
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
            set
            {
                interest = value;
            }
        }
        public double Balance
        {
            get
            {
                return balance;
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
            deposit = 0;
            withdrawal = 0;
            
        }
    }
}
