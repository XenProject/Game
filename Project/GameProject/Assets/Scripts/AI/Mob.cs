using UnityEngine;
using System.Collections;

public class Mob : BaseCharacter {

	private EnemyHealth eh;
	// Use this for initialization
	void Start () {
		eh = (EnemyHealth)gameObject.GetComponent("EnemyHealth");

		Name = "Test Mob";
	}

	// Update is called once per frame
	void Update () {

	}

	public void DisplayHealth(){
		Messenger<int, int>.Broadcast("Enemy Health Update", eh.curHealth, eh.maxHealth);
	}
}
