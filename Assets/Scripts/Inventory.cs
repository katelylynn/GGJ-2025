using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private TextMeshProUGUI tmp;

    private void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        tmp.text = $"Resource A: {GameManager.Instance.inventory[0]}, Resource B: {GameManager.Instance.inventory[1]}";
    }
}