﻿using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
	public int maxHealth = 70;
	public int curHealth = 70;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		AddjustCurrHealth(0);
	}

	/*void OnGUI(){
		GUI.Box(new Rect(10, 40, healthBarLength, 20), curHealth + "/" + maxHealth);
	}*/

	public void AddjustCurrHealth(int adj){
		curHealth += adj;

		if(curHealth < 0){
			curHealth = 0;
		}

		if(curHealth > maxHealth){
			curHealth = maxHealth;
		}
		//healthBarLength = (Screen.width / 4) * (curHealth / (float)maxHealth);
	}

}
