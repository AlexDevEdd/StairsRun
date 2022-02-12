using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class PlayerBehaviorWalking : IPlayerBehavior
{
    private Player _player;
    private PlayerMovable _playerMoveble;
    private RagDollControl _ragDollControl;
    public PlayerBehaviorWalking(Player player, PlayerMovable playerMoveble, RagDollControl ragDollControl)
    {
        _player = player;      
        _playerMoveble = playerMoveble;
        _ragDollControl = ragDollControl;
    }

    public  void Enter()
    {
        _playerMoveble.moveXEnabled = true;

        if (_playerMoveble.moveXEnabled) 
            _ragDollControl.SetRunAnimation();
        Debug.Log("PlayerBehaviorWalking");
    }
    public void Exit()
    {     
        _playerMoveble.moveXEnabled = false;
    }

    public void Update()
    {
        if (_playerMoveble.moveXEnabled)
            _playerMoveble.LimitRangeX();
    }

    public void FixedUpdate()
    {
        if (_playerMoveble.moveXEnabled)
            _playerMoveble.ApplyForceZ();

    }

}


