using System;
using System.Text.RegularExpressions;

namespace ATMLab
{
    class Program
    {
        //const string usernameExpression = "^(?=.{8,20}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$";
        //const string passwordExpression = "^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&])[A-Za-z\\d@$!%*#?&]{8,}$";
        const string usernameExpression = ".";
        const string passwordExpression = ".";
        const string usernameMessage = "Username: ";
        const string passwordMessage = "Password: ";
        const string continueMessage = "Would you like to continue? (Y/N): ";
        const string selectMenuMessage = "Selection: ";
        const string blankError = "Entry can't be empty";
        const string wrongFormatError = "Entry has wrong format";
        const string numberOnlyError = "Please use numbers only";
        const string ynError = "Please choose y or n";
        const string error = "Something went wrong.";
        const string mainMenu = "\n1- Login\n2- Register\n3- Exit\n";
        const string loginMenu = "\n1- Deposit\n2- Withdraw\n3- Check Balance\n4- Lougout\n";
        const string notInMenuError = "Selection not found";
        const string amountEntry = "Enter amount: ";
        static void Main(string[] args)
        {
            Console.Clear();
            ATM bank = new ATM();
            int item;
            bool looper = true;
            while (looper)
            {
                item = MenuSelection(mainMenu, 1, 3);
                if (item == 3)
                {
                    looper = false;
                }
                else
                {
                    Console.Clear();
                    MainMenu(item, bank);
                }
                //if (looper)
                //{
                //    looper = Verification(continueMessage, ynError);
                //}
            }
            Console.WriteLine("Goodbye!");
        }

        public static string UserEntry(string message, string expression)
        {
            string text;
            bool checker;
            while (true)
            {
                checker = true;
                Console.Write(message);
                text = Console.ReadLine().Trim().ToLower();
                checker = Validation(text, expression);
                if (checker)
                {
                    return text;
                }
                else
                {
                    Console.WriteLine("Try again.");
                }
            }
        } //end UserEntry

        public static bool Validation(string text, string expression)
        {
            if (!String.IsNullOrEmpty(text) || !String.IsNullOrWhiteSpace(text))
            {
                if (Regex.IsMatch(text, expression))
                {
                    return true;
                }
                else
                {
                    Console.WriteLine(wrongFormatError);
                    return false;
                }

            }
            else
            {
                Console.WriteLine(blankError);
                return false;
            }

        } //end Validation

        public static bool Verification(string phrase, string error)
        {
            string text;
            while (true)
            {
                Console.Write(phrase);
                text = Console.ReadLine().Trim().ToLower();

                switch (text)
                {
                    case "y":
                    case "yes":
                        return true;


                    case "n":
                    case "no":
                        return false;


                    default:
                        Console.WriteLine(error);
                        break;
                }
            }
        } //end Verification

        public static int IntegerEntry(string phrase, string error)
        {
            string text;
            int integerNumber;
            while (true)
            {
                Console.Write(phrase);
                text = Console.ReadLine().Trim().ToLower();
                if (int.TryParse(text, out integerNumber))
                {
                    return integerNumber;
                }
                else
                {
                    Console.WriteLine(error);
                }
            }
        } //end IntegerEntry

        public static int MenuSelection(string menu, int lower, int upper)
        {
            int item;
            while (true)
            {
                Console.WriteLine(menu);
                item = IntegerEntry(selectMenuMessage, numberOnlyError);
                if (item >= lower && item <= upper)
                {
                    return item;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine(notInMenuError);
                }
            }
        } //End MenuSelection

        public static double AmountEntry (string phrase)
        {
            double amount;
            while (true)
            {
                Console.Write(phrase);
                if (double.TryParse(Console.ReadLine(), out amount))
                {
                    return amount;
                }
                else
                {
                    Console.WriteLine(numberOnlyError);
                }
            }
        } //end AmountEntry

        public static void MainMenu(int item, ATM bank)
        {
            int item2;
            bool looper = true;
            switch (item)
            {
                case 1:
                    if (bank.accounts.Count > 0)
                    {
                        bank.User = UserLogin(bank);
                        while (looper)
                        {
                            if (bank.User == null)
                            {
                                Console.WriteLine("Account not found!");
                                looper = false;
                            }
                            else
                            {
                                item2 = MenuSelection(loginMenu, 1, 4);
                                if (item2 == 4)
                                {
                                    looper = false;
                                    bank.User = null;
                                    Console.Clear();
                                    Console.WriteLine("Logged out successfully.");
                                }
                                else
                                {
                                    LoginMenu(item2, bank);
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("No users found, please register!");
                    }
                    break;
                case 2:
                    UserRegister(bank);
                    break;
                case 3:
                    Console.Clear();
                    break;
                default:
                    Environment.Exit(0);
                    break;
            }

        } //end MainMenu

        public static void LoginMenu(int item, ATM bank)
        {

            switch (item)
            {
                case 1:
                    UserDeposit(bank);
                    break;
                case 2:
                    UserWithdraw(bank);
                    break;
                case 3:
                    UserBalance(bank);
                    break;
                case 4:
                    Console.Clear();
                    break;
                default:
                    Environment.Exit(0);
                    break;
            }

        } //end LoginMenu

        public static Account UserLogin(ATM bank)
        {
            string userName = UserEntry(usernameMessage, ".");
            string password = UserEntry(passwordMessage, ".");
            for (int i = 0; i < bank.accounts.Count; i++)
            {
                if (bank.accounts[i].Name == userName)
                {
                    if (bank.accounts[i].Password == password)
                    {
                        Console.Clear();
                        Console.WriteLine($"User {bank.accounts[i].Name} logged in successfully.");
                        return bank.accounts[i];
                    }
                    else
                    {
                        Console.WriteLine("Wrong Password!");
                    }
                }
            }
            return null;

        } //end UserLogin

        public static void UserRegister(ATM bank)
        {
            Account newAccount = new Account();
            newAccount.Name = UserEntry(usernameMessage, usernameExpression);
            newAccount.Password = UserEntry(passwordMessage, passwordExpression);
            bank.accounts.Add(newAccount);
            Console.Clear();
            Console.WriteLine("User added successfully.");
        } //end UserRegister

        public static void UserDeposit(ATM bank)
        {
            double amount = AmountEntry(amountEntry);
            bank.Deposit(amount);
            Console.Clear();
            Console.WriteLine($"Logged in as user: {bank.User.Name}");
            Console.WriteLine($"The amount {amount} was added successfully.");
        } //end UserDeposit

        public static void UserWithdraw(ATM bank)
        {
            double amount = AmountEntry(amountEntry);
            if (amount <= bank.CheckBalance())
            {
                Console.Clear();
                Console.WriteLine($"Logged in as user: {bank.User.Name}");
                Console.WriteLine($"The amount {amount} was withdrawn successfully.");
                bank.Withdraw(amount);
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Logged in as user: {bank.User.Name}");
                Console.WriteLine("Account balance not enough!");
            }
        } //end UserWithdraw

        public static void UserBalance(ATM bank)
        {
            Console.Clear();
            Console.WriteLine($"Logged in as user: {bank.User.Name}");
            Console.WriteLine($"Current Balance: ${bank.CheckBalance()}");
        } //end UserBalance

        public static void UserLogout(ATM bank)
        {

        } //end UserLogout

    }
}
