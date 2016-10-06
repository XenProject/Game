using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerCharacter : BaseCharacter {

	private GameObject levelTitle;

	private static GameObject[] _weaponMesh;

	private PlayerHealth ph;

	private static List<Item> _inventory = new List<Item>();
	public static List<Item> Inventory{
		get{ return _inventory; }
	}

	private static Item _equipedWeapon;

	public static Item EquipedWeapon{
		get{ return _equipedWeapon; }
		set{
				if(value == null){
					foreach( DictionaryEntry de in (_equipedWeapon as BuffItem).GetBuffs() ){
		          GameObject.Find("pc").GetComponent<PlayerCharacter>().GetPrimaryAttributeByName(de.Key.ToString()).BuffValue = 0;
		      }
					for(int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++){
						GameObject.Find("pc").GetComponent<PlayerCharacter>().GetVital(cnt).Update();
					}
					GameObject.Find("pc").GetComponent<PlayerHealth>().UpdateVitalBar();
				}

				_equipedWeapon = value;

				HideWeaponMeshes();

				if(_equipedWeapon == null)
					return;

				switch(_equipedWeapon.Name){
					case "KatanaTest":
						_weaponMesh[0].SetActive(true);
						break;
					case "Axe":
						_weaponMesh[1].SetActive(true);
						break;
					default:
						Debug.LogWarning("Unknown Weapon Mesh");
						break;
				}

				foreach( DictionaryEntry de in (_equipedWeapon as BuffItem).GetBuffs() ){
	          GameObject.Find("pc").GetComponent<PlayerCharacter>().GetPrimaryAttributeByName(de.Key.ToString()).BuffValue = (int)de.Value;
	      }

				for(int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++){
					GameObject.Find("pc").GetComponent<PlayerCharacter>().GetVital(cnt).Update();
				}
				GameObject.Find("pc").GetComponent<PlayerHealth>().UpdateVitalBar();
		}
	}

	private static Item _equipedChest;

	public static Item EquipedChest{
		get{ return _equipedChest; }
		set{ _equipedChest = value;	}
	}

	// Update is called once per frame
	void Start(){
		ph = (PlayerHealth)gameObject.GetComponent("PlayerHealth");

		if(ph!=null){
			Transform weaponMount = transform.Find("EthanSkeleton/EthanHips/EthanSpine/EthanSpine1/EthanSpine2/EthanNeck/EthanRightShoulder/EthanRightArm/EthanRightForeArm/EthanRightHand/EthanRightHandMiddle1");
			int count = weaponMount.childCount;
			_weaponMesh = new GameObject[count-1];

			for(int cnt = 1; cnt < count; cnt++){
					_weaponMesh[cnt-1] = weaponMount.GetChild(cnt).gameObject;
					Debug.Log(_weaponMesh[cnt-1]);
			}

			HideWeaponMeshes();

			levelTitle = GameObject.Find("LevelTitle");
		}
	}

	void Update () {
		if(ph!=null){
			Messenger<int, int>.Broadcast("Player Health Update", ph.curHealth, ph.maxHealth);
			Messenger<int, int>.Broadcast("Player Mana Update", ph.curMana, ph.maxMana);
			Messenger<int, int>.Broadcast("Player Energy Update", ph.curEnergy, ph.maxEnergy);
			Messenger<int, int>.Broadcast("Player Exp Update", ph.curExp, ph.maxExp);

			levelTitle.GetComponent<Text>().text = gameObject.GetComponent<PlayerCharacter>().Level.ToString();

			Debug.Log("Health - " + ph.maxHealth + ", Mana - " + ph.maxMana + ", Energy - " + ph.curEnergy);
		}
	}

	private static void HideWeaponMeshes(){
		for(int cnt = 0; cnt < _weaponMesh.Length; cnt++){
			_weaponMesh[cnt].SetActive(false);
		}
	}
}
