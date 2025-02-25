using System;
using System.Collections.Generic;

namespace Module07
{
    public class BankAccount
    {
        private decimal balance;
        private string accountNumber;
        private List<Transaction> transactions;

        public BankAccount(string accountNumber, decimal initialBalance)
        {
            this.accountNumber = accountNumber;
            balance = initialBalance;
            transactions = new List<Transaction>();
        }

        public void ProcessTransaction(Transaction transaction)
        {
            if (transaction == null)
            {
                Console.WriteLine("Error: Transaction cannot be null.");
                return;
            }

            if (transaction.Type == TransactionType.Withdrawal && transaction.Amount > balance)
            {
                Console.WriteLine("Error: Insufficient funds for withdrawal.");
                return;
            }

            transactions.Add(transaction);

            if (transaction.Type == TransactionType.Deposit)
            {
                balance += transaction.Amount;
            }
            else if (transaction.Type == TransactionType.Withdrawal)
            {
                balance -= transaction.Amount;
            }

            // Logging for debugging
            Console.WriteLine($"Transaction Processed: {transaction.Type} ${transaction.Amount}");
            Console.WriteLine($"New Balance: ${balance}");
        }

        public decimal GetBalance()
        {
            return balance; // Return balance directly
        }

        public void PrintBalance()
        {
            Console.WriteLine($"Account {accountNumber} Balance: ${balance}");
        }
    }

    public class Transaction
    {
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public DateTime Date { get; set; }
    }

    public enum TransactionType
    {
        Deposit,
        Withdrawal
    }

    class Program
    {
        static void Main()
        {
            var account = new BankAccount("1234", 1000);

            account.ProcessTransaction(new Transaction { Amount = 500, Type = TransactionType.Deposit });
            account.ProcessTransaction(new Transaction { Amount = 200, Type = TransactionType.Withdrawal });
            account.ProcessTransaction(null); // Should be handled safely
            account.ProcessTransaction(new Transaction { Amount = 2000, Type = TransactionType.Withdrawal }); // Should check funds

            Console.WriteLine($"Final Balance: ${account.GetBalance()}");
        }
    }
}
