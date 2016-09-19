using UnityEngine;
using System.Collections;

public class PlayerHealth : PlayerCharacter {
	public int maxHealth;
	public int curHealth;

	public int maxMana;
	public int curMana;
	
	public int maxEnergy;
	public int curEnergy;

	//public float healthBarLength;
	// Use this for initialization
	void Start () {
		//healthBarLength = Screen.width / 4;
		maxHealth = GetVital((int)VitalName.Health).CurValue;
		curHealth = maxHealth;

		maxMana = GetVital((int)VitalName.Mana).CurValue;
		curMana = maxMana;

		maxEnergy = GetVital((int)VitalName.Energy).CurValue;
		curEnergy = maxEnergy;
	}

	// Update is called once per frame
	void Update () {
		AddjustCurrHealth(0);
	}

	/*void OnGUI(){
		GUI.Box(new Rect(10, 10, healthBarLength, 20), curHealth + "/" + maxHealth);
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
