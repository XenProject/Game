using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler {
	public int Id;
	private InventoryUI inv;

	void Start(){
		inv = GameObject.Find("Canvas").GetComponent<InventoryUI>();
	}

	public void OnDrop(PointerEventData eventData){
		ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();

		if(droppedItem.slot == -1){
			if(PlayerCharacter.Inventory[Id].Name != "Error"){
				return;
			}else{
				switch(droppedItem.item.GetType().ToString()){
					case "Weapon":
						PlayerCharacter.EquipedWeapon = null;
					break;
					case "Clothing":
						switch((droppedItem.item as Clothing).Slot){
							case(ArmorSlot.Chest):
								PlayerCharacter.EquipedChest = null;
							break;
						}
					break;
				}
				PlayerCharacter.Inventory[Id] = droppedItem.item;
				droppedItem.slot = Id;
			}
		}

		if(PlayerCharacter.Inventory[Id].Name == "Error"){
			PlayerCharacter.Inventory[droppedItem.slot] = new Item();
			PlayerCharacter.Inventory[Id] = droppedItem.item;
			droppedItem.slot = Id;
		}else if(droppedItem.slot != Id){
			Transform item = this.transform.GetChild(0);
			item.GetComponent<ItemData>().slot = droppedItem.slot;
			item.transform.SetParent(inv.slots[droppedItem.slot].transform);
			item.transform.position = inv.slots[droppedItem.slot].transform.position;
			PlayerCharacter.Inventory[droppedItem.slot] = item.GetComponent<ItemData>().item;

			droppedItem.slot = Id;
			droppedItem.transform.SetParent(this.transform);
			droppedItem.transform.position = this.transform.position;

			PlayerCharacter.Inventory[Id] = droppedItem.item;
		}
	}
}
