using TMPro;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    public Inventory playerInventory;
    public Inventory toolboxInventory;
    public Player player;
    public GameObject playerHud;
    public GameObject gameOverPanel;
    public TMP_Text result;
    public AudioManager audioManager;
    public PauseMenu pauseMenu;

    public Item[] items;
    public float minEventTimer = 5f;
    public float maxEventTimer = 15f;
    float nextEventTimer = 0f;
    float timeStarted;
    BreakableComponent[] shipComponents;
    bool gameOver = false;
    bool shieldActive = true;

    public int hullMaxIntegrity = 5;
    int hullCurrentIntegrity;

    // Start is called before the first frame update
    void Start()
    {
        timeStarted = Time.time;

        shipComponents = FindObjectsOfType<BreakableComponent>();

        playerInventory.items.Clear();
        toolboxInventory.items.Clear();

        GenerateRandomEvent();

        nextEventTimer = Random.Range(minEventTimer, maxEventTimer);

        foreach (Item item in items)
        {
            toolboxInventory.Add(item);
        }

        if (audioManager)
            audioManager.Play("Background");

        hullCurrentIntegrity = hullMaxIntegrity;

        PauseMenu.isGamePaused = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (!gameOver && !PauseMenu.isGamePaused)
        {
            nextEventTimer -= Time.deltaTime;
            if (nextEventTimer <= 0f)
            {
                GenerateRandomEvent();
                if (!shieldActive)
                    GenerateRandomEvent();
                nextEventTimer = Random.Range(minEventTimer, maxEventTimer);
            }

            if (player.o2Current <= 0f)
                GameOver();
        }
    }

    void GenerateRandomEvent()
    {
        if (shipComponents != null && shipComponents.Length > 0)
        {
            int i = Random.Range(0, shipComponents.Length);
            if (shipComponents[i].IsActive())
                return;
            shipComponents[i].ActivateEvent();
        }
    }

    public void ToggleGravity(bool toggle)
    {
        player.ToggleGravity(toggle);
    }

    public void ToggleShield(bool toggle)
    {
        shieldActive = toggle;
    }

    public void ToggleO2(bool toggle)
    {
        player.ToggleO2(toggle);
    }

    public void SetO2Multiplier(int multiplier)
    {
        player.SetO2Multiplier(multiplier);
    }

    public void RefillO2()
    {
        player.RefillO2();
    }

    public string GetHullIntegrity()
    {
        return $"({hullCurrentIntegrity}/{hullMaxIntegrity}";
    }

    public string GetStatusReport()
    {
        string result = string.Empty;

        foreach (BreakableComponent b in shipComponents)
        {
            string status = b.IsActive() ? "Bad" : "OK";
            result += $"{b.componentName} - Status: {status}\n";
        }

        return result;
    }

    public void PauseGame()
    {
        pauseMenu.Pause();
    }

    public void GameOver()
    {
        if (audioManager)
            audioManager.Play("GameOver");
        gameOver = true;
        // Change player movementSpeed to 0 to prevent input when game is over
        player.movementSpeed = 0f;
        playerHud.SetActive(false);
        gameOverPanel.SetActive(true);
        Debug.Log(timeStarted);
        int seconds = (int)((Time.time - timeStarted) % 60);
        result.text = $"You have survived for {seconds} seconds";
    }
}
