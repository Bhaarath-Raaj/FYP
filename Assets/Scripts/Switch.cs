using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public bool isOn = true;
    public DoorHandler doorController;
    public DoorHandler doorController2; 

    void Start()
    {
        doorController.ToggleDoor(isOn);
        doorController2.ToggleDoor(!isOn);
    }

    public void ToggleSwitch()
    {
        isOn = !isOn;
        Debug.Log("Switch is " + (isOn ? "ON" : "OFF"));
        doorController.ToggleDoor(isOn);
        doorController2.ToggleDoor(!isOn);
    }
}