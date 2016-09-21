using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if(Input.GetButtonUp("Toggle Inventory") ){
			Messenger.Broadcast("ToggleInventory");
		}

		if(Input.GetButtonUp("Toggle Character Window") ){
			Messenger.Broadcast("ToggleCharacterWindow");
		}
	}
}
