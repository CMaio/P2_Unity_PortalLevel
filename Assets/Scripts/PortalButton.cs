using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PortalButton : MonoBehaviour
{
    public UnityEvent OpenEvent, CloseEvent;
    [SerializeField] bool oneTimeOpen = false;

    void OnTriggerEnter(Collider _Collider)
    {
        if (_Collider.tag == "Companion")
        {
            OpenEvent.Invoke();
        }
    }
    private void OnTriggerExit(Collider _Collider)
    {
        if (_Collider.tag == "Companion")
        {
            CloseEvent.Invoke();
        }

    }

    public void openDoor()
    {
        if (!oneTimeOpen)
        {
            OpenEvent.Invoke();
            oneTimeOpen = true;
        }
    }
}
