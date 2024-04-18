using CustomEventBus;
using CustomEventBus.Signals;
using System;

namespace Game.Level
{
    public class ScoreController : IService, IDisposable
    {
        private EventBus _eventBus;
        private int _score;

        public void Init()
        {
            _eventBus = ServiceLocator.Current.Get<EventBus>();
            _eventBus.Subscribe<OnAddKey>(OnScoreAdded);
        }

        private void OnScoreAdded(OnAddKey signal)
        {
            _score++;
            _eventBus.Invoke(new OnScoreChange(_score));
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<OnAddKey>(OnScoreAdded);
        }
    }
}