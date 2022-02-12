using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovable : MonoBehaviour
{

    [SerializeField] private float _moveForceZ = 1f;
    [SerializeField] private float _moveForceX = 25f;
    [SerializeField] private Vector2 moveRangeX = new Vector2(-2f, 2f);
    [SerializeField] private float _moveForceY = 0.5f;

    private Rigidbody _rigidbody;

    [HideInInspector] public bool moveXEnabled = false;
    [HideInInspector] public bool moveYEnabled = false;
    public float MoveForceY => _moveForceY;
    public float MoveForceZ => _moveForceZ;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
   
    public void ApplyForceZ()
    {
        _rigidbody.AddForce(Vector3.forward * _moveForceZ * Time.fixedDeltaTime, ForceMode.Acceleration);
    }

    public void ApplyForceX(float forceRatio)
    {
        _rigidbody.AddForce(Vector3.right * forceRatio * _moveForceX * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }
    public void ApplyForceY()
    {     
        _rigidbody.AddForce(Vector3.up * _moveForceY * Time.fixedDeltaTime, ForceMode.Acceleration);
    }
    public void LimitRangeX()
    {
        if (transform.position.x > moveRangeX.y)
        {
            _rigidbody.velocity = new Vector3(0f, _rigidbody.velocity.y, _rigidbody.velocity.z);
            transform.position = new Vector3(moveRangeX.y, transform.position.y, transform.position.z);
            return;
        }
        if (transform.position.x <= moveRangeX.x)
        {
            _rigidbody.velocity = new Vector3(0f, _rigidbody.velocity.y, _rigidbody.velocity.z);
            transform.position = new Vector3(moveRangeX.x, transform.position.y, transform.position.z);
            return;
        }
    }

    public void MoveLeft()
    {
        if (moveXEnabled)
            _rigidbody.AddForce(Vector3.left * _moveForceX * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }

    public void MoveRight()
    {
        if (moveXEnabled)
            _rigidbody.AddForce(Vector3.right * _moveForceX * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }
}

