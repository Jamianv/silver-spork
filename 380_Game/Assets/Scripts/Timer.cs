using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    static bool record;
    public float currentTime;
    public float[] bestTimes = new float[5];
    private float[] temp = new float[5];
    private GUIStyle guiStyle = new GUIStyle();

    // Use this for initialization
    void Start()
    {
        //Reset current time
        currentTime = 0.0F;
        StartRecord();
        OnGUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (record)
        {
            currentTime += 1 * Time.deltaTime;
        }

        if (Input.GetButtonDown("Record"))
        {
            StopRecord(true);
        }
    }

    public void Awake()
    {
        DontDestroyOnLoad(this);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }

    void StartRecord()
    {
        record = true;
    }

    //Call StopRecord(false) to pause record
    //call StopRecord(true) to end record and check if its the best time

    void StopRecord(bool checkBestTime)
    {
        if (checkBestTime == false)
        {
            record = false;
        }

        for (int x = 0; x < 5; x++)
        {
            if (currentTime < bestTimes[x] || bestTimes[x] == 0)
            {
                bestTimes[x] = currentTime;
                x = 5;
            }
        }
    }

    private void OnGUI()
    {
        guiStyle.fontSize = 20;
        guiStyle.normal.textColor = Color.white;

        int minutes = Mathf.FloorToInt(currentTime / 60F);
        int seconds = Mathf.FloorToInt(currentTime - minutes * 60);

        int bestMinute = Mathf.FloorToInt(bestTimes[0] / 60F);
        int bestSecond = Mathf.FloorToInt(bestTimes[0] - bestMinute * 60);

        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

        string bestTime = string.Format("{0:0}:{1:00}", bestMinute, bestSecond);

        GUI.Label(new Rect(Screen.width / 2, Screen.height / 15, 100, 50), niceTime,
            guiStyle);
    }
}
