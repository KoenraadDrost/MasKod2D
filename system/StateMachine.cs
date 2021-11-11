using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MasKod2D.state
{
    class StateMachine
    {
        public void SwitchState()
        {

        }

        public void RunScript()
        {
            //          string script = @"    
            //-- defines a factorial function
            //function fact (n)
            //	if (n == 0) then
            //		return 1
            //	else
            //		return n*fact(n - 1)
            //	end
            //end

            //return fact(5)";

            Script script = new Script(0);
            // D:\My Documents\Studie\Jaar 4\Semester 2 Gamesprogramming\Algorythms and Artificial Intelligence\PairProject\MasKod2D\state\Roam.lua
            StreamReader sr = new StreamReader("D:/My Documents/Studie/Jaar 4/Semester 2 Gamesprogramming/Algorythms and Artificial Intelligence/PairProject/MasKod2D/script/state/Roam.lua");
            string tempString = sr.ReadToEnd();
            //Console.WriteLine($"script result: {Script.RunFile("Roam")} ");
            DynValue res = Script.RunString(tempString);
            Console.WriteLine("Script result: " + res);
            //return res.Number;
        }
    }
}
