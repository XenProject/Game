using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class EquipSlot : MonoBehaviour, IDropHandler {

	private string tmp = ":";

	public void OnDrop(PointerEventData eventData){
		ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();

		//Debug.Log(droppedItem.item.GetType().ToString() == this.name.Split(' ')[0]);
		if(droppedItem.item.GetType().ToString() == this.name.Split(' ')[0]){
			switch(droppedItem.item.GetType().ToString()){
				case "Weapon":
					if(PlayerCharacter.EquipedWeapon==null){
						PlayerCharacter.Inventory[droppedItem.slot] = new Item();
						PlayerCharacter.EquipedWeapon = droppedItem.item;
						droppedItem.slot = -1;
					}else{
						return;
					}
					break;
				case "Clothing":
					Debug.Log(this.name.Contains((droppedItem.item as Clothing).Slot.ToString()));
					if(this.name.Contains((droppedItem.item as Clothing).Slot.ToString()) ){
						switch((droppedItem.item as Clothing).Slot){
							case(ArmorSlot.Chest):
								if(PlayerCharacter.EquipedChest==null){
									PlayerCharacter.Inventory[droppedItem.slot] = new Item();
									PlayerCharacter.EquipedChest = droppedItem.item;
									droppedItem.slot = -1;
								}else{
									return;
								}
								break;
						}
					}
					tmp += (droppedItem.item as Clothing).Slot.ToString();
					break;
			}
			Messenger<string>.Broadcast("Equip", droppedItem.item.GetType().ToString() + " Slot");
			if(tmp!=":")
				Messenger<string>.Broadcast("Equip", droppedItem.item.GetType().ToString() + " Slot" + tmp);
			tmp=":";
		}
	}

}
