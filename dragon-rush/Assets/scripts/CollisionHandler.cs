using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip failure;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem failureParticles;
    [SerializeField] float levelLoadDelay = 2f;

    AudioSource AudioSource;

    bool isControllable = true;
    void OnCollisionEnter(Collision other)
    {
        if (!isControllable) return;

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
        isControllable = false;
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void StartCrashSequence()
    {
        //add sounds and VFX
        isControllable = false;
        GetComponent<Movement>().enabled = false;
        Invoke("RestartLevel", levelLoadDelay);
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
