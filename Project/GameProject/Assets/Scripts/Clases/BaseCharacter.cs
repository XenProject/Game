using UnityEngine;
using System.Collections;

public class BaseCharacter : MonoBehaviour {
	private string _name;
	private int _level;
	private uint _freeExp;
	// Use this for initialization
	void Awake(){
		_name = string.Empty;
		_level = 0;
		_freeExp = 0;
	}

	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public string Name{
		get{ return _name; }
		set{ _name = value; }
	}

	public int Level{
		get{ return _level; }
		set{ _level = value; }
	}

	public uint FreeExp{
		get{ return _freeExp; }
		set{ _freeExp = value; }
	}

}
