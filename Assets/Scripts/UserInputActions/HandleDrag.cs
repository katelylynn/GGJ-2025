using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleDrag : MonoBehaviour
{
    [SerializeField] private KeyInputManager keyInputManager;
    [SerializeField] private KeyCode PickUpKey;

    public bool isDragging;

    void Start()
    {
        isDragging = false;
        keyInputManager.OnKeyDown += HandleOnKeyDown;
        keyInputManager.OnKeyUp += HandleOnKeyUp;
    }

    private void HandleOnKeyDown(KeyCode keyCode)
    {
        if (keyCode == PickUpKey)
        {
            Debug.Log(this + ": Started dragging");
            isDragging = true;
        }
    }

    private void HandleOnKeyUp(KeyCode keyCode)
    {
        if (keyCode == PickUpKey)
        {
            Debug.Log(this + ": Stopped dragging");
            isDragging = false;
        }
    }
}
