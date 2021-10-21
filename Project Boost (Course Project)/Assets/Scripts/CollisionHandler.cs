using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    // PARAMETERS
    [SerializeField] float delayTime = 1f;

    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip successSFX;

    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;

    //CACHE
    AudioSource audioSource;

    // STATE
    bool isTransitioning = false;
    bool isCollisionDisabled;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        isCollisionDisabled = GetComponent<Debugging>().isCollisionDisabled;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || isCollisionDisabled)
            return;

        switch (collision.collider.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                NextLevelSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    public void NextLevelSequence()
    {
        isTransitioning = true;

        audioSource.Stop();
        audioSource.PlayOneShot(successSFX); // SFX for clearing level

        successParticles.Play(); // particle effects of success

        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", delayTime);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;

        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX); // SFX for crash

        crashParticles.Play(); // particle effects of explosion

        GetComponent<Movement>().enabled = false;
        Invoke("RestartLevel", delayTime);
    }

    void RestartLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Scene build index of current scene for restart
        SceneManager.LoadScene(currentSceneIndex); // Restarting level
    }

    void LoadNextLevel()
    {
        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextLevelIndex == SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(0); // 0 --> build Index of lvl 1
        }
        else
        {
            SceneManager.LoadScene(nextLevelIndex);
        }
    }
}
