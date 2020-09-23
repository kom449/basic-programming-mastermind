using System;
using System.Collections.Generic;
using System.Runtime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Mastermind
{
    public class hardcore
    {
        public static bool HCmode;
        public static void HCversion()
        {
            Console.Clear();
            Thread.Sleep(1000);
            Console.WriteLine("So... You have chosen death.");
            Thread.Sleep(5000);
            Console.Clear();
            HCmode = true;
            Program.settings();
        }

        public string Decrypt(string str){
            byte[] b;
            string decrypted;
            try{
                b = Convert.FromBase64String(str);
                decrypted = Encoding.ASCII.GetString(b);
            }
            catch (FormatException){
                decrypted = "";
            }
            return decrypted;
        }

        public string Encrypt(string str){
            byte[] b = Encoding.ASCII.GetBytes(str);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;
        }
    }
}
