using Cysharp.Threading.Tasks;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Stacker : ObjectSpawner
{
    private const string TAG_BLOCK = "Block";

    [SerializeField] private Transform _stairPrefab;
    [SerializeField] private Transform _currentStairParent;
    [SerializeField] private Transform _currentStair;
    [SerializeField] private float _stackStepY = 1f;
    [SerializeField] private float _stackStepZ = 0.2f;
    [SerializeField] private GameObject _player;

    private Transform _newStair;
    private float _stairHight;
    private ColliderChecker _colliderChecker;
    private List<StackStair> _stackStairsList = new List<StackStair>();
    private List<StackStair> _buildStacktackStairsList = new List<StackStair>();

    public List<StackStair> StackStairsList => _stackStairsList;
    public List<StackStair> BuildStacktackStairsList => _buildStacktackStairsList;

    public event Action OnPlayerClimb;

    private void Awake()
    {
        _stackStairsList.Add(_currentStairParent.GetComponentInChildren<StackStair>());
        _colliderChecker = FindObjectOfType<ColliderChecker>();
        _colliderChecker.OnStackStair += AddStair;
        _colliderChecker.OnUnStackStair += RemoveStair;
        _stairHight = _stairPrefab.gameObject.GetComponentInChildren<Renderer>().bounds.size.y;

    }

    private void AddStair()
    {
        if (_stackStairsList.Count == 0)
        {
            SpawnStair();
            _currentStair.SetParent(_currentStairParent, true);
            _stackStairsList.Add(_currentStair.GetComponent<StackStair>()); ;
        }

        float stepY = _stackStepY * _stackStairsList.Count;
        float stepZ = _stackStepZ * _stackStairsList.Count;
        SpawnStair();
        _currentStair.transform.position = _currentStairParent.position + new Vector3(0, stepY, stepZ);
        _currentStair.SetParent(_currentStairParent, true);
        _stackStairsList.Add(_currentStair.GetComponent<StackStair>()); ;

    }

    private void SpawnStair() => _currentStair = SpawnObject(_stairPrefab.gameObject.transform, _currentStairParent);

    private void RemoveStair()
    {
        if (_stackStairsList.Count == 1 && _colliderChecker.IsFinish == true)
            DestroyStair();

        else if (_stackStairsList.Count >= 1 && _colliderChecker.IsCanBuild == true)
            DestroyStair();

        else if (_stackStairsList.Count > 1 && _colliderChecker.IsCanBuild == false)
            DestroyStair();

        else if (_stackStairsList.Count == 1) return;

        else return;
    }

    private void DestroyStair()
    {
        var currentRemovedStair = _stackStairsList[_stackStairsList.Count - 1];
        currentRemovedStair.gameObject.SetActive(false);
        _stackStairsList.Remove(currentRemovedStair);
        Destroy(currentRemovedStair.gameObject);
    }

    private void AddNewStair()
    {
        var block = GetCurrentBlock();
        if (block == null) return;

        var blockStack = block.GetComponentInChildren<BlockStairStack>();
        var newStackPosition = blockStack.gameObject.transform;
        newStackPosition.position = new Vector3(_player.transform.position.x, newStackPosition.position.y, newStackPosition.position.z);
        float stepY = 0.5f * _buildStacktackStairsList.Count;
        _newStair = SpawnObject(_stairPrefab.gameObject.transform, blockStack.transform);
        _newStair.position = newStackPosition.transform.position + new Vector3(0, stepY, 0);
        _newStair.SetParent(blockStack.transform, true);
        _buildStacktackStairsList.Add(_newStair.GetComponent<StackStair>());
    }

    public async void BuildStair()
    {
        var hight = FindCurrentBlockHight();
        if (hight == 0)
        {
            Player.Instance.SetBehaviorWalking();
            return;
        }
        var stairsCountList = _stackStairsList.Count;
        var needStairs = (float)Math.Ceiling(hight / _stairHight);

        if (_newStair == null || _newStair != null)
        {
            if (stairsCountList < needStairs)
            {
                for (int i = 1; i <= needStairs; i++)
                {
                    if (i >= _stackStairsList.Count && _stackStairsList.Count == 1 && _colliderChecker.IsFinish)
                    {
                        BuildLastStair();

                        Player.Instance.SetBehaviorClimb();
                        return;
                    }

                    if (i > _stackStairsList.Count && _stackStairsList.Count == 1)
                    {
                        _colliderChecker.IsLose = true;
                        _colliderChecker.IsCanBuild = true;
                        BuildLastStair();

                        Player.Instance.SetBehaviorClimb();
                        return;
                    }
                    await Build();
                }
            }
            else
            {
                for (int i = 1; i <= needStairs; i++)
                {
                    await Build();
                }
            }

        }
        Player.Instance.SetBehaviorClimb();
        _buildStacktackStairsList.Clear();
    }

    private void BuildLastStair()
    {
        AddNewStair();
        EnableLastIndexCollider();
        RemoveStair();
    }

    private void EnableLastIndexCollider()
    {
        var lastStair = _buildStacktackStairsList.Last().GetComponent<StackStair>();
        lastStair.GetComponentInChildren<BoxCollider>().enabled = true;
    }

    private async Task Build()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.1f), ignoreTimeScale: false);
        AddNewStair();
        RemoveStair();
    }

    public float FindCurrentBlockHight()
    {

        var currentBlock = GetCurrentBlock();
        if (currentBlock == null)
        {
            if (_colliderChecker.IsFinish) return currentBlock.GetBlockHight(); ;
            return 0f;
        }

        return currentBlock.GetBlockHight();
    }

    private Block GetCurrentBlock()
    {
        Block block = null;
        RaycastHit[] hits;

        hits = Physics.RaycastAll(_currentStairParent.transform.position, _currentStairParent.transform.forward, 5f);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.CompareTag(TAG_BLOCK))
            {
                Debug.Log(hits[i].collider.name);
                block = hits[i].collider.GetComponent<Block>();
                return block;
            }
        }

        return block;
    }
}