using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tooltip : MonoBehaviour {
	private Item item;
	private string data;
	private GameObject tooltip;

	void Start(){
		tooltip = GameObject.Find("Tooltip");
		tooltip.SetActive(false);
	}

	void Update(){
		if(tooltip.activeSelf){
			tooltip.transform.position = Input.mousePosition;
		}
	}

	public void Activate(Item item){
		this.item = item;
		ConstructDataString();

		if(Input.mousePosition.x < Screen.width/2)
			tooltip.GetComponent<RectTransform>().pivot = new Vector2(-0.1f,1f);
		else
			tooltip.GetComponent<RectTransform>().pivot = new Vector2(1.1f,1f);
			
		tooltip.SetActive(true);
	}

	public void Deactivate(){
		tooltip.SetActive(false);
	}

	public void ConstructDataString(){
		data = "<color=#ffffff><b>" + item.Name + "</b></color>\n\n" + "Rarity:  ";

		switch(item.Rarity){
			case(RarityTypes.Common):
				data += "<color=#ffffff>";
				break;
			case(RarityTypes.Uncommon):
				data += "<color=#00d5fb>";
				break;
			case(RarityTypes.Rare):
				data += "<color=#0000ff>";
				break;
			case(RarityTypes.Mythical):
				data += "<color=#8322ff>";
				break;
			case(RarityTypes.Legendary):
				data += "<color=#ffbf00>";
				break;
			case(RarityTypes.Set):
				data += "<color=#00ff00>";
				break;
		}

		data += item.Rarity + "</color>";
		if(item is Weapon)
			data += "\nDamage:  " + ((int)((item as Weapon).MaxDamage * (item as Weapon).DamageVariance)).ToString() + " - " + ((item as Weapon).MaxDamage).ToString();

		if(item is BuffItem){
			foreach( DictionaryEntry de in (item as BuffItem).GetBuffs() ){
          data += "\n" + de.Key + ":  " + de.Value.ToString();
      }
		}


		if(item.MaxDurability!=0)
			data += "\n\nDurability:  " + item.CurDurability + "/" + item.MaxDurability;

		tooltip.transform.GetChild(0).GetComponent<Text>().text = data;
	}

}
