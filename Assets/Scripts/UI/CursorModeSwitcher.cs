using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class CursorModeSwitcher : MonoBehaviour
{
    [SerializeField] VirtualMouseInput virtualCursor; 

    void OnEnable()
    {
        InputSystem.onDeviceChange += OnDeviceChange;
        Refresh();
    }

    void OnDisable()
    {
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        if (device is Gamepad)
            Refresh();
    }

    void Refresh()
    {
        bool hasGamepad = Gamepad.all.Count > 0;
        virtualCursor.gameObject.SetActive(hasGamepad);   
        //Cursor.visible = !hasGamepad;          // curseur système visible sinon
    }
}