using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("Caverns lvl 1");
    }
}
