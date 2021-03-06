﻿using UnityEngine;
using System.Collections;
using System;						//enum

public class GameSettings : MonoBehaviour {

	public const string PLAYER_SPAWN_POINT = "Player Spawn Point";
	public const string MELEE_WEAPON_MESH_PATH = "Mesh/Weapon/Melee";

	void Awake(){
		DontDestroyOnLoad(this);
	}
	// Use this for initialization

	public void SaveCharacterData(){
		GameObject pc = GameObject.Find("pc");

		PlayerCharacter pcClass = pc.GetComponent<PlayerCharacter>();

		PlayerPrefs.DeleteAll();

		PlayerPrefs.SetString("Player Name", pcClass.Name);

		PlayerPrefs.SetInt("Level", pcClass.Level);
		PlayerPrefs.SetInt("Current Exp", pcClass.FreeExp);


		for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++){
			PlayerPrefs.SetInt(((AttributeName)cnt).ToString() + " - Base Value", pcClass.GetPrimaryAttribute(cnt).BaseValue);
			PlayerPrefs.SetInt(((AttributeName)cnt).ToString() + "  - Exp To Level", pcClass.GetPrimaryAttribute(cnt).ExpToLevel);
		}

		for(int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++){
			PlayerPrefs.SetInt(((VitalName)cnt).ToString() + " - Base Value", pcClass.GetVital(cnt).BaseValue);
			PlayerPrefs.SetInt(((VitalName)cnt).ToString() + "  - Exp To Level", pcClass.GetVital(cnt).ExpToLevel);
			PlayerPrefs.SetInt(((VitalName)cnt).ToString() + "  - Current Value", pcClass.GetVital(cnt).CurValue);

			PlayerPrefs.SetString( ((VitalName)cnt).ToString() + "Mods", pcClass.GetVital(cnt).GetModifyingAttributesString());
		}

		for(int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++){
			PlayerPrefs.SetInt(((SkillName)cnt).ToString() + " - Base Value", pcClass.GetSkill(cnt).BaseValue);
			PlayerPrefs.SetInt(((SkillName)cnt).ToString() + "  - Exp To Level", pcClass.GetSkill(cnt).ExpToLevel);

			PlayerPrefs.SetString( ((SkillName)cnt).ToString() + "Mods", pcClass.GetSkill(cnt).GetModifyingAttributesString());
		}
	}

	public void LoadCharacterData(){
		GameObject pc = GameObject.Find("pc");

		PlayerCharacter pcClass = pc.GetComponent<PlayerCharacter>();

		pcClass.Name = PlayerPrefs.GetString("Player Name", "Name Me");

		pcClass.Level = PlayerPrefs.GetInt("Level", 1);
		pcClass.FreeExp = PlayerPrefs.GetInt("Current Exp", 50);

		for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++){
			pcClass.GetPrimaryAttribute(cnt).BaseValue = PlayerPrefs.GetInt(((AttributeName)cnt).ToString() + " - Base Value", 0);
			pcClass.GetPrimaryAttribute(cnt).ExpToLevel = PlayerPrefs.GetInt(((AttributeName)cnt).ToString() + "  - Exp To Level", Attribute.STARTING_EXP_COST);
			//Debug.Log(pcClass.GetPrimaryAttribute(cnt).BaseValue + " : " + pcClass.GetPrimaryAttribute(cnt).ExpToLevel);
		}

		for(int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++){
			pcClass.GetVital(cnt).BaseValue = PlayerPrefs.GetInt(((VitalName)cnt).ToString() + " - Base Value", 0);
			pcClass.GetVital(cnt).ExpToLevel = PlayerPrefs.GetInt(((VitalName)cnt).ToString() + "  - Exp To Level", 0);

			pcClass.GetVital(cnt).Update();

			pcClass.GetVital(cnt).CurValue = PlayerPrefs.GetInt(((VitalName)cnt).ToString() + "  - Current Value", 0);
			//PlayerPrefs.SetString( ((VitalName)cnt).ToString() + "Mods", pcClass.GetVital(cnt).GetModifyingAttributesString());
			//Debug.Log(pcClass.GetVital(cnt).BaseValue + " - " + pcClass.GetVital(cnt).ExpToLevel + " - " + pcClass.GetVital(cnt).CurValue);
		}

		for(int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++){
			pcClass.GetSkill(cnt).BaseValue = PlayerPrefs.GetInt(((SkillName)cnt).ToString() + " - Base Value", 0);
			pcClass.GetSkill(cnt).ExpToLevel = PlayerPrefs.GetInt(((SkillName)cnt).ToString() + "  - Exp To Level", 0);

			//pcClass.GetSkill(cnt).Update();
			//PlayerPrefs.SetString( ((SkillName)cnt).ToString() + "Mods", pcClass.GetSkill(cnt).GetModifyingAttributesString());
		}

		/*for(int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++){
			Debug.Log(cnt + ":" + pcClass.GetSkill(cnt).BaseValue + " - " + pcClass.GetSkill(cnt).ExpToLevel);
		}*/

	}
}
