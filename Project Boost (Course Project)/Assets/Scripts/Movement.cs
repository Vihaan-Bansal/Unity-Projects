using UnityEngine;

public class Movement : MonoBehaviour
{

    // PARAMETERS
    [SerializeField] AudioClip engineThrustSFX;

    [SerializeField] float thrustSpeed = 1000f;
    [SerializeField] float rotationSpeed = 250f;

    [SerializeField] ParticleSystem mainBoosterParticles;
    [SerializeField] ParticleSystem rightBoosterParticles;
    [SerializeField] ParticleSystem leftBoosterParticles;

    // CACHE
    Rigidbody rb;
    AudioSource audioSource;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Thrust Input
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            // Audio Control (Stop Playing)
            audioSource.Stop(); // stop audio
            mainBoosterParticles.Stop(); // stop particles
        }

        // Rotation Input
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            leftBoosterParticles.Stop();
            rightBoosterParticles.Stop();
        }
    }


    void StartThrusting()
    {
        // Audio Control (Start Playing)
        if (!audioSource.isPlaying && !mainBoosterParticles.isPlaying)
        {
            audioSource.PlayOneShot(engineThrustSFX); // audio
            mainBoosterParticles.Play(); // particles
        }
        rb.AddRelativeForce(0, thrustSpeed * Time.deltaTime, 0);
    }

    void RotateLeft()
    {
        if (!rightBoosterParticles.isPlaying)
        {
            rightBoosterParticles.Play();
        }
        Rotate(rotationSpeed);
    }

    void RotateRight()
    {
        if (!leftBoosterParticles.isPlaying)
        {
            leftBoosterParticles.Play();
        }
        Rotate(-rotationSpeed);
    }

    void Rotate(float rotate)
    {
        rb.freezeRotation = true;
        transform.Rotate(0, 0, rotate * Time.deltaTime);
        rb.freezeRotation = false;
    }

}
