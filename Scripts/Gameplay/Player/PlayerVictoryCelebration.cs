using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerVictoryCelebration : MonoBehaviour
{
    public UnityEvent OnVictory;

    public void Celebrate()
    {
        Animator animator = GetComponentInChildren<Animator>(true); 

        animator.gameObject.SetActive(true);
        animator.transform.localPosition = Vector3.zero;
        animator.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        animator.SetTrigger("Victory");
        OnVictory?.Invoke();
    }
}
