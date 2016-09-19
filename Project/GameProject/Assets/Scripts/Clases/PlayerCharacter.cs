using UnityEngine;
public class PlayerCharacter : BaseCharacter {
	private PlayerHealth ph;
	// Update is called once per frame
	void Start(){
		ph = (PlayerHealth)gameObject.GetComponent("PlayerHealth");
	}

	void Update () {
		Messenger<int, int>.Broadcast("Player Health Update", ph.curHealth, ph.maxHealth);
		Messenger<int, int>.Broadcast("Player Mana Update", ph.curMana, ph.maxMana);
		Messenger<int, int>.Broadcast("Player Energy Update", ph.curEnergy, ph.maxEnergy);
		Debug.Log("Health - " + ph.maxHealth + ", Mana - " + ph.maxMana + ", Energy - " + ph.curEnergy);
	}
}
