using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
		public Transform target;
		public int moveSpeed = 1;
		public int rotationSpeed = 1;

		private Transform myTransform;
	// Use this for initialization
	void Awake(){
		myTransform = transform;
	}

	void Start () {
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		target = go.transform;
	}

	// Update is called once per frame
	void Update () {
		Debug.DrawLine(target.position, myTransform.position, Color.red);
		//Look Target
		myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
		//Move to target
		if(Vector3.Distance(myTransform.position, target.position) >= 1.0){
			myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
		}
	}
}
