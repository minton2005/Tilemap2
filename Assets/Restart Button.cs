using UnityEngine;

public class RestartButton : MonoBehaviour
{
    public void OnRestartButtonClick()
    {
        GameManager.instance.RestartGame();
    }
}
