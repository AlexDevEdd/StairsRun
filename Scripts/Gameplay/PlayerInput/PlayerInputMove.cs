using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInputMove : MonoBehaviour, IBeginDragHandler, IDragHandler
{
   
    [SerializeField] private Transform _player; 
    private PlayerMovable _playerMoveble;

    private void Awake()
    {
        _playerMoveble = FindObjectOfType<PlayerMovable>();     
    }

    public void OnBeginDrag(PointerEventData eventData)
    {       

    }

    public void OnDrag(PointerEventData eventData)

    {
        if (Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y))
        {
            if (_playerMoveble.moveXEnabled)
            {
                if (eventData.delta.x > 0)                
                    _playerMoveble.MoveRight();

                else
                    _playerMoveble.MoveLeft();               
            }
           
        }
    }
}
