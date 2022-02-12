using UnityEngine;

public class RagDollControl : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody[] _rigidbodies;
    [SerializeField] private CapsuleCollider[] capsuleColliders;

    private int _runHash = Animator.StringToHash("IsRun");
    private int _idleHash = Animator.StringToHash("IsIdle");
    private int _climbHash = Animator.StringToHash("IsClimb");
    private int _danceHash = Animator.StringToHash("IsDance");

    private void Awake()
    {
        for (int i = 0; i < _rigidbodies.Length; i++)
        {
            _rigidbodies[i].isKinematic = true;

        }
        for (int i = 0; i < capsuleColliders.Length; i++)
        {
            capsuleColliders[i].enabled = false;
        }
    }

    public void AnimationEnable(bool isEnable)
    {
        _animator.enabled = isEnable;
    }
    public void MakePhysical()
    {
        Player.Instance.gameObject.GetComponent<Rigidbody>().freezeRotation = false;
        AnimationEnable(false);

        for (int i = 0; i < _rigidbodies.Length; i++)
        {
            _rigidbodies[i].isKinematic = false;
        }
        for (int i = 0; i < capsuleColliders.Length; i++)
        {
            capsuleColliders[i].enabled = true;
        }
    }

    public void MakeUnPhysical()
    {
        Player.Instance.gameObject.GetComponent<Rigidbody>().freezeRotation = true;
        AnimationEnable(true);

        for (int i = 0; i < _rigidbodies.Length; i++)
        {
            _rigidbodies[i].isKinematic = true;
        }
        for (int i = 0; i < capsuleColliders.Length; i++)
        {
            capsuleColliders[i].enabled = false;
        }
    }


    public void SetRunAnimation()
    {
        _animator.SetBool(_runHash, true);
        _animator.SetBool(_idleHash, false);
        _animator.SetBool(_climbHash, false);

    }
    public void SetClimbAnimation()
    {
        _animator.SetBool(_climbHash, true);
        _animator.SetBool(_idleHash, false);
        _animator.SetBool(_runHash, false);
    }
    public void SetIdleAnimation()
    {
        _animator.SetBool(_idleHash, true);
        _animator.SetBool(_climbHash, false);
        _animator.SetBool(_runHash, false);
    }
    public void SetDanceAnimation()
    {
        _animator.SetBool(_danceHash, true);
        _animator.SetBool(_idleHash, false);
        _animator.SetBool(_climbHash, false);
        _animator.SetBool(_runHash, false);
    }
}
