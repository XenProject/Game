using UnityEngine;
using System.Collections;
using System;

public class BaseCharacter : MonoBehaviour {
	private string _name;
	private int _level;
	private int _freeExp;

	private Attribute[] _primaryAttribute;
	private Vital[] _vital;
	private Skill[] _skill;

	public void Awake(){
		_name = string.Empty;
		_level = 1;
		_freeExp = 50;

		_primaryAttribute = new Attribute[Enum.GetValues(typeof(AttributeName)).Length];
		_vital = new Vital[Enum.GetValues(typeof(VitalName)).Length];
		_skill = new Skill[Enum.GetValues(typeof(SkillName)).Length];

		SetupPrimaryAttributes();
		SetupVitals();
		SetupSkills();
	}

	public string Name{
		get{ return _name; }
		set{ _name = value; }
	}

	public int Level{
		get{ return _level; }
		set{ _level = value; }
	}

	public int FreeExp{
		get{ return _freeExp; }
		set{ _freeExp = value; }
	}

	public void AddExp(int exp){
		_freeExp += exp;

		CalculateLevel();
	}

	public void CalculateLevel(){

	}

	private void SetupPrimaryAttributes(){
		for(int cnt = 0; cnt < _primaryAttribute.Length; cnt++){
			_primaryAttribute[cnt] = new Attribute();
			_primaryAttribute[cnt].Name = ((AttributeName)cnt).ToString();
		}
	}

	private void SetupVitals(){
		for(int cnt = 0; cnt < _vital.Length; cnt++)
			_vital[cnt] = new Vital();
		SetupVitalModifiers();
	}

	private void SetupSkills(){
		for(int cnt = 0; cnt < _skill.Length; cnt++)
			_skill[cnt] = new Skill();
		SetupSkillModifiers();
	}

	public Attribute GetPrimaryAttribute(int index){
		return _primaryAttribute[index];
	}

	public Vital GetVital(int index){
		return _vital[index];
	}

	public Skill GetSkill(int index){
		return _skill[index];
	}

	private void SetupVitalModifiers(){
		//Heatlh
		//Debug.Log("Vitalmod");
		GetVital((int)VitalName.Health).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Vitality), 25.0f));
		//Mana
		GetVital((int)VitalName.Mana).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Intelligence), 20.0f));
		//Energy
		GetVital((int)VitalName.Energy).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Agility), 20.0f));
	}

	private void SetupSkillModifiers(){
		//Debug.Log("Skill");
		GetSkill((int)SkillName.Melee).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Vitality), 1.0f));
		//Ranged
		GetSkill((int)SkillName.Ranged).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Agility), 0.7f));
		//Magic
		GetSkill((int)SkillName.Magic).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Intelligence), 2.0f));
	}

	public void StatUpdate(){
		for(int cnt = 0; cnt < _vital.Length; cnt++)
			_vital[cnt].Update();
		for(int cnt = 0; cnt < _skill.Length; cnt++)
			_skill[cnt].Update();
	}

}
