using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountApp.Model
{
    [Serializable]
    internal class Account
    {
        public int accountNumber;
        public string accountName;
        public string bankName;
        public decimal balance;
        public decimal minimumBalance = 500.00m;

        public Account(int accountNumber, string accountName, string bankName, decimal openingBalance)
        {
            this.accountNumber = accountNumber;
            this.accountName = accountName;
            this.bankName = bankName;
            this.balance = openingBalance;
        }
        public void DisplayBalance()
        {
            
            Console.WriteLine($"Current Balance: Rs. {balance}");
        }

        public void Deposit(decimal amount)
        {
            if (amount > 0)
            {
                balance += amount;
                Console.WriteLine($"Amount of Rs. {amount} deposited successfully.");
            }
            else
            {
                Console.WriteLine("Invalid deposit amount.");
            }
        }
        public void Withdraw(decimal amount)
        {
            if (amount > 0)
            {
                decimal potentialBalance = balance - amount;
                if (potentialBalance >= minimumBalance)
                {
                    balance = potentialBalance;
                    Console.WriteLine($"Withdrawn Rs. {amount}.");
                }
                else
                {
                    Console.WriteLine($"Withdrawal failed. Minimum balance of Rs. {minimumBalance:F2} must be maintained.");
                }
            }
            else
            {
                Console.WriteLine("Invalid withdrawal amount.");
            }
        }
    }
}
