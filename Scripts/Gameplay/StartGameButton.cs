using UnityEngine;

public class StartGameButton : MonoBehaviour
{
    public void StartGame()
    {
        Player.Instance.SetBehaviorWalking();
        this.gameObject.SetActive(false);
    }


}

