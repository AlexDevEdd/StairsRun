using Gamebase.Systems.GlobalEvents;
using UnityEngine;

public class LevelLose : MonoBehaviour
{
    private ColliderChecker _colliderChecker;
    private RagDollControl _ragDoll;
    private StackStair _stackStair;

    private void Awake()
    {
        _colliderChecker = FindObjectOfType<ColliderChecker>();
        _ragDoll = FindObjectOfType<RagDollControl>();
     
        _colliderChecker.OnLose += Lose;
    }

    private void Lose()
    {
       // _ragDoll.AnimationEnable(false);
        _ragDoll.MakePhysical();
           
        GlobalEventsSystem.Instance.Invoke(GlobalEventType.Lose);
    }
}

