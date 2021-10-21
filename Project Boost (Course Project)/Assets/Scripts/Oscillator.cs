using UnityEngine;

public class Oscillator : MonoBehaviour
{

    const float tau = Mathf.PI * 2; // constant value of 6.283
    float cycles;

    // PARAMETERS
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;

    // CACHE
    Vector3 startingPosition;
    Vector3 offset;
    float rawSineWaves;
    float movementFactor;

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        if (period <= Mathf.Epsilon) // mathf.epsilon is the tiniest number
            return;

        cycles = Time.time / period; // continually growing over time

        rawSineWaves = Mathf.Sin(tau * cycles); // continually changing b/w -1 and 1

        movementFactor = (rawSineWaves + 1f) / 2f; // recalculated to go from 0 to 1

        offset = movementVector * movementFactor; // offset to move
        transform.position = startingPosition + offset; // moving the obstacle according to offset
    }
}
