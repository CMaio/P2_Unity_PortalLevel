using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonEvent : MonoBehaviour
{
    [SerializeField] bool onceTrigger;
    private bool isActive = true;
    [SerializeField] UnityEvent buttonEvent;
    [SerializeField] KeyCode buttonKeyCode;
    [SerializeField] string buttonName;
    public bool pressButton()
    {
        bool canBeCalled = isActive;
        if (canBeCalled)
        {
            buttonEvent.Invoke();
        }
        if (onceTrigger) { isActive = false; }
        return canBeCalled;
    } 
    

    public KeyCode getKeyCode()
    {
        return buttonKeyCode;
    }
    
    public string getButtonName()
    {
        return buttonName;
    }
    
    
    
   
}
