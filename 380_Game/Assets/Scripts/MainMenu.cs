using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	public string selectLevel;
	public string selectOptions;

	public void NewGame()
	{
		Application.LoadLevel (selectLevel);

	}
	public void QuitGame()
	{
		Application.Quit ();
	}
	public void Options()
	{
		Application.LoadLevel (selectOptions);
	}
}
