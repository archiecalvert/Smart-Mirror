using Smart_Mirror_Version_2.Classes.Managers;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Smart_Mirror_Version_2.Classes
{
    public class Timer : Component
    {
        
        public float duration;
        public float MaxTime;
        public bool isActive = false;
        public Timer(float Duration)
        {
            duration = Duration;
            MaxTime = Duration;
            isActive = false;
            OnCompletion = () => { return; };
        }
        public void Start()
        {
            isActive = true;
        }
        public float PercentageRemaining()
        {
            return duration / MaxTime * 100;
        }
        public override void Update()
        {
            if (isActive)
            {
                duration -= 1F / Main.FPS;
                if (duration < 0)
                {
                    isActive = false;
                    SystemHandler.Components.Remove(this);
                    OnCompletion();
                }
            }
        }
        public override void Draw()
        {
            return;
        }
        public ActionManager.Event OnCompletion;
    }
}
    /*
    public class Timer : Component
    {
        Stopwatch timer;
        float duration;
        public bool isActive = false;
        public Timer(float DurationSeconds)
        {
            timer = new Stopwatch();
            duration = DurationSeconds;
            OnCompletion = () =>
            {
                SystemHandler.Components.Remove(this);
            };
        }
        public ActionManager.Event OnCompletion;
        public void Start()
        {
            timer.Start();
            isActive = true;
        }
        public float SecondsRemaining()
        {
            return duration - (timer.ElapsedMilliseconds/1000);
        }
        public float PercentageRemaining()
        {
            return 100 - (timer.ElapsedMilliseconds/ (1000*duration)) * 100;
        }
        public override void Update()
        {
            if(PercentageRemaining() < 0)
            {
                timer.Stop();
                OnCompletion();
                isActive = false;
            }
        }
        public override void Draw()
        {
            return;
        }
    }

}*/