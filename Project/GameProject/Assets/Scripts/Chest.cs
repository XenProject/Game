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

	// Use this for initialization
	void Start () {
		state = Chest.State.Close;
		anim = GetComponent<Animation>();
		rend = GetComponentInChildren<Renderer>();
		tmpColor = rend.material.color;
	}

	// Update is called once per frame
	void Update () {

	}

	public void OnMouseEnter(){
		Debug.Log("Mouse on chest");
		HighLight(true);
	}

	public void OnMouseExit(){
		Debug.Log("Exit");
		HighLight(false);
	}

	public void OnMouseUp(){
		Debug.Log("Up");

		GameObject go = GameObject.FindGameObjectWithTag("Player");

		if(go==null)
			return;

		if(Vector3.Distance(transform.position, go.transform.position) > 2.0f)
			return;

		switch(state){
			case State.Open:
				state = Chest.State.Inbetween;
				StartCoroutine("Close");
				break;
			case State.Close:
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
		anim["Open"].speed = 1.0f;
		anim.Play("Open");

		yield return new WaitForSeconds(anim["Open"].length);
		state = Chest.State.Open;
	}

	private IEnumerator Close(){
		anim["Open"].time = anim["Open"].length;
		anim["Open"].speed = -1.0f;
		anim.Play("Open");

		yield return new WaitForSeconds(anim["Open"].length);
		state = Chest.State.Close;
	}

	private void HighLight(bool glow){
		if(glow){
			rend.material.color = Color.yellow;
		}else{
			rend.material.color = tmpColor;
		}
	}
}
