using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Delegates_n_Tasks
{
    class TaskClassB //
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


        public void DelayMethodA()
        {
            Thread.CurrentThread.Name = "ThreadClassA";
            Debug.WriteLine($"Hello from thread running DelayMethodA in ThreadClassA : { Thread.CurrentThread.Name} ");

            Task taskA = new Task(() => Console.WriteLine(""));
            taskA.Start();
            Debug.WriteLine($" TaskA id = {taskA.Id}");

            Task taskB = new Task(() => Console.WriteLine(""));
            taskB.Start();
            Debug.WriteLine($" TaskB id = {taskB.Id}");

            Task taskC = new Task(PrintMethodThreadSleep);
            Debug.WriteLine($" TaskC id = {taskC.Id}");
            taskC.Start();

            TaskParams taskDParams = new TaskParams(2000, 666);
            Task taskD = new Task(PrintMethodThreadSleepWithId, taskDParams); // I like this one!!!, to bad i cant pass the taskD.Id
            taskD.Start();
            Debug.WriteLine($" TaskD id = {taskD.Id}");

            TaskParams taskEParams = new TaskParams(1000, 667);
            Task taskE = new Task(PrintMethodThreadSleepWithId, taskEParams);
            taskE.Start();
            Debug.WriteLine($" TaskE id = {taskE.Id}");

            taskE.Wait();
            taskD.Wait();
            taskC.Wait();
            taskA.Wait();
            taskB.Wait();
        }

        public async void PrintMethodThreadSleepWithId(object? _taskParams) //See Task API.
        {
            TaskParams taskParams = (TaskParams)_taskParams; //Vid prod kod skall det såklart in try/catch på flera ställen.
            while (true)
            {
                await Task.Delay(taskParams.DelayTime);
                Debug.WriteLine($"PrintMethodThreadSleepWithId {taskParams.TaskId}");
            }
        }

        public void PrintMethodThreadSleep2() //Funkar också men jag gillar PrintMethodThreadSleep bättre.
        {
            Debug.WriteLine($"ASDFASDF :  ");
            Thread.Sleep(2000);
            Debug.WriteLine($"ASDFASDF  ");
        }

        public async void PrintMethodThreadSleep()
        {
            Debug.WriteLine($"ASDFASDF :  ");
            await Task.Delay(2000);
            Debug.WriteLine($"ASDFASDF  ");
        }

    }
}
