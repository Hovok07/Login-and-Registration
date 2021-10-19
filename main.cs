using System;
using System.IO;
using System.Threading.Tasks;

namespace Application
{
    abstract class DataSave
    {
        protected  string filename;


        protected void MakeFile()
        {
            if(!File.Exists(this.filename))
            {
                File.Create(this.filename);
            }
        }

        public abstract void WriteToFile();
     
        public  abstract bool ReadFile();
    }
    class Player : DataSave
    {
        public string username;
        private string password;
        public string Password
        {
            get { return password; }
            set { this.password = value; }
        }


        public Player()
        {
            base.filename = "PlayerData.txt";
            base.MakeFile();
        }


        public override bool ReadFile()
        {
           using(StreamReader sr = File.OpenText(base.filename))
            {
                string s;

                while((s = sr.ReadLine()) != null)
                {
                    if(s.Contains(this.username) && s.Contains(this.password))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public override void WriteToFile()
        {
            if (this.password.Length <= 0 || this.username.Length <= 0)
            {
                return;
            }

            string line = "USERNAME: " + this.username + " PASSWORD: " + this.password;



            using(StreamWriter sw = File.AppendText(filename))
            {
                sw.WriteLine(line);
            }
        }

    }
    class Program
    {
        public static void Main(string[] args)
        {

            bool running = true;

            while(running)
            {
                string username;
                string password;
                int choice = 0;

                Console.Write("Enter your username: ");
                username = Console.ReadLine();

                Console.Write("Enter your password: ");
                password = Console.ReadLine();


                Player LocalPlayer = new Player();


                LocalPlayer.username = username;
                LocalPlayer.Password = password;


                Console.WriteLine("Make sure you answer 1 or 2");
                Console.WriteLine("1.Login");
                Console.WriteLine("2.Sign Up");
                choice = Convert.ToInt32(Console.ReadLine());


                switch (choice)
                {
                    case 1:
                        if (LocalPlayer.ReadFile() == false)
                        {
                            Console.WriteLine("You don't have an account, please make on one");


                        }
                        else
                        {
                            Console.WriteLine("Thanks for testing my project");

                            running = false;
                        }
                        break;
                    case 2:
                        if (LocalPlayer.ReadFile() == true)
                        {
                            Console.WriteLine("You already have an account, please login");

                        }
                        else
                        {
                            Console.WriteLine("We are making you an account");

                            LocalPlayer.WriteToFile();
                        }
                        break;

                }
            }

        }
    }
}