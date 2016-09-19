using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {
	public GameObject playerCharacter;
	public GameObject gameSettings;

	private GameObject _pc;
	//private PlayerCharacter _pcScript;
	private Vector3 _playerSpawnPointPos;

	// Use this for initialization
	void Start () {
		_playerSpawnPointPos = new Vector3(55,10,30);
		GameObject go = GameObject.Find(GameSettings.PLAYER_SPAWN_POINT);
		if(go == null){
			Debug.LogWarning("Can not find Spawn Point!");

			go = new GameObject(GameSettings.PLAYER_SPAWN_POINT);
			Debug.Log("Created Spawn Point");

			go.transform.position = _playerSpawnPointPos;
			Debug.Log("Moved Spawn Point");
		}

		_pc = Instantiate(playerCharacter,go.transform.position, Quaternion.identity) as GameObject;
		_pc.name = "pc";

		//_pcScript = _pc.GetComponent<PlayerCharacter>();

		LoadCharacter();
	}

	// Update is called once per frame
	void Update () {

	}

	public void LoadCharacter(){
		GameObject gs = GameObject.Find("__GameSettings");
		if(gs == null){
			GameObject gs1 = Instantiate(gameSettings, Vector3.zero, Quaternion.identity) as GameObject;
			gs1.name = "__GameSettings";
		}
		GameSettings gsScript = GameObject.Find("__GameSettings").GetComponent<GameSettings>();

		gsScript.LoadCharacterData();
	}

}
