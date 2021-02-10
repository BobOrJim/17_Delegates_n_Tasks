using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;


namespace Delegates_n_Tasks
{
    public class TaskClassA //Dessa tasks körs på samma tråd som main.
    {

        public async Task DelayMethodA()
        {
            var a = Task.CurrentId.HasValue;
            Debug.WriteLine(a);
            int counter = 0;
            while (true)
            {
                await Task.Delay(1000);
                Debug.WriteLine($" Printing from DelayMethodA in TaskClass. Counter = {counter}");
                counter += 1;
            }
        }

        public async Task DelayMethodB()
        {
            var a = Task.CurrentId.HasValue;
            Debug.WriteLine(a);
            int counter = 0;
            while (true)
            {
                await Task.Delay(2000);
                Debug.WriteLine($" Printing from DelayMethodB in TaskClass. Counter = {counter}");
                counter += 1;
            }
        }


    }
}
