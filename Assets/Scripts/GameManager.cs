using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static int days; // how many full cycles of the three phases have gone by
    public int phase; // 0: utility, 1: shop, 2: combat

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
        days = 1;
        phase = SceneManager.GetActiveScene().buildIndex;
        inventory = new[] { 0, 0 };

        // keep gameobject between scenes
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // subscribe to events
        EndPhaseButton.PhaseButtonClicked += LoadNextPhase;
        UtilityTimer.TimerCompleted += LoadNextPhase;
    }

    private void Update()
    {
        // for devs
        if (Input.GetKey(KeyCode.N)) LoadNextPhase();
    }

    private void LoadNextPhase()
    {
        if (phase == 0)
        {
            var buttency = ScoreManager.GetInstance().score;
            inventory[0] += buttency;
            ScoreManager.GetInstance().score = 0;
            SceneManager.LoadScene("Shop");
            phase = 1;
        }
        else if (phase == 1)
        {
            SceneManager.LoadScene("Combat");
            phase = 2;
        }
        else if (phase == 2)
        {
            SceneManager.LoadScene("Utility");
            phase = 0;
            days++;
        }
    }
}