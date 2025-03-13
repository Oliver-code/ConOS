using Encryption;
using Functions;

class Program
{

    static void Logon(string user)
    {

        Console.WriteLine("Welcome!");
        
    }

    static void Main()
    {

        if (!Directory.Exists("C"))
        {
            Console.WriteLine($"{Environment.CurrentDirectory}\\C does not exist");
            Control.Pause("key");
            Console.WriteLine();
            Environment.Exit(1);
        }

        if (!Directory.Exists("C\\Reg"))
        {
            Console.WriteLine("Reg does not exist, cannot continue");
            Console.Write("Press any key to Exit . . . ");
            Console.ReadKey();
            Environment.Exit(1);

        }

        string finst = File.ReadAllText("C\\Reg\\inst\\finst.int");

        if (finst == "1")
        {

            Console.WriteLine("Welcome to ConOS!");
            Control.Pause("key");
        usrnme:
            Console.WriteLine("\nEnter the name of your user below");
            Console.Write(">>");
            string usrnme = Console.ReadLine();
            if (usrnme == "Administrator")
            {
                Console.WriteLine("Cannot name user Administrator");
                goto usrnme;


            }

            Console.WriteLine("Enter password below");
            
            string pass = Encode.HashSHA512(Console.ReadLine());

            Console.WriteLine("Enter password for administrator (an account with admin privileges)");
            string adminpass = Console.ReadLine();

            Directory.CreateDirectory($"C\\Users\\{usrnme}");
            File.WriteAllText($"C\\Users\\{usrnme}\\pass.str", pass);
            Directory.CreateDirectory("C\\Users\\Administrator");
            File.WriteAllText("C\\Users\\Administrator\\pass.str", Encode.HashSHA512(adminpass));
            Console.WriteLine("Finished setup! continue to load up ConOS");
            Control.Pause("key");
            Console.WriteLine();
            File.WriteAllText("C\\Reg\\inst\\finst.int", "0");
            Console.Clear();
            Main();

        }

        Console.WriteLine("(L)ogin (C)reate user (R)eset application l(I)st users");
        ConsoleKeyInfo key = Console.ReadKey();
        char chr = key.KeyChar;
        string inpt = $"{chr}";

        if (inpt == "L")
        {
        usr:
            Console.WriteLine("\nEnter username");
            string user = Console.ReadLine();
            if (user == "")
            {

                Console.WriteLine("The user does not exist");
                goto usr;

            }
            if (!Directory.Exists(@$"C\Users\{user}"))
            {

                Console.WriteLine("The user does not exist");
                goto usr;

            }
        pass:
            Console.WriteLine($"Enter password for {user}");
            string pass = Encode.HashSHA512(Console.ReadLine());
            if (pass != File.ReadAllText($@"C\Users\{user}\pass.str"))
            {

                Console.WriteLine("Incorrect password");
                goto pass;
            }
            else
            {
                Logon(user);
            }
        }

        if (inpt == "C")
        {
        nmeadd:

            Console.WriteLine("\nEnter username");
            string usrnme = Console.ReadLine();
            if (Directory.Exists(@$"C\Users\{usrnme}"))
            {

                Console.WriteLine("User already exists");
                goto nmeadd;
            }
            Directory.CreateDirectory(@$"C\Users\{usrnme}");
            Console.WriteLine($"Enter {usrnme}'s password");
            string pass = Encode.HashSHA512(Console.ReadLine());
            File.WriteAllText(@$"C\Users\{usrnme}\pass.str", pass);
            Console.WriteLine($"Created User '{usrnme}'");
            Main();


        }

    }

}