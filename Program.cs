using AccountApp.Model;
using System;
using System.IO; // Add this namespace for file operations
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace AccountApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Account account = LoadAccount(); // Try to load an existing account

            if (account != null)
            {
                // Account loaded successfully, display the welcome message and balance
                Console.WriteLine("Welcome Back!");
                Console.WriteLine("Balance(Rs): " + account.balance);
            }
            else
            {
                // Account doesn't exist or couldn't be loaded, create a new account
                Console.WriteLine("Welcome! Enter Account");

                int accountNumber;
                string accountName, bankName;
                decimal openingBalance;

                Console.Write("Enter Account Number: ");
                accountNumber = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter Account Holder Name: ");
                accountName = Console.ReadLine();

                Console.Write("Enter Bank Name: ");
                bankName = Console.ReadLine();

                Console.Write("Enter Opening Balance (Rs): ");
                if (decimal.TryParse(Console.ReadLine(), out openingBalance))
                {
                    if (openingBalance >= 500.00m)
                    {
                        account = new Account(accountNumber, accountName, bankName, openingBalance);
                        Console.WriteLine("Account created successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Opening balance must be at least Rs. 500.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid opening balance.");
                }
            }

            // Now, you can continue with the menu options and serialization as in the previous code
            while (true)
            {
                Console.WriteLine("\nWhat do you wish to do?");
                Console.WriteLine("1. Display Balance");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your choice: ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            account.DisplayBalance();
                            break;
                        case 2:
                            Deposit(account);
                            break;
                        case 3:
                            Withdraw(account);
                            break;
                        case 4:
                            SerializeAccount(account);
                            Console.WriteLine("Thank you for using this application!");
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please enter a valid option.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid option.");
                }
            }
        }

        private static void Deposit(Account account)
        {
            Console.Write("Enter amount to deposit: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal depositAmount))
            {
                account.Deposit(depositAmount);
            }
            else
            {
                Console.WriteLine("Invalid amount.");
            }
        }

        private static void Withdraw(Account account)
        {
            Console.Write("Enter withdrawal amount (Rs.): ");
            if (decimal.TryParse(Console.ReadLine(), out decimal withdrawAmount))
            {
                account.Withdraw(withdrawAmount);
            }
            else
            {
                Console.WriteLine("Invalid amount.");
            }
        }

        private static void SerializeAccount(Account account)
        {
            string filePath = @"C:\Users\Adithya AV\OneDrive\Desktop\FilePOC\account.txt";

            // Serialization
            DataSerializer.BinarySerializer(account, filePath);
            Console.WriteLine("Account object serialized");
        }

        private static Account LoadAccount()
        {
            string filePath = @"C:\Users\Adithya AV\OneDrive\Desktop\FilePOC\account.txt";

            if (File.Exists(filePath))
            {
                // Account file exists, try to load it
                Account loadedAccount = DataSerializer.BinaryDeserializer(filePath);
                return loadedAccount;
            }

            return null; // Account file doesn't exist
        }


        //private static Account LoadAccount()
        //{
        //    string filePath = @"C:\Users\Adithya AV\OneDrive\Desktop\FilePOC\account.txt";

        //    try
        //    {
        //        if (File.Exists(filePath))
        //        {
        //            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
        //            {
        //                if (fileStream.Length > 0)
        //                {
        //                    // Create a BinaryFormatter instance
        //                    BinaryFormatter formatter = new BinaryFormatter();

        //                    // Deserialize the account from the stream
        //                    Account loadedAccount = (Account)formatter.Deserialize(fileStream);

        //                    return loadedAccount;
        //                }
        //                else
        //                {
        //                    Console.WriteLine("The account file is empty.");
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("An error occurred while loading the account data: " + ex.Message);
        //    }

        //    return null; // Account file doesn't exist or couldn't be loaded
        //}


    }
}
