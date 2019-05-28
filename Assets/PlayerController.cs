using EasySurvivalScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerCamera playerCamera;
    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GetComponentInChildren<PlayerCamera>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public bool EnableMovement {
        get {
            return playerMovement.enabled;
        }
        set {
            playerMovement.enabled = value;
            playerCamera.enabled = value;
        }
    }
}
