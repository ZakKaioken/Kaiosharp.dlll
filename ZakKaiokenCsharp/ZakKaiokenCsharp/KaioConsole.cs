using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaiosharp
{
    public class KaioConsole
    {
        /*
         * Since I am putting my open source dll on github im gonna have to refine the code make it look
         * nicer for you people
        */


        #region AskTellWait
        /*Basic Ask, Tell and Wait functions
        Tell is used to talk to the person reading the console application.
        it's syntax is Tell(variable); I have custom tells that help visualize the information you put through it.

        Ask is basically a prompt kinda like the "set /p variable=var:" code item used in the cmd batch scripting language
        syntax: answer = ask(question); //I'll setup an example project on the side to show them off with examples
        */
        public static string Ask(string question, ConsoleColor cc = ConsoleColor.White)
        {
            Tell(question,cc);
            string answer = Console.ReadLine();
            return answer;
        }
        public static void Tell<T>(T message, ConsoleColor cc=ConsoleColor.White)
        {
            Console.ForegroundColor = cc;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        public static void Wait()
        {
            Console.ReadKey();
        }

        #endregion


        //useful for scaling values (example: 0-1 to 0-255)
        public static float remapValue(float value, float start1, float end1, float start2, float end2)
        {
            float endzone = start2 + (end2 - start2) * ((value - start1) / (end1 - start1));
            return endzone;
        }

        //makes understanding aspect ratios much easier
        public static void Tell(AspectRatio aspectRatio)
        {
            Tell("Aspect Ratio: " + aspectRatio.width + "/" + aspectRatio.height + " (" + aspectRatio.ratio + ").");
        }
        //tells all the items in a string array
        public static void Tell (string[] strings)
        {
            foreach (string st in strings)
            {
                Tell(st);
            }
        }
        //tells all the items in a string list
        public static void Tell(List<string> strings)
        {
            foreach (string st in strings)
            {
                Tell(st);
            }
        }
        //tells people if something is true or false
        public static void Tell(bool bol)
        {
            if (bol) {
                Tell("Yeah. that is true.");
            } else
            {
                Tell("No way. That is false.");
            }
        }

        //linearly interpolates two values. it's useful when combined with the remap values function.
        public static float Lerp(float firstFloat, float secondFloat, float by)
        {
            return firstFloat * by + secondFloat * (1 - by);
        }


        //if someone could change this function to IsDivisible<t>(t x, t n) please do. (but dont break it)
        public static bool IsDivisible(int x, int n)
        {
            return (x % n) == 0;
        }
        //I use this to help generate aspect ratios
        public static bool IsDivisible(float x, float n)
        {
            return (x % n) == 0;
        }

    }
}
