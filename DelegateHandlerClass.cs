using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Delegates_n_Tasks
{
    class DelegateHandlerClass
    {

        public struct TaskParams
        {
            public int DelayTime;
            public int TaskId;
            public TaskParams(int delayTime, int taskId)  //struct construtor, blir snyggt med new där den används.
            {
                DelayTime = delayTime;
                TaskId = taskId;
            }
        }

        //Detta är en delegat som agerar som interface.
        public delegate double ResultHandlerDelegate(double value1, double value2); //Tänk klass ish
        public ResultHandlerDelegate MathDelegate;                                      //tänk objekt ish
        public DelegateHandlerClass()
        {
            MathDelegate = new ResultHandlerDelegate(AddNumbers);                             
        }

        public double AddNumbers(double value1, double value2)
        {
            return value1 + value2;
        }

        //detta är en CALLBACK delegat
        public delegate void WheretoCall(string status); // Step 1
        public WheretoCall wheretocall = null; // Step 2
        public void Search() //Vi låssas att detta är egen task som söker efter en fil på HD.
        {
            wheretocall("Filen du letade efter ligger här"); // Step 3. This is where i "raise" the event
        }




        public void run()
        {
            TaskParams taskParams = new TaskParams(2000, 666);
            Task taskD = new Task(FireDelegate, taskParams); // I like this one!!!, to bad i cant pass the taskD.Id
            taskD.Start();
            taskD.Wait();
        }

        public async void FireDelegate(object? _taskParams)
        {
            TaskParams taskParams = (TaskParams)_taskParams; //Vid prod kod skall det såklart in try/catch på flera ställen.
            while (true)
            {
                await Task.Delay(taskParams.DelayTime);
                //Debug.WriteLine($"FireEvent {taskParams.TaskId}");
                EventArgs emptyEventArgs = new EventArgs();
                string MyString = "HEJ ";
                Search(); // Varje gång denna anropas, låtsas vi att vi har hittat en fil vi letat efter.
            }
        }



    }
}
