using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip failure;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem failureParticles;

    AudioSource AudioSource;

    bool isControllable = true;
    void OnCollisionEnter(Collision other)
    {
        switch(other.gameObject.tag)
        {
            case "Finish":
                StartSuccessSequence();
                //Debug.Log("Its Safe");
                break;
            case "Unfriendly":
                StartCrashSequence();
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

    void StartSuccessSequence()
    {
        //add sound and visual effects 
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", 2f);
    }

    void StartCrashSequence()
    {
        //add sounds and VFX
        GetComponent<Movement>().enabled = false;
        Invoke("RestartLevel", 2f);
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
    private void RestartLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
