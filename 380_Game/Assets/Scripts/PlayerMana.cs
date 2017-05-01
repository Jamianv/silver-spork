using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMana : MonoBehaviour {
	[SerializeField]
	private Stat mana;

	public Stat Mana {
		get {
			return mana;
		}
		set {
			mana = value;
		}
	}

	// Use this for initialization
	private void Awake(){
		mana.Initialize ();
	}

	private void decreaseMana(int decrease){
		mana.CurrentVal -= decrease;
	}
	private void increaseMana(int increase){
		mana.CurrentVal += increase;
	}
}
