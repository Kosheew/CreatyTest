using CustomEventBus;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Level
{
    public class InitGame : MonoBehaviour
    {
        [SerializeField] private UserInterface _userInterface;
        [SerializeField] private Player _player;
        [SerializeField] private TimeLevel _timeLevel;
        [SerializeField] private SceneController _sceneController;
        [SerializeField] private FinishController _finishController;

        private EventBus _eventBus;
        private ScoreController _scoreController;

        private List<IDisposable> _disposables = new List<IDisposable>();

        private void Awake()
        {
            _eventBus = new EventBus();
            _scoreController = new ScoreController();

            RegisterServices();
            Init();
            AddDisposables();
        }

        private void RegisterServices()
        {
            ServiceLocator.Initialize();
            ServiceLocator.Current.Register(_eventBus);
            ServiceLocator.Current.Register(_scoreController);
            ServiceLocator.Current.Register<FinishController>(_finishController);
            ServiceLocator.Current.Register<UserInterface>(_userInterface);
            ServiceLocator.Current.Register<Player>(_player);
            ServiceLocator.Current.Register<TimeLevel>(_timeLevel);
            ServiceLocator.Current.Register<SceneController>(_sceneController);
        }

        private void Init()
        {
            _scoreController.Init();  
            _finishController.Init();
            _userInterface.Init();
            _player.Init();
            _timeLevel.Init();
            _sceneController.Init();
        }

        private void AddDisposables()
        {
            _disposables.Add(_scoreController);
        }

        private void OnDestroy()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
    }
}