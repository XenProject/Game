using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler{
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

}
