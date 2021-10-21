using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    //PARAMETERS
    [SerializeField] float reloadTime = 1f;

    // CACHE
    PlayerControls playerControls;

    void Start() {
        playerControls = GetComponent<PlayerControls>();
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log($"{gameObject.name} ** Triggered {other.gameObject.name}");
        playerControls.enabled = false;
        Invoke("ReloadScene", reloadTime);
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
