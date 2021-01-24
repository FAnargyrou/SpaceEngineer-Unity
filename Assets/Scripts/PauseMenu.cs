using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    GameMode gameMode;
    public TMP_Text hullIntegrity;
    public TMP_Text report;
    // Start is called before the first frame update
    void Start()
    {
        gameMode = FindObjectOfType<GameMode>();
        gameObject.SetActive(false);
    }

    public void Pause()
    {
        if (isGamePaused)
            ResumeGame();
        else
            PauseGame();

        if (gameMode && gameMode.audioManager)
        {
            gameMode.audioManager.Play("PauseSound");
        }
    }

    void PauseGame()
    {
        gameObject.SetActive(true);
        isGamePaused = true;
        hullIntegrity.text = gameMode.GetHullIntegrity();
        report.text = gameMode.GetStatusReport();
    }
    void ResumeGame()
    {
        gameObject.SetActive(false);
        isGamePaused = false;
    }
}
