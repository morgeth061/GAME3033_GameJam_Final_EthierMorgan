using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject winMenu;

    public GameObject PickupGameObject;

    public GameObject[] SpawnPoints;

    public TextMeshProUGUI TimerText;
    public TextMeshProUGUI HighScoreText;
    public TextMeshProUGUI PickupsText;

    public bool pauseVisible = false;

    public float timeRemaining;

    private int pickups;

    public bool gameStarted = false;

    void Start()
    {
        Time.timeScale = 1;
        pickups = 0;
        timeRemaining = 10;
        NewLocation();

        HighScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

    void Update()
    {
        if (gameStarted)
        {
            timeRemaining -= Time.deltaTime;

            if ((int)Mathf.Round(timeRemaining) >= 10)
            {
                TimerText.text = "00:" + (int)Mathf.Round(timeRemaining);
            }
            else
            {
                TimerText.text = "00:0" + (int)Mathf.Round(timeRemaining);
            }
        }

        if (timeRemaining <= 0)
        {
            GameWinCheck();
        }
    }

    private void GameWinCheck()
    {

        gameStarted = false;


        GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject.GetComponent<Animator>()
            .updateMode = AnimatorUpdateMode.UnscaledTime;

        //Time.timeScale = 0;
        if (pickups > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", pickups);
            GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject.GetComponent<Animator>().SetTrigger("WinAnim");
            
        }
        else
        {
            GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject.GetComponent<Animator>().SetTrigger("LoseAnim");
        }
    }

    public void ShowWinUI()
    {
        winMenu.transform.Find("WinText").gameObject.GetComponent<TextMeshProUGUI>().text = "You won!";
        winMenu.SetActive(true);
    }

    public void ShowLoseUI()
    {
        winMenu.transform.Find("WinText").gameObject.GetComponent<TextMeshProUGUI>().text = "You lost...";
        winMenu.SetActive(true);
    }

    public void ShowEndUI(bool didWin)
    {
        if (didWin)
        {
            winMenu.transform.Find("WinText").gameObject.GetComponent<TextMeshProUGUI>().text = "You won!";
        }
        else
        {
            winMenu.transform.Find("WinText").gameObject.GetComponent<TextMeshProUGUI>().text = "You lost...";
        }
        winMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Pause()
    {
        if (gameStarted)
        {
            if (!pauseVisible)
            {
                pauseVisible = true;
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                pauseVisible = false;
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        
    }

    public void PickupCollected()
    {
        NewLocation();
        ResetStats();
    }

    private void NewLocation()
    {
        float randomFloat = Random.Range(0, SpawnPoints.Length);

        PickupGameObject.transform.position = SpawnPoints[(int)randomFloat].transform.position;
    }

    private void ResetStats()
    {
        pickups++;
        timeRemaining = 10;

        PickupsText.text = pickups.ToString();
    }

    public void OnPauseClick()
    {
        Pause();
    }

    public void OnMainMenuClick()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
