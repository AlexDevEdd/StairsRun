using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class PlayerBehaviorClimb : IPlayerBehavior
{
    private Player _player;
    private PlayerMovable _playerMoveble;
    private RagDollControl _ragDollControl;
    public PlayerBehaviorClimb(Player player, PlayerMovable playerMoveble, RagDollControl ragDollControl)
    {
        _player = player;     
        _playerMoveble = playerMoveble;
        _ragDollControl = ragDollControl;
    }

    public void Enter()
    {
        _playerMoveble.moveXEnabled = false;
        _playerMoveble.moveYEnabled = true;

        if (_playerMoveble.moveYEnabled)       
            _ragDollControl.SetClimbAnimation();
        Debug.Log("PlayerBehaviorClimb");
    }

    public void Exit()
    {       
        _playerMoveble.moveYEnabled = false;
        _playerMoveble.moveXEnabled = true;
    }

    public void Update()
    {
       
    }
    public  void FixedUpdate()
    {
        if (_playerMoveble.moveYEnabled)
        {
            _playerMoveble.ApplyForceY();
        }

    }

}



