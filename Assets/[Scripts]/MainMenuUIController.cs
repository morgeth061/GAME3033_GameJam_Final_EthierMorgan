using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIController : MonoBehaviour
{
    public GameObject MainCanvas;
    public GameObject InstructionsCanvas;
    public GameObject CreditsCanvas;
    
    void Start()
    {
        MainCanvas.SetActive(true);
        InstructionsCanvas.SetActive(false);
        CreditsCanvas.SetActive(false);
    }

    public void OnMainMenuClick()
    {
        MainCanvas.SetActive(true);
        InstructionsCanvas.SetActive(false);
        CreditsCanvas.SetActive(false);
    }

    public void OnPlayClick()
    {
        SceneManager.LoadScene("Level");
    }

    public void OnInstructionsClick()
    {
        MainCanvas.SetActive(false);
        InstructionsCanvas.SetActive(true);
        CreditsCanvas.SetActive(false);
    }
    public void OnCreditsClick()
    {
        MainCanvas.SetActive(false);
        InstructionsCanvas.SetActive(false);
        CreditsCanvas.SetActive(true);
    }

    public void OnQuitClick()
    {
        print("Quitting!");
        Application.Quit();
    }

    public void OnResetClick()
    {
        PlayerPrefs.SetInt("HighScore", 0);
    }
}
