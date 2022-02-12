using Doozy.Engine.Progress;
using Gamebase.Systems.GlobalEvents;
using Gamebase.Systems.Resources;
using UnityEngine;

namespace Assets.Scripts.UIControllers
{
    public class UIScoreController : MonoBehaviour
    {

        [SerializeField] private float rewardMultiplier = 1.8f;
        [SerializeField] private Progressor _progressor;
        [SerializeField] private float _poinstForOneStair = 1000f;

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
            float score = (_stacker.BuildStacktackStairsList.Count * _poinstForOneStair) * rewardMultiplier;
            _progressor.SetValue(score);
           // ResourcesSystem.Instance.AddResource<Coins>((int)score);
        }
    }
}

