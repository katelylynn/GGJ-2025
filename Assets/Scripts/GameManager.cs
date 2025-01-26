using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

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
        inventory = new int[] {0, 0};

        // keep gameobject between scenes
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // subscribe to events
        EndPhaseButton.PhaseButtonClicked += LoadNextPhase;
        UtilityTimer.TimerCompleted += LoadNextPhase;
        Projectile.KilledEnemy += () => { IncrementInventory(new int[] { 0, 1 }); };
        Player.PlayerDied += () => { StartCoroutine(LoadPhaseDelay(1f)); };
        EnemySpawner.EnemiesDefeated += DeclareGameWin;
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

    private void IncrementInventory(int[] amount)
    {
        inventory[0] += amount[0];
        inventory[1] += amount[1];
    }

    private IEnumerator LoadPhaseDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("called");
        LoadNextPhase();
    }

    private void DeclareGameWin()
    {
        Debug.Log("You win yay!");
    }
}
