using System.Collections.Generic;
using UnityEngine;

public class ModifiedStat : BaseStat {
	private List<ModifyingAttribute> _mods;
	private int _modValue;

	public ModifiedStat(){
		_mods = new List<ModifyingAttribute>();
		_modValue = 0;
	}

	public void AddModifier( ModifyingAttribute mod ){
		_mods.Add(mod);
	}

	private void CalculateModValue(){
		_modValue = 0;
		//Debug.Log("Calculate mod Value, Count - " + _mods.Count);
		if(_mods.Count > 0){
			foreach(ModifyingAttribute att in _mods){
				_modValue += (int)(att.attribute.AdjustedBaseValue * att.ratio);
				//Debug.Log(att);
				//Debug.Log(_modValue);
			}
		}
	}

	public new int AdjustedBaseValue{
		get{ return BaseValue + BuffValue + _modValue; }
	}

	public void Update(){
		CalculateModValue();
	}

	public string GetModifyingAttributesString(){
		string temp = "";

		for(int cnt = 0;cnt < _mods.Count; cnt++){
			temp +=_mods[cnt].attribute.Name;
			temp += "_";
			temp += _mods[cnt].ratio;

			if(cnt < _mods.Count - 1)
				temp += "|";
		}
		UnityEngine.Debug.Log(temp);
		return temp;
	}

}

public struct ModifyingAttribute{
	public Attribute attribute;
	public float ratio;

	public ModifyingAttribute(Attribute att, float rat){
		attribute = att;
		ratio = rat;
	}
}
