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
        tmp.text =
            $"Bubbles Blown: {GameManager.Instance.inventory[0]}, Staples Collected: {GameManager.Instance.inventory[1]}";
    }
}