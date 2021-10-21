using UnityEngine;

public class Debugging : MonoBehaviour
{
    // CACHE
    CollisionHandler collisionHandler;

    // STATE
    public bool isCollisionDisabled = false;

    void Start()
    {
        collisionHandler = GetComponent<CollisionHandler>();
    }

    void Update()
    {
        DisableCollision();
        LoadNextLevel();
    }

    void DisableCollision()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCollisionDisabled = !isCollisionDisabled; // if true --> false, if false --> true
        }
    }

    void LoadNextLevel()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            collisionHandler.NextLevelSequence();
        }
    }
}
