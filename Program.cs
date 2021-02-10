using System;
using System.Diagnostics;
using System.Threading;

namespace Delegates_n_Tasks
{
    class Program
    {
        static void Main(string[] args) //Allt nedan funkar, bara att ta bort kommenterarer för att testa. //Det har förekommit slav vid namnsättning, alla Event skall heta event på slutet, o alla deligater Delates på slutet.
        {
            Debug.WriteLine("");
            Debug.WriteLine("");
            Debug.WriteLine(" Testing debugger print.");
            Debug.WriteLine("");
            Debug.WriteLine("");

            TaskClassA TaskObjectA = new TaskClassA();
            //TaskObjectA.DelayMethodA();
            //TaskObjectA.DelayMethodB();

            TaskClassB taskClassB = new TaskClassB();
            //taskClassB.DelayMethodA();

            //Nedan är egenteligen orelevant. Task och Thread är olika saker, och jag skall alltid använda Task.
            //Jag måste sluta och slösa tid på detta, och lita på Microsofts inplementation av Task.
            while (false)
            {
                ProcessThreadCollection currentThreads = Process.GetCurrentProcess().Threads;
                Debug.WriteLine($" Då slänger vi ett öga i main över alla trådar igen:");
                foreach (ProcessThread thread in currentThreads)
                {
                    Debug.WriteLine($"Tråd id som körs = {thread.Id}");
                }
                Thread.Sleep(5000);
            }


            //Nedan prenumererar jag på "vanligt" event
            EventHandlerClass eventHandlerClass = new EventHandlerClass();
            eventHandlerClass.MyEvent += eventHandlerClassListner;
            static void eventHandlerClassListner(object sender, object? e)
            {
                Debug.WriteLine($"eventHandlerClassListner metod i Main heard its event fire. The sender was: {sender.ToString()}");
                //Environment.Exit(0); Denna stänger av. Kan användas till div bra grejor i framtiden.
            }
            eventHandlerClass.run();

            //kompilator "markören" måste inte nödvändigtviss "ligga" någonstans.PC miljö är asynkron. 
            //Detta innebär rent praktiskt att jag bygger appar som är helt baserade på events, see mitt 17_delatetes_n_events projektet.


            //Här anropas callback deligaten. Dvs denna kod är typ exakt som koden för ett event. duh.
            DelegateHandlerClass f1 = new DelegateHandlerClass();
            f1.wheretocall += CallHere;  //Dvs, när wheretocall anropas i DelatateHandlerClass, kommer CallHere anropas här. Man kan säga att jag skickar en funktionspekare till en funktion :)
            f1.run();
            static void CallHere(string message)
            {
                Debug.WriteLine(message);
            }



            //Med Action

            //Med Func







            Console.ReadLine();
        }
    }
}
