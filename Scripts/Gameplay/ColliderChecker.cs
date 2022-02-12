using System;
using UnityEngine;

public class ColliderChecker : MonoBehaviour
{
    private const string CORRECT_STAIR_TAG = "CorrectStair";
    private const string WRONG_STAIR_TAG = "WrongStair";
    private const string FRONT_COLLIDER_TAG = "FrontCollider";
    private const string FINISH_COLLIDER_TAG = "FinishCollider";
    private const string UP_COLLIDER_TAG = "UpCollider";
    private const string RESULT_COLLIDER_TAG = "ResultCollider";

    public event Action OnStackStair;
    public event Action OnUnStackStair;
    public event Action OnVictory;
    public event Action OnLose;

    [HideInInspector] public bool IsFinish = false;
    [HideInInspector] public bool IsLose = false;
    [HideInInspector] public bool IsCanBuild = false;
    [HideInInspector] public bool IsPhysicsEnabled = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(CORRECT_STAIR_TAG))
        {
            OnStackStair?.Invoke();
            other.gameObject.SetActive(false);

        }
        if (other.CompareTag(WRONG_STAIR_TAG))
        {
            other.gameObject.SetActive(false);
            OnUnStackStair?.Invoke();
        }
        if (other.CompareTag(FRONT_COLLIDER_TAG))
        {
            other.gameObject.SetActive(false);
            Player.Instance.SetBehaviorBuild();
        }
        if (other.CompareTag(FINISH_COLLIDER_TAG))
        {
            IsFinish = true;
            other.gameObject.SetActive(false);
            Player.Instance.SetBehaviorBuild();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(UP_COLLIDER_TAG))
        {
            if (IsLose)
            {
                Player.Instance.SetBehaviorIdle();
                other.isTrigger = false;
                OnLose?.Invoke();
            }
            else
            {
                Player.Instance.SetBehaviorWalking();
                other.isTrigger = false;
            }
        }

        if (other.CompareTag(RESULT_COLLIDER_TAG))
        {
            if (IsLose)
            {
                IsPhysicsEnabled = false;
                Player.Instance.SetBehaviorIdle();              
                OnLose?.Invoke();
            }
            else
            {
                Player.Instance.SetBehaviorIdle();
                other.isTrigger = false;
                OnVictory?.Invoke();
            }      
        }
    }

}
