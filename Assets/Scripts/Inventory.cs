using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    private TextMeshProUGUI tmp;

    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        tmp.text = $"Resource A: {GameManager.Instance.inventory[0]}, Resource B: {GameManager.Instance.inventory[1]}";
    }
}
