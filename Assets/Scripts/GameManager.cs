using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private enum Phases { Utility, Shop, Combat };
    private Phases phase;

    private void Awake()
    {
        // if GameManager instance already exists, destroy this duplicate
        if (Instance != null) 
        {
            Destroy(gameObject);
            return;
        }

        phase = Phases.Utility;

        // keep gameobject between scenes
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // subscribe to events
        EventManager.PhaseCompleted += LoadNextPhase;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.N)) LoadNextPhase();
    }

    public void LoadNextPhase()
    {
        if (phase == Phases.Utility) 
        {
            SceneManager.LoadScene("Shop");
            phase = Phases.Shop;
        }
        else if (phase == Phases.Shop)
        {
            SceneManager.LoadScene("Combat");
            phase = Phases.Combat;
        }
        else if (phase == Phases.Combat)
        {
            SceneManager.LoadScene("Utility");
            phase = Phases.Utility;
        }
    }
}
