using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class EquipSlot : MonoBehaviour, IDropHandler {

	private string tmp = ":";

	public void OnDrop(PointerEventData eventData){
		ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();

		switch(droppedItem.item.GetType().ToString()){
			case "Weapon":
				PlayerCharacter.Inventory[droppedItem.slot] = new Item();
				PlayerCharacter.EquipedWeapon = droppedItem.item;
				droppedItem.slot = -1;
				break;
			case "Clothing":
				switch((droppedItem.item as Clothing).Slot){
					case(ArmorSlot.Chest):
						PlayerCharacter.Inventory[droppedItem.slot] = new Item();
						PlayerCharacter.EquipedChest = droppedItem.item;
						droppedItem.slot = -1;
						break;
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
