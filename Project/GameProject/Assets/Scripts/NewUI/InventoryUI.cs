using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour {

	GameObject inventoryPanel;
	GameObject slotPanel;
	public GameObject inventorySlot;
	public GameObject inventoryItem;

	int slotAmount;
	public List<GameObject> slots = new List<GameObject>();

	private Item _testItem;

	private bool _displayInventoryWindow = false;
	public string LastEquipType;

	void Start () {
		slotAmount = 20;
		inventoryPanel = GameObject.Find("Inventory Panel");
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

	}

	void OnEnable(){
		Messenger.AddListener("ToggleInventory", ToggleInventoryWindow);
		Messenger<string>.AddListener("Equip", Equip);
	}

	void OnDisable(){
		Messenger.RemoveListener("ToggleInventory", ToggleInventoryWindow);
		Messenger<string>.RemoveListener("Equip", Equip);
	}

	// Update is called once per frame
	void Update () {
		if(_displayInventoryWindow)
			inventoryPanel.SetActive(true);
		else
			inventoryPanel.SetActive(false);

		if(Input.GetKeyUp(KeyCode.G)){
			AddItem(_testItem);
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
	}

	public void Equip(string str){
		LastEquipType = str;
	}

}
