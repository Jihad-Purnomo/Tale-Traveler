using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour
{
    public static PlayerInput PlayerInput { get; private set; }
    public static EventSystem eventSystem { get; private set; }

    public static Vector2 Move { get; private set; }
    public static bool JumpPressed { get; private set; }
    public static bool JumpReleased { get; private set; }
    public static bool GrabPressed { get; private set; }
    public static bool GrabReleased { get; private set; }
    public static bool SpellPressed { get; private set; }
    public static bool SpellReleased { get; private set; }
    public static bool ChangeSpell { get; private set; }

    public static bool MenuSubmit { get; private set; }
    public static bool MenuCancel { get; private set; }

    private InputAction _move;
    public static InputAction jumpAction { get; private set; }
    private InputAction _grab;
    private InputAction _spell;
    private InputAction _changeSpell;

    private InputAction _submit;
    private InputAction _cancel;

    private void Awake()
    {
        PlayerInput = GetComponent<PlayerInput>();
        eventSystem = GetComponent<EventSystem>();
    }

    private void Start()
    {
        _move = PlayerInput.actions["Move"];
        jumpAction = PlayerInput.actions["Jump"];
        _grab = PlayerInput.actions["Grab"];
        _spell = PlayerInput.actions["Spell"];
        _changeSpell = PlayerInput.actions["ChangeSpell"];

        _submit = PlayerInput.actions["Submit"];
        _cancel = PlayerInput.actions["Cancel"];
    }

    private void Update()
    {
        Move = _move.ReadValue<Vector2>();

        JumpPressed = jumpAction.WasPressedThisFrame();
        JumpReleased = jumpAction.WasReleasedThisFrame();

        GrabPressed = _grab.WasPressedThisFrame();
        GrabReleased = _grab.WasReleasedThisFrame();

        SpellPressed = _spell.WasPressedThisFrame();
        SpellReleased = _spell.WasReleasedThisFrame();

        ChangeSpell = _changeSpell.WasPressedThisFrame();

        MenuSubmit = _submit.WasPressedThisFrame();
        MenuCancel = _cancel.WasPressedThisFrame();
    }

    public static void DisableAction(InputAction action)
    {
        action.Disable();
    }

    public static void EnableAction(InputAction action)
    {
        action.Enable();
    }

    public static void ChangeActionMap(string mapName)
    {
        PlayerInput.SwitchCurrentActionMap(mapName);
    }

    public static void SelectUI(GameObject button)
    {
        eventSystem.SetSelectedGameObject(button);
    }
}
