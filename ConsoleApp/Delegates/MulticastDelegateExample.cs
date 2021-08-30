using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Delegates
{
    public class MulticastDelegateExample
    {

        public delegate void ShowMessage(string s);

        public void Message1(string str)
        {
            Console.WriteLine($"1st message: {str}");
        }
        public void Message2(string str)
        {
            Console.WriteLine($"2nd message: {str}");
        }

        public void Test()
        {
            ShowMessage showMessage = null;
            showMessage += Message1;
            showMessage += Message2;
            showMessage += Console.WriteLine;

            showMessage("Hello!");

            showMessage -= Message2;
            showMessage("Bye Message2!");

            showMessage = Message2;
            showMessage("Hi!");
        }
    }
}
