using Doozy.Engine.Progress;
using Gamebase.Systems.GlobalEvents;
using UnityEngine;

public class TestScore : MonoBehaviour
{
    [SerializeField] private float rewardMultiplier = 1.5f;
    [SerializeField] private Progressor _progressor;
    private Stacker _stacker;
    private void Awake()
    {
        _stacker = FindObjectOfType<Stacker>();
        GlobalEventsSystem.Instance.Subscribe(GlobalEventType.Victory, SetScore);
    }
    private void OnDestroy()
    {
        GlobalEventsSystem.Instance.Unsubscribe(GlobalEventType.Victory, SetScore);
    }

    private void SetScore()
    {
        float score = _stacker.BuildStacktackStairsList.Count * rewardMultiplier;
        _progressor.SetValue(score);
    }
}

