using Gamebase.Systems.GlobalEvents;
using UnityEngine;

public class LevelVictory : MonoBehaviour
{
    private ColliderChecker _colliderChecker;
    private RagDollControl _ragDoll;
  
    private void Awake()
    {
        _colliderChecker = FindObjectOfType<ColliderChecker>();
        _ragDoll = FindObjectOfType<RagDollControl>();
        _colliderChecker.OnVictory += Victory;
    }

    private void Victory()
    {       
        _ragDoll.SetDanceAnimation();
        _ragDoll.gameObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        GlobalEventsSystem.Instance.Invoke(GlobalEventType.Victory);
    }
}

