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
            streetType;
        int socialInsurance,
            streetNumber;
        double interest,
            balance,
            deposit,
            oldBalance;

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
        public int StreetNumber
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

        public void updateBalance()
        {
            oldBalance = balance;
            balance += deposit;
            deposit = 0;
            
        }
    }
}
