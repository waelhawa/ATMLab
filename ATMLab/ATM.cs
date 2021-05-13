using System;
using System.Collections.Generic;
using System.Text;

namespace ATMLab
{
    class ATM
    {
        public Account User { get; set; }
        public List<Account> accounts = new List<Account>();


        public void Register (string userName, string passWord)
        {
            Account User = new Account();
            User.Name = userName;
            User.Password = passWord;
            accounts.Add(User);
        }

        public void Login (string userName, string passWord)
        {
            for (int i = 0; i < accounts.Count; i++)
            {
                if (accounts[i].Name == userName && accounts[i].Password == passWord)
                {
                    User = accounts[i];
                    break;
                }
            }
            if (User == null)
            {
                Console.WriteLine("Account not found!");
            }
        }

        public void Logout ()
        {
            User = null;
        }

        public double CheckBalance ()
        {
            if (User != null)
            {
                return Math.Round(User.Balance, 2);
            }
            return 0;
        }

        public void Deposit (double amount)
        {
            if (User != null)
            {
                User.Balance += Math.Round(amount, 2);
            }
        }

        public void Withdraw (double amount)
        {
            if (User != null)
            {
                User.Balance -= Math.Round(amount, 2);
            }
        }


    }
}
