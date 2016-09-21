using UnityEngine;
using System.Collections.Generic;
public class PlayerCharacter : BaseCharacter {
	private PlayerHealth ph;
	
	private static List<Item> _inventory = new List<Item>();
	public static List<Item> Inventory{
		get{ return _inventory; }
	}

	// Update is called once per frame
	void Start(){
		ph = (PlayerHealth)gameObject.GetComponent("PlayerHealth");
	}

	void Update () {
		if(ph!=null){
			Messenger<int, int>.Broadcast("Player Health Update", ph.curHealth, ph.maxHealth);
			Messenger<int, int>.Broadcast("Player Mana Update", ph.curMana, ph.maxMana);
			Messenger<int, int>.Broadcast("Player Energy Update", ph.curEnergy, ph.maxEnergy);
			Debug.Log("Health - " + ph.maxHealth + ", Mana - " + ph.maxMana + ", Energy - " + ph.curEnergy);
		}
	}
}
