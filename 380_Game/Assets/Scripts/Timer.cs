using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {
    static bool record;
    static float currentTime;
    static float bestTime;
    private GUIStyle guiStyle = new GUIStyle();

    public
	// Use this for initialization
	void Start () {
        //Reset current time
        currentTime = 0.0F;

        StartRecord();
        OnGUI();
	}
	
	// Update is called once per frame
	void Update (){
        if (record)
        {
            currentTime += 1 * Time.deltaTime;
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
        record = false;
        if (checkBestTime && bestTime == 0)
        {
            bestTime = currentTime;
        }
        else if (checkBestTime && currentTime < bestTime)
            bestTime = currentTime;
    }

    private void OnGUI()
    {
        guiStyle.fontSize = 20;
        guiStyle.normal.textColor = Color.white;
        int minutes = Mathf.FloorToInt(currentTime / 60F);
        int seconds = Mathf.FloorToInt(currentTime - minutes * 60);
        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
        GUI.Label(new Rect(Screen.width / 2, Screen.height / 15 , 100, 50), niceTime, guiStyle);
    }
}
