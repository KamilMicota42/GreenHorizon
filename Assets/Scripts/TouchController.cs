using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchController : MonoBehaviour
{
    public FixedButton _JumpButton;
    public FixedButton _DashButton;

    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        Debug.Log("Jump button pressed: " + _JumpButton.pressed);
        Debug.Log("Dash button pressed: " + _DashButton.pressed);
        playerMovement.HandleJumpInput(_JumpButton.pressed);
        playerMovement.HandleDashInput(_DashButton.pressed);
    }
}
