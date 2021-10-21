using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    
    [Header ("MOVEMENT")]
    [SerializeField] float controlSpeed = 15f;
    [SerializeField] float xRange = 6f;
    [SerializeField] float yRange = 5f;

    [Header ("PITCH")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -10f;

    [Header ("YAW")]
    [SerializeField] float positionYawFactor = 3f;
    [SerializeField] float controlYawFactor = 8f;

    [Header ("ROLL")]
    [SerializeField] float controlRollFactor = -20f;

    [Header ("LASERS")]
    [SerializeField] GameObject[] lasers;

    float xThrow, yThrow;

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessTranslation()
    {
        // Getting Input
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        // Getting Raw position update
        // x axis
        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float xPos = transform.localPosition.x + xOffset;

        // y axis
        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float yPos = transform.localPosition.y + yOffset;

        // Clamped x and y position
        float clampedXPos = Mathf.Clamp(xPos, -xRange, xRange);
        float clampedYPos = Mathf.Clamp(yPos, -yRange, yRange);

        // Applying movement
        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessRotation()
    {
        // Changing pitch
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;

        // Changing yaw
        float yawDueToPosition = transform.localPosition.x * positionYawFactor;
        float yawDueToControlThrow = xThrow * controlYawFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = yawDueToPosition + yawDueToControlThrow;
        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessFiring()
    {
        if (Input.GetButton("Fire1"))
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
    }

    // Activationg lasers
    void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
}
