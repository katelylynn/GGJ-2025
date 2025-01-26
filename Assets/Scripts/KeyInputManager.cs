using UnityEngine;
using System;
using System.Collections.Generic;

public class KeyInputManager : MonoBehaviour
{
    public static KeyInputManager Instance { get; private set; }

    private List<KeyCode> keyCodes = new List<KeyCode>();
    public event Action<KeyCode> OnKeyDown;
    public event Action<KeyCode> OnKeyUp;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
        {
            keyCodes.Add(code);
        }
    }

    private void Update()
    {
        foreach (KeyCode keyCode in keyCodes)
        {
            if (Input.GetKeyDown(keyCode))
            {
                OnKeyDown?.Invoke(keyCode);
            }
            if (Input.GetKeyUp(keyCode))
            {
                OnKeyUp?.Invoke(keyCode);
            }
        }
    }
}
