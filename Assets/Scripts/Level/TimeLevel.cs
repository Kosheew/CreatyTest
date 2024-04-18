using System.Collections;
using UnityEngine;
using CustomEventBus;
using CustomEventBus.Signals;

namespace Game.Level
{
    public class TimeLevel : MonoBehaviour, IService
    {
        [SerializeField] private int time;

        private EventBus _eventBus;
        public void Init()
        {
            _eventBus = ServiceLocator.Current.Get<EventBus>();

            StartCoroutine(TimerUpdate());
        }

        IEnumerator TimerUpdate()
        {
            while (time > 0)
            {
                time -= 1;

                int seconds = time % 60;
                int minutes = time / 60;

                string timer = string.Format("{00:00} : {01:00}", minutes, seconds);
                _eventBus?.Invoke(new OnTimerChange(timer));
                yield return new WaitForSeconds(1f);
            }
            _eventBus?.Invoke(new OnGameEnd());
        }    
    }
}