using UnityEngine;
using System.Collections;

public class PlayerHealth : PlayerCharacter {
	public int maxHealth;
	public int curHealth;

	public int maxMana;
	public int curMana;

	public int maxEnergy;
	public int curEnergy;

	public int maxExp;
	public int curExp;

	//public float healthBarLength;
	// Use this for initialization
	void Start () {
		//healthBarLength = Screen.width / 4;
		maxHealth = GetVital((int)VitalName.Health).AdjustedBaseValue;
		curHealth = maxHealth;

		maxMana = GetVital((int)VitalName.Mana).AdjustedBaseValue;
		curMana = maxMana;

		maxEnergy = GetVital((int)VitalName.Energy).AdjustedBaseValue;
		curEnergy = maxEnergy;

		maxExp = MaxExp;
		curExp = FreeExp;
	}

	// Update is called once per frame
	void Update () {
		AddjustCurrHealth(0);
		if(Input.GetKeyDown(KeyCode.F)){
			AddExp(5);
			maxExp = MaxExp;
			curExp = FreeExp;
		}
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

	public void UpdateVitalBar(){
		maxHealth = GetVital((int)VitalName.Health).AdjustedBaseValue;
		maxMana = GetVital((int)VitalName.Mana).AdjustedBaseValue;
		maxEnergy = GetVital((int)VitalName.Energy).AdjustedBaseValue;
	}

}
