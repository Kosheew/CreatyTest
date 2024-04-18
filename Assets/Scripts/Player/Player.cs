using CustomEventBus;
using CustomEventBus.Signals;
using UnityEngine;


public class Player : MonoBehaviour, IService
{
    private EventBus _eventBus;

    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(StringConstant.Key))
        {
            _eventBus?.Invoke(new OnAddKey());
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag(StringConstant.Finish))
        {
            _eventBus?.Invoke(new OnGameEnd());
        }
    }
}
