using CustomEventBus;
using CustomEventBus.Signals;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Level
{
    public class UserInterface : MonoBehaviour, IService
    {
        [SerializeField] private Text _timerText;
        [SerializeField] private Text _scoreText;

        private EventBus _eventBus;
        private FinishController _finishController;
        private int _counter;

        public void Init()
        {
            _eventBus = ServiceLocator.Current.Get<EventBus>();
            _finishController = ServiceLocator.Current.Get<FinishController>();
            _eventBus.Subscribe<OnTimerChange>(ChangeTimer);
            _eventBus.Subscribe<OnScoreChange>(ScoreChange);
            _counter = _finishController.MaxKeys;

            _scoreText.text = $"0 / {_counter}";
        }

        private void ChangeTimer(OnTimerChange signal)
        {
            _timerText.text = signal.Value;
        }

        private void ScoreChange(OnScoreChange signal)
        {
            _scoreText.text = $"{signal.Value} / {_counter}";
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<OnTimerChange>(ChangeTimer);
            _eventBus.Unsubscribe<OnScoreChange>(ScoreChange);
        }
    }
}