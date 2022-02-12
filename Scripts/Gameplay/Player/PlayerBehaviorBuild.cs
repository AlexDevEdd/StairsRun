using System;
using UnityEngine;

public class PlayerBehaviorBuild : IPlayerBehavior
{
    private Player _player;
    private PlayerMovable _playerMoveble;
    private Stacker _stacker;
    private RagDollControl _ragDollControl;

    public PlayerBehaviorBuild(Player player, PlayerMovable playerMoveble, Stacker stacker, RagDollControl ragDollControl)
    {
        _player = player;
        _playerMoveble = playerMoveble;
        _stacker = stacker;
        _ragDollControl = ragDollControl;
    }

    public void Enter()
    {
        _playerMoveble.moveXEnabled = false;

        if (_stacker.FindCurrentBlockHight() == 0)
        {
            Player.Instance.SetBehaviorWalking();
            Debug.Log("FindCurrentBlockHight() == 0");
        }
        else
        {
            _ragDollControl.SetIdleAnimation();
            _stacker.BuildStair();
            Debug.Log("PlayerBehaviorBuild");
        }
      
    }

    public void Exit()
    {
        _playerMoveble.moveYEnabled = true;
        _playerMoveble.moveXEnabled = true;
    }

    public void Update()
    {
      

    }

    public void FixedUpdate()
    {
      

    }

}

