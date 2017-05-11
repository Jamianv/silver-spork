using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public GameObject PauseUI;
    public GameObject LeaderboardUI;
	public GameObject OptionsUI;


    private GameObject timerObject;
    public Timer timerScript;
    private string[] timeTable = new string[5];
    public Text txt;


    private bool paused = false;

    void Start()
    {
        PauseUI.SetActive(false);
        LeaderboardUI.SetActive(false);
		OptionsUI.SetActive (false);
        timerObject = GameObject.FindGameObjectWithTag("Timer");
        timerScript = timerObject.GetComponent<Timer>();

        Debug.Log(txt != null);

    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            paused = !paused;
            if (paused == true)
            {
                PauseUI.SetActive(true);
            }
        }

        if (paused)
        {
            Time.timeScale = 0;
        }

        if (!paused)
        {
            PauseUI.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void Resume()
    {
        paused = false;
    }

    public void Restart()
    {
        string current = Application.loadedLevelName;
        SceneManager.LoadScene(current);
        timerScript.currentTime = 0;
    }

    public void MainMenu()
    {
		SceneManager.LoadScene ("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Options()
    {
		PauseUI.SetActive (false);
		OptionsUI.SetActive (true);
    }

    public void Leaderboard()
    {
        PauseUI.SetActive(false);
        LeaderboardUI.SetActive(true);

        for (int x = 0; x < 5; x++)
        {
            int bestMinute = Mathf.FloorToInt(timerScript.bestTimes[x] / 60F);
            int bestSecond = Mathf.FloorToInt(timerScript.bestTimes[x] - bestMinute * 60);
            timeTable[x] = string.Format("{0:0}:{1:00}", bestMinute, bestSecond);
        }
        txt.text = "Leaderboard\n\n1. " + timeTable[0] + "\n\n2. " + timeTable[1] + "\n\n3. " + timeTable[2] + "\n\n4. " + timeTable[3] + "\n\n5. " + timeTable[4];

    }

    public void Return()
    {
        LeaderboardUI.SetActive(false);
        PauseUI.SetActive(true);
    }
	public void ReturnFromOptions()
	{
		OptionsUI.SetActive (false);
		PauseUI.SetActive (true);
	}
}
