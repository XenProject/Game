using UnityEngine;
using System.Collections.Generic;

public class PlayerCharacter : BaseCharacter {
	private static GameObject[] _weaponMesh;

	private PlayerHealth ph;

	private static List<Item> _inventory = new List<Item>();
	public static List<Item> Inventory{
		get{ return _inventory; }
	}

	private static Item _equipedWeapon;

	public static Item EquipedWeapon{
		get{ return _equipedWeapon; }
		set{ _equipedWeapon = value;

				HideWeaponMeshes();

				switch(_equipedWeapon.Name){
					case "KatanaTest":
						_weaponMesh[0].active = true;
						break;
					default:
						Debug.LogWarning("Unknown Weapon Mesh");
						break;
				}
		}
	}

	// Update is called once per frame
	void Start(){
		ph = (PlayerHealth)gameObject.GetComponent("PlayerHealth");

		Transform weaponMount = transform.Find("EthanSkeleton/EthanHips/EthanSpine/EthanSpine1/EthanSpine2/EthanNeck/EthanRightShoulder/EthanRightArm/EthanRightForeArm/EthanRightHand/EthanRightHandMiddle1");
		int count = weaponMount.GetChildCount();
		_weaponMesh = new GameObject[count-1];

		for(int cnt = 1; cnt < count; cnt++){
				_weaponMesh[cnt-1] = weaponMount.GetChild(cnt).gameObject;
				Debug.Log(_weaponMesh[cnt-1]);
		}

		HideWeaponMeshes();
	}

	void Update () {
		if(ph!=null){
			Messenger<int, int>.Broadcast("Player Health Update", ph.curHealth, ph.maxHealth);
			Messenger<int, int>.Broadcast("Player Mana Update", ph.curMana, ph.maxMana);
			Messenger<int, int>.Broadcast("Player Energy Update", ph.curEnergy, ph.maxEnergy);
			Debug.Log("Health - " + ph.maxHealth + ", Mana - " + ph.maxMana + ", Energy - " + ph.curEnergy);
		}
	}

	private static void HideWeaponMeshes(){
		for(int cnt = 0; cnt < _weaponMesh.Length; cnt++){
			_weaponMesh[cnt].active = false;
		}
	}
}
