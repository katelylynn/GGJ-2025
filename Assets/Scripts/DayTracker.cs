using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DayTracker : MonoBehaviour
{
    private TextMeshProUGUI tmp;

    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        tmp.text = $"{SceneManager.GetActiveScene().name} Phase Day {GameManager.days}";
    }
}
