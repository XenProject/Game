using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler{
	public Item item;
	public int amount;
	public int slot;

	private InventoryUI inv;
	private Tooltip tooltip;
	private Vector2 offset;

	void Start(){
		inv = GameObject.Find("Canvas").GetComponent<InventoryUI>();
		tooltip = inv.GetComponent<Tooltip>();
	}

	public void OnBeginDrag(PointerEventData eventData){
		if(slot==-1)
			Messenger<string>.Broadcast("Equip", this.transform.parent.name);
		if(item != null){
			this.transform.SetParent(this.transform.parent.parent.parent.parent);
			this.transform.position = eventData.position - offset;
			GetComponent<CanvasGroup>().blocksRaycasts = false;
		}
	}

	public void OnDrag(PointerEventData eventData){
		if(item != null){
			this.transform.position = eventData.position - offset;
		}
	}

	public void OnEndDrag(PointerEventData eventData){
		if(slot!=-1){
			this.transform.SetParent(inv.slots[slot].transform);
			this.transform.position = inv.slots[slot].transform.position;
		}else{
				this.transform.SetParent(GameObject.Find(inv.LastEquipType).transform);
				this.transform.position = GameObject.Find(inv.LastEquipType).transform.position;
		}
		if(!eventData.pointerCurrentRaycast.isValid && slot != -1){
			Debug.Log("Delete Item");
			PlayerCharacter.Inventory[this.slot] = new Item();
			Destroy(gameObject);
		}
		GetComponent<CanvasGroup>().blocksRaycasts = true;
	}

	public void OnPointerDown(PointerEventData eventData){
		if(item !=null){
			offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
		}
	}

	public void OnPointerEnter(PointerEventData eventData){
		tooltip.Activate(item);
	}

	public void OnPointerExit(PointerEventData eventData){
		tooltip.Deactivate();
	}

	public void OnPointerClick( PointerEventData eventData )
	{
		if (eventData.button == PointerEventData.InputButton.Right && this.slot != -1 && (this.item.GetType().ToString() == "Weapon" || this.item.GetType().ToString() == "Clothing") ){
			Debug.Log("Right Click on " + this.transform.name);
			string tmp = ":";

			switch(this.item.GetType().ToString()){
				case "Weapon":
					if(PlayerCharacter.EquipedWeapon==null){
						PlayerCharacter.Inventory[this.slot] = new Item();
						this.slot = -1;
						PlayerCharacter.EquipedWeapon = this.item;
					}else{
						return;
					}
					break;
				case "Clothing":
					//Debug.Log(this.name.Contains((this.item as Clothing).Slot.ToString()));
					if(this.name.Contains((this.item as Clothing).Slot.ToString()) ){
						switch((this.item as Clothing).Slot){
							case(ArmorSlot.Chest):
								if(PlayerCharacter.EquipedChest==null){
									PlayerCharacter.Inventory[this.slot] = new Item();
									this.slot = -1;
									PlayerCharacter.EquipedChest = this.item;
								}else{
									return;
								}
								break;
						}
					}
					tmp += (this.item as Clothing).Slot.ToString();
					break;
			}
			Messenger<string>.Broadcast("Equip", this.item.GetType().ToString() + " Slot");
			if(tmp!=":")
				Messenger<string>.Broadcast("Equip", this.item.GetType().ToString() + " Slot" + tmp);

			inv.ToggleCharacterWindow();
			this.transform.SetParent(GameObject.Find(inv.LastEquipType).transform);
			this.transform.position = GameObject.Find(inv.LastEquipType).transform.position;
			inv.ToggleCharacterWindow();
			tooltip.Deactivate();
		}
	}


}
