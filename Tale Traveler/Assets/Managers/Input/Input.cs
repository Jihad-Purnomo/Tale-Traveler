using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour
{
    public static PlayerInput PlayerInput { get; private set; }

    public static Vector2 Move { get; private set; }
    public static bool JumpPressed { get; private set; }
    public static bool JumpReleased { get; private set; }
    public static bool GrabPressed { get; private set; }
    public static bool GrabReleased { get; private set; }

    private InputAction _move;
    private InputAction _jump;
    private InputAction _grab;

    private void Awake()
    {
        PlayerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        _move = PlayerInput.actions["Move"];
        _jump = PlayerInput.actions["Jump"];
        _grab = PlayerInput.actions["Grab"];
    }

    private void Update()
    {
        Move = _move.ReadValue<Vector2>();

        JumpPressed = _jump.WasPressedThisFrame();
        JumpReleased = _jump.WasReleasedThisFrame();

        GrabPressed = _grab.WasPressedThisFrame();
        GrabReleased = _grab.WasReleasedThisFrame();
    }
}
