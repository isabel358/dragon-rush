using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        switch(other.gameObject.tag)
        {
            case "Finish":
                LoadNextLevel();
                //Debug.Log("Its Safe");
                    break;
            case "Unfriendly":
                RestartLevel();
                //Debug.Log("Its Dangerous");
                break;
            case "Collectable":
                Debug.Log("Its so shiny");
                break;
            default:
                Debug.Log("Something happens");
                break;
        }
    }
    void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }
    private static void RestartLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
