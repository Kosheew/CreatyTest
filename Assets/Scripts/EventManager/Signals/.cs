using UnityEngine;

namespace CustomEventBus.Signals
{
    public class OnTimerChange
    {
        public readonly int Value;

        public OnTimerChange(int value)
        {
            Value = value;
        }
    }
}


