using UnityEngine;
using UnityEngine.UI;

public class GameMode : MonoBehaviour
{
    public Inventory playerInventory;
    public Inventory toolboxInventory;
    public Player player;
    public GameObject gameOverPanel;
    public Text result;

    public Item[] items;
    public float minEventTimer = 5f;
    public float maxEventTimer = 15f;
    float nextEventTimer = 0f;
    float timeStarted;
    BreakableComponent[] shipComponents;
    bool gameOver = false;
    bool shieldActive = true;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            nextEventTimer -= Time.deltaTime;
            if (nextEventTimer <= 0f)
            {
                GenerateRandomEvent();
                if (!shieldActive)
                    GenerateRandomEvent();
                nextEventTimer = Random.Range(minEventTimer, maxEventTimer);
            }
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

    public void GameOver()
    {
        gameOver = true;
        // Change player movement to 0 to prevent movement
        player.movementSpeed = 0f;
        gameOverPanel.SetActive(true);
        Debug.Log(timeStarted);
        int seconds = (int)((Time.time - timeStarted) % 60);
        result.text = $"You have survived for {seconds} seconds";
        Debug.Log("Game Over!!!");
    }
}
