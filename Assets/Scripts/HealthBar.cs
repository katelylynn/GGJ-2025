using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = 1;

        Player.DamageTaken += DecrementHealth;
        Player.PlayerDied += () => { slider.value = 0; };
    }

    private void DecrementHealth()
    {
        slider.value -= Player.damageAmount * Time.deltaTime;
    }
}
