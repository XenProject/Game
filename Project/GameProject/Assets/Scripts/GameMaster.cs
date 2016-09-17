using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {
	public GameObject playerCharacter;

	// Use this for initialization
	void Start () {
		Instantiate(playerCharacter,new Vector3(53,10,30), Quaternion.identity);
	}

	// Update is called once per frame
	void Update () {

	}
}
