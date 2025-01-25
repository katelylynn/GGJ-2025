using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private enum Phases { Utility, Shop, Combat };
    private Phases phase;

    public int[] inventory;

    private void Awake()
    {
        // if GameManager instance already exists, destroy this duplicate
        if (Instance != null) 
        {
            Destroy(gameObject);
            return;
        }

        // init values
        phase = Phases.Utility;
        inventory = new int[] {0, 0};

        // keep gameobject between scenes
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // subscribe to events
        EventManager.PhaseCompleted += LoadNextPhase;
    }

    private void Update()
    {
        // for devs
        if (Input.GetKey(KeyCode.N)) LoadNextPhase();
    }

    private void LoadNextPhase()
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
