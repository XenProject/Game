using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chest : MonoBehaviour {

	public enum State{
		Open,
		Close,
		Inbetween
	}

	public State state;
	private Animation anim;
	private Renderer rend;
	private Color tmpColor;

	public float maxDistance = 3.0f;

	private GameObject _player;
	private Transform _myTransform;
	public bool inUse = false;
	private bool _used = false;

	public List<Item> loot = new List<Item>();

	// Use this for initialization
	void Start () {
		state = Chest.State.Close;
		anim = GetComponent<Animation>();
		rend = GetComponentInChildren<Renderer>();
		tmpColor = rend.material.color;
	}

	void Update(){
		if(!inUse)
			return;
		if(_player == null)
			return;
		if(Vector3.Distance(transform.position, _player.transform.position) > maxDistance )
			myGUI.chest.ForceClose();
			//Messenger.Broadcast("CloseChest");
	}

	public void OnMouseEnter(){
		//Debug.Log("Mouse on chest");
		HighLight(true);
	}

	public void OnMouseExit(){
		//Debug.Log("Exit");
		HighLight(false);
	}

	public void OnMouseUp(){
		//Debug.Log("Up");

		GameObject go = GameObject.FindGameObjectWithTag("Player");

		if(go==null)
			return;

		if(Vector3.Distance(transform.position, go.transform.position) > maxDistance && !inUse)
			return;

		switch(state){
			case State.Open:
				state = Chest.State.Inbetween;
				StartCoroutine("Close");
				ForceClose();
				break;
			case State.Close:
				if(myGUI.chest !=null){
					myGUI.chest.ForceClose();
				}
				state = Chest.State.Inbetween;
				StartCoroutine("Open");
				break;
		}
	}
		/*if(state == Chest.State.Close){
			Open();
		}else{
			Close();
		}*/

	private IEnumerator Open(){
		myGUI.chest = this;

		_player = GameObject.FindGameObjectWithTag("Player");
		inUse = true;

		anim["Open"].speed = 1.0f;
		anim.Play("Open");

		if(!_used)
			PopulateChest(5);

		yield return new WaitForSeconds(anim["Open"].length);

		state = Chest.State.Open;
		Messenger.Broadcast("DisplayLoot");
		//Messenger<int>.Broadcast("PopulateChest", 5, MessengerMode.DONT_REQUIRE_LISTENER);
	}

	private IEnumerator Close(){
		_player = null;
		inUse = false;

		anim["Open"].time = anim["Open"].length;
		anim["Open"].speed = -1.0f;
		anim.Play("Open");

		yield return new WaitForSeconds(anim["Open"].length);
		state = Chest.State.Close;
	}

	public void ForceClose(){
		Messenger.Broadcast("CloseChest");

		StopCoroutine("Open");
		StartCoroutine("Close");
	}

	private void HighLight(bool glow){
		if(glow){
			rend.material.color = Color.yellow;
		}else{
			rend.material.color = tmpColor;
		}
	}

	private void PopulateChest(int x){
		for(int cnt = 0; cnt<x; cnt++){
			loot.Add(ItemGenerator.CreateItem());
		}
		_used = true;
	}

}
