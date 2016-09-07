using UnityEngine;
using System.Collections;

public class PlayerCharacter : MonoBehaviour {
	private PlayerHealth ph;
	// Update is called once per frame
	void Start(){
		ph = (PlayerHealth)gameObject.GetComponent("PlayerHealth");
	}

	void Update () {
		Messenger<int, int>.Broadcast("Player Health Update", ph.curHealth, ph.maxHealth);
	}
}
