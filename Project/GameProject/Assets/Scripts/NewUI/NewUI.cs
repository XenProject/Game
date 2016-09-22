using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class NewUI : MonoBehaviour {

	GameObject inventoryPanel;
	GameObject slotPanel;
	public GameObject inventorySlot;
	public GameObject inventoryItem;

	int slotAmount;
	public List<GameObject> slots = new List<GameObject>();

	private Item _testItem;
	/****************EquipWeapon*****************/
	private float _doubleClickTimer = 0;
	private const float DOUBLE_CLICK_TIMER_THRESHHOLD = .3f;
	private Item _selectedItem = null;

	void Start () {
		slotAmount = 20;
		inventoryPanel = GameObject.Find("Inventory Panel");
		slotPanel = inventoryPanel.transform.FindChild("Slot Panel").gameObject;
		for(int cnt = 0; cnt < slotAmount; cnt++){
			slots.Add(Instantiate(inventorySlot));
			slots[cnt].transform.SetParent(slotPanel.transform);
		}

		_testItem = ItemGenerator.CreateItem();
		AddItem(_testItem);

	}

	// Update is called once per frame
	void Update () {
		Debug.Log(PlayerCharacter.Inventory.Count);
		if(Input.GetKeyUp(KeyCode.G)){
			AddItem(_testItem);
		}
	}

	public void AddItem(Item addedItem){
		PlayerCharacter.Inventory.Add(addedItem);

		int cnt = PlayerCharacter.Inventory.Count-1;

		GameObject itemObj = Instantiate(inventoryItem);
		itemObj.transform.SetParent(slots[cnt].transform);
		itemObj.GetComponent<Image>().sprite = addedItem.IconSprite;
		itemObj.GetComponent<RectTransform>().localPosition = Vector2.zero;
	}

	/*public void EquipWeapon(){

		if(_doubleClickTimer != 0 && _selectedItem != null ){
			if(Time.time -  _doubleClickTimer < DOUBLE_CLICK_TIMER_THRESHHOLD){
				Debug.Log("Equip Weapon: " + PlayerCharacter.Inventory[cnt].Name);

				if(PlayerCharacter.EquipedWeapon == null){
					PlayerCharacter.EquipedWeapon = _selectedItem;
					PlayerCharacter.Inventory.RemoveAt(cnt);
				}else{
					Item tmp = PlayerCharacter.EquipedWeapon;
					PlayerCharacter.EquipedWeapon = PlayerCharacter.Inventory[cnt];
					PlayerCharacter.Inventory[cnt] = tmp;
				}

				_doubleClickTimer = 0;
				_selectedItem = null;
			}else{
				_doubleClickTimer = 0; /***********************If bug switch 0 to Time.time******************/
			/*}
		}else{
			_doubleClickTimer = Time.time;
			_selectedItem = PlayerCharacter.Inventory[cnt];
		}
	}*/
}
