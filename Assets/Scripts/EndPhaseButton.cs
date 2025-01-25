using UnityEngine;
using UnityEngine.UI;

public class EndPhaseButton : MonoBehaviour
{
    private Button button;

    void Start()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(EventManager.TriggerPhaseCompleted);
    }
}
