using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButtonPresser : MonoBehaviour
{
    [SerializeField] KeyCode KeyCode;
    List<ButtonEvent> activeButtons = new List<ButtonEvent>();
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<ButtonEvent>(out ButtonEvent button))
        {
            activeButtons.Add(button);
        }
    }

    private void Update()
    {
        foreach(var button in activeButtons)
        {
            Debug.Log("Activate " + button.getButtonName() + " pressing " + button.getKeyCode());
            if (Input.GetKeyDown(button.getKeyCode()))
            {
                button.pressButton();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<ButtonEvent>(out ButtonEvent button))
        {
            activeButtons.Remove(button);
        }
    }

}
