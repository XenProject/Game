using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {
	public GameObject target;
	public float attackTimer;
	public float coolDown;
	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectWithTag("Player");
		attackTimer = 0;
		coolDown = 2.0f;
	}

	// Update is called once per frame
	void Update () {
		if(attackTimer > 0){
			attackTimer -= Time.deltaTime;
		}
		if(attackTimer < 0){
			attackTimer = 0;
		}

		if(attackTimer == 0){
			Attack(-15);
		}

	}

	private void Attack(int dmg){
		//Обзор
		Vector3 dir = (target.transform.position - transform.position).normalized;
		//Скалярное произведение векторов
		float direction = Vector3.Dot(dir, transform.forward);

		if(Vector3.Distance(target.transform.position, transform.position) <= 1.0f){
			if(direction > 0){
				PlayerHealth ph = (PlayerHealth)target.GetComponent("PlayerHealth");
				ph.AddjustCurrHealth(dmg);
				attackTimer = coolDown;
			}
		}
	}

}
