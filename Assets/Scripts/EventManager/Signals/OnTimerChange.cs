using UnityEngine;

namespace CustomEventBus.Signals
{ 
    public class OnTimerChange
    {
        public readonly string Value;

        public OnTimerChange(string value)
        {
            Value = value;
        }
    }
}