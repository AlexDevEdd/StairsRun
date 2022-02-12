using Gamebase.Miscellaneous;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField] private Player _player;
 
    private Dictionary<Type, IPlayerBehavior> _behaviorsMap;
    private IPlayerBehavior _behaviorCurrent;

    private Stacker _stacker;
    private PlayerMovable _playerMoveble;
    private RagDollControl _ragDollControl;

    public override void Initialize()
    {     
         _stacker = FindObjectOfType<Stacker>();
        _playerMoveble = GetComponent<PlayerMovable>();
        _ragDollControl = FindObjectOfType<RagDollControl>();
        InitBehaviors();
        SetBehaviorByDefault();
    }


    private void Update()
    {
        if (_behaviorCurrent != null)
            _behaviorCurrent.Update();
    }
    private void FixedUpdate()
    {
        if (_behaviorCurrent != null)
            _behaviorCurrent.FixedUpdate();
    }
    private void InitBehaviors()
    {
        _behaviorsMap = new Dictionary<Type, IPlayerBehavior>();
        
        _behaviorsMap[typeof(PlayerBehaviorWalking)] = new PlayerBehaviorWalking(_player, _playerMoveble, _ragDollControl);
        _behaviorsMap[typeof(PlayerBehaviorClimb)] = new PlayerBehaviorClimb(_player, _playerMoveble, _ragDollControl);
        _behaviorsMap[typeof(PlayerBehaviorIdle)] = new PlayerBehaviorIdle(_player, _playerMoveble,_ragDollControl);
        _behaviorsMap[typeof(PlayerBehaviorBuild)] = new PlayerBehaviorBuild(_player, _playerMoveble, _stacker, _ragDollControl);

    }
    private void SetBehavior(IPlayerBehavior newBehavior)
    {
        if (_behaviorCurrent != null)
            _behaviorCurrent.Exit();
        if (_behaviorCurrent == newBehavior)
            return;

        _behaviorCurrent = newBehavior;
        _behaviorCurrent.Enter();
    }
    private void SetBehaviorByDefault()
    {
        SetBehaviorIdle();      
    }
    private IPlayerBehavior GetBehavior<T>() where T : IPlayerBehavior
    {
        var type = typeof(T);
        return _behaviorsMap[type];
    }
    public void SetBehaviorIdle()
    {
        var behaviorIdle = GetBehavior<PlayerBehaviorIdle>();
        SetBehavior(behaviorIdle);
    }
    public void SetBehaviorWalking()
    {
        var behaviorWalking = GetBehavior<PlayerBehaviorWalking>();
        SetBehavior(behaviorWalking);
    }
    public void SetBehaviorClimb()
    {
        var behaviorClimb = GetBehavior<PlayerBehaviorClimb>();
        SetBehavior(behaviorClimb);
    }

    public void SetBehaviorBuild()
    {
        var behaviorBuild = GetBehavior<PlayerBehaviorBuild>();
        SetBehavior(behaviorBuild);
    }
}



