using UnityEngine;
using UnityEngine.UI;

public class UtilityTimer : MonoBehaviour
{
    private Slider slider;
    private float time = 0.0f;
    [SerializeField] private float timeLimit = 60.0f;

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = 1;
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > timeLimit) EventManager.TriggerTimerCompleted();
        slider.value = 1 - (time/timeLimit);
    }
}
