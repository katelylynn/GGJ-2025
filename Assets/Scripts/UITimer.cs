using UnityEngine;
using UnityEngine.UI;

public class UITimer : MonoBehaviour
{
    public float time;
    public float Max;
    public Image fill;


    private void Update()
    {
        time -= Time.deltaTime;
        fill.fillAmount = time / Max;
        if (time < -(Max / 2)) Destroy(gameObject);
    }
}