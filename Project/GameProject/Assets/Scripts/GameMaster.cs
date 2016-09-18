using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {
	public GameObject playerCharacter;

	private GameObject _pc;
	private PlayerCharacter _pcScript;
	// Use this for initialization
	void Start () {
		_pc = Instantiate(playerCharacter,new Vector3(53,10,30), Quaternion.identity) as GameObject;

		_pcScript = _pc.GetComponent<PlayerCharacter>();
	}

	// Update is called once per frame
	void Update () {

	}

	public void LoadCharacter(){
		GameSettings gsScript = GameObject.Find("__GameSettings").GetComponent<GameSettings>();

		gsScript.LoadCharacterData();
	}

}
