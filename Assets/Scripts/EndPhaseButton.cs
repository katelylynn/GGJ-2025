using System;
using UnityEngine;
using UnityEngine.UI;

public class EndPhaseButton : MonoBehaviour
{
    private Button button;
    public static event Action PhaseButtonClicked;

    void Start()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(PhaseButtonClicked.Invoke);
    }
}
