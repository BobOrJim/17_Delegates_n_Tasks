using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Delegates_n_Tasks
{
    public class EventHandlerClass
    {
        //Här vill jag invertera deps, så att när denna skjuter, skall main bli informerad.

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

        public event EventHandler MyEvent;
        public void run()
        {
            TaskParams taskParams = new TaskParams(2000, 666);
            Task taskD = new Task(FireEvent, taskParams); // I like this one!!!, to bad i cant pass the taskD.Id
            taskD.Start();
            taskD.Wait();
            //Debug.WriteLine($" TaskD id = {taskD.Id}");
        }



        public async void FireEvent(object? _taskParams)
        {
            TaskParams taskParams = (TaskParams)_taskParams; //Vid prod kod skall det såklart in try/catch på flera ställen.
            while (true)
            {
                await Task.Delay(taskParams.DelayTime);
                //Debug.WriteLine($"FireEvent {taskParams.TaskId}");
                EventArgs emptyEventArgs = new EventArgs();
                this.MyEvent(this, emptyEventArgs); //Alright, dags att höja flaggan, kanske lyssnar någon eller inte, det är iof inget jag oroar mig för.
            }
        }
    }
}
