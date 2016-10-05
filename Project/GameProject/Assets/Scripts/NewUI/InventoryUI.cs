using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class InventoryUI : MonoBehaviour {

	GameObject inventoryPanel;
	GameObject slotPanel;
	GameObject characterPanel;
	GameObject statsInfo;

	public GameObject inventorySlot;
	public GameObject inventoryItem;

	int slotAmount;
	public List<GameObject> slots = new List<GameObject>();

	private Item _testItem;

	private bool _displayInventoryWindow;
	private bool _displayCharacterWindow;
	public string LastEquipType;

	private PlayerCharacter pcClass;

	void Start () {
		slotAmount = 20;
		inventoryPanel = GameObject.Find("Inventory Panel");
		characterPanel = GameObject.Find("Character Panel");
		statsInfo = GameObject.Find("Stats Info");
		slotPanel = inventoryPanel.transform.FindChild("Slot Panel").gameObject;

		for(int cnt = 0; cnt < slotAmount; cnt++){
			PlayerCharacter.Inventory.Add(new Item());
			slots.Add(Instantiate(inventorySlot));
			slots[cnt].transform.SetParent(slotPanel.transform);
			slots[cnt].GetComponent<Slot>().Id = cnt;
		}

		_testItem = ItemGenerator.CreatePotion();
		AddItem(_testItem);
		AddItem(ItemGenerator.CreateChest());


		_displayCharacterWindow = false;
		_displayInventoryWindow = false;
		inventoryPanel.SetActive(_displayInventoryWindow);
		characterPanel.SetActive(_displayCharacterWindow);
	}

	void OnEnable(){
		Messenger.AddListener("ToggleInventory", ToggleInventoryWindow);
		Messenger.AddListener("ToggleCharacterWindow", ToggleCharacterWindow);

		Messenger<string>.AddListener("Equip", Equip);
	}

	void OnDisable(){
		Messenger.RemoveListener("ToggleInventory", ToggleInventoryWindow);
		Messenger.RemoveListener("ToggleCharacterWindow", ToggleCharacterWindow);

		Messenger<string>.RemoveListener("Equip", Equip);
	}

	// Update is called once per frame
	void Update () {
		if(pcClass==null){
			pcClass = GameObject.Find("pc").GetComponent<PlayerCharacter>();
		}

		if(Input.GetKeyUp(KeyCode.G)){
			AddItem(_testItem);
		}
		if(_displayCharacterWindow){
			statsInfo.transform.GetChild(0).GetComponent<Text>().text = "Stats:\n\n";
			for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++){
				statsInfo.transform.GetChild(0).GetComponent<Text>().text += ((AttributeName)cnt).ToString() + ":  " + pcClass.GetPrimaryAttribute(cnt).AdjustedBaseValue.ToString() + "\n";
			}
		}
	}

	public void AddItem(Item addedItem){

		if(addedItem.Stackable && CheckIfItemIsInInventory(addedItem)){
			for(int cnt = 0; cnt < PlayerCharacter.Inventory.Count; cnt++){
				if(PlayerCharacter.Inventory[cnt].Name == addedItem.Name){
					ItemData data = slots[cnt].transform.GetChild(0).GetComponent<ItemData>();
					data.amount++;
					data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
					data.transform.GetChild(0).GetComponent<Text>().enabled = true;
					break;
				}
			}
		}else{
			for(int cnt = 0;cnt < PlayerCharacter.Inventory.Count; cnt++){
				if(PlayerCharacter.Inventory[cnt].Name == "Error"){
					PlayerCharacter.Inventory[cnt] = addedItem;

					GameObject itemObj = Instantiate(inventoryItem);

					itemObj.GetComponent<ItemData>().item = addedItem;
					itemObj.GetComponent<ItemData>().amount = 1;
					itemObj.GetComponent<ItemData>().slot = cnt;

					itemObj.transform.SetParent(slots[cnt].transform);
					itemObj.GetComponent<Image>().sprite = addedItem.IconSprite;
					itemObj.GetComponent<RectTransform>().localPosition = Vector2.zero;
					itemObj.name = addedItem.Name;
					break;
				}
			}
		}
	}

	private bool CheckIfItemIsInInventory(Item item){
		for(int cnt = 0; cnt < PlayerCharacter.Inventory.Count; cnt++){
			if(PlayerCharacter.Inventory[cnt].Name == item.Name){
				return true;
			}
		}
		return false;
	}

	public void ToggleInventoryWindow(){
		_displayInventoryWindow = !_displayInventoryWindow;
		inventoryPanel.SetActive(_displayInventoryWindow);
	}

	public void ToggleCharacterWindow(){
		_displayCharacterWindow = !_displayCharacterWindow;
		characterPanel.SetActive(_displayCharacterWindow);
	}

	public void Equip(string str){
		LastEquipType = str;
	}

}
