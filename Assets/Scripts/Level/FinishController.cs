using CustomEventBus;
using CustomEventBus.Signals;
using UnityEngine;

namespace Game.Level
{
    public class FinishController : MonoBehaviour, IService
    {
        private EventBus _eventBus;
        public int MaxKeys { get; private set; }

        public void Init()
        {
            _eventBus = ServiceLocator.Current.Get<EventBus>();
            MaxKeys = GameObject.FindGameObjectsWithTag(StringConstant.Key).Length;
            _eventBus.Subscribe<OnScoreChange>(ScoreChange);
        }

        private void ScoreChange(OnScoreChange signal)
        {
            if (signal.Value >= MaxKeys)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<OnScoreChange>(ScoreChange);
        }
    }
}