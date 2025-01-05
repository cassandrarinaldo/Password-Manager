using System.IO;
using System.Text.RegularExpressions;

namespace Projekt1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string str = args[0];
            switch (str)
            {
                case "init":
                    if (args.Length == 3)
                    {
                        string masterpwd1 = createMasterpwd();
                        Init command1 = new Init(args[1], args[2], masterpwd1);
                        Secret command7 = new Secret(args[1]);
                        Console.WriteLine(command7.returnSecret());
                    }
                    else
                    {
                        Console.WriteLine("Something went wrong, try again.");
                    }
                    break;
                case "create":
                    if (args.Length == 3 && File.Exists(args[2]))
                    {
                        string masterpwd2 = userInput("Enter your masterpassword: ");
                        string secret_Key = userInput("Enter your Secret Key: ");
                        Create command2 = new Create(args[1], args[2], masterpwd2, secret_Key);
                    }
                    else
                    {
                        Console.WriteLine("Something went wrong, try again.");
                    }
                    break;
                case "get":
                    if ((args.Length == 4 || args.Length == 3) && File.Exists(args[1]) && File.Exists(args[2]))
                    {
                        string masterpwd3 = userInput("Enter your masterpassword: \r"); //
                        if (args.Length == 4)
                        {
                            Get command3 = new Get(args[1], args[2], masterpwd3);
                            command3.GetPwd(args[3]);
                        }
                        else
                        {
                            Get command3 = new Get(args[1], args[2], masterpwd3);
                            command3.GetDomain();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Something went wrong, try again.");
                    }
                    break;
                case "set":
                    if ((args.Length == 4 || (args.Length == 5 && (args[4] == "-g" || args[4] == "--generate"))) && File.Exists(args[1]) && File.Exists(args[2]))
                    {
                        string masterpwd4 = userInput("Enter your masterpassword: ");
                        Set command4 = new Set(args[1], args[2], masterpwd4);
                        string value = "";
                        if (args.Length == 5)
                        {
                            value = getRandomValue();
                            Console.WriteLine("Your password is: " + value);
                        }
                        else
                        {
                            value = userInput("Enter a password for given domain: ");
                        }
                        command4.SetDomain(args[3], value);
                    }
                    else
                    {
                        Console.WriteLine("Something went wrong, try again.");
                    }
                    break;
                case "delete":
                    if (args.Length == 4 && File.Exists(args[1]) && File.Exists(args[2]))
                    {
                        string masterpwd5 = userInput("Enter your masterpassword: ");
                        Delete command5 = new Delete(args[1], args[2], masterpwd5);
                        command5.DeleteDomain(args[3]);
                    }
                    else
                    {
                        Console.WriteLine("Something went wrong, try again.");
                    }
                    break;
                case "secret":
                    if (args.Length == 2 && File.Exists(args[1]))
                    { 
                        Secret command6 = new Secret(args[1]);
                        Console.WriteLine(command6.returnSecret());
                    }
                    else
                    {
                        Console.WriteLine("Something went wrong, try again.");
                    }
                    break;
                default:
                    Console.WriteLine("Something went wrong, try again.");
                    break;
            }
            
            static string userInput(string instructions)
            {
                Console.WriteLine(instructions);
                string input =Console.ReadLine();
                return input;
            }
            static string getRandomValue()
            {
                string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                Random random = new Random();
                string pwd = "";
                for (int i = 0; i < 20; i++) {
                    pwd += chars[random.Next(chars.Length)];
                }
                return pwd;
            }
            static string createMasterpwd()
            {
                string masterpwd = userInput("Choose your masterpassword: ");
                return masterpwd;
            }
        }
    } 
}

