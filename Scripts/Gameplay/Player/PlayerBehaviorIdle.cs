using System;
using UnityEngine;

public class PlayerBehaviorIdle : IPlayerBehavior
{
    private Player _player;
    private PlayerMovable _playerMoveble;
    private RagDollControl _ragDollControl;
   
    public PlayerBehaviorIdle(Player player, PlayerMovable playerMoveble, RagDollControl ragDollControl)
    {
        _player = player;
        _playerMoveble = playerMoveble;
        _ragDollControl = ragDollControl;
       
    }
    public void Enter()
    {
      
        _ragDollControl.SetIdleAnimation();
        _playerMoveble.moveXEnabled = false;
        _playerMoveble.moveYEnabled = false;

        Debug.Log("PlayerBehaviorIdle");
    }


    public void Exit()
    {

    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {

    }
}


