using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class myGUI : MonoBehaviour {
	public GUISkin mySkin;

	public float lootWindowHeight = 90;

	public float buttonWidth = 40;
	public float buttonHeight = 40;
	public float closeButtonWidth = 20;
	public float closeButtonHeight = 20;

	public bool _displayLootWindow = false;
	public List<Item> loot = new List<Item>();
	private float _offset = 10;
	private const int LOOT_WINDOW_ID = 0;
	private Rect _lootWindowRect = new Rect(0,0,0,0);
	private Vector2 _lootWindowSlider = Vector2.zero;
	public static Chest chest;

	private string _toolTip = "";

	/**************************************/
	/*               Inventory            */
	/*************************************/
	public bool _displayInventoryWindow = false;
	private const int INVENTORY_WINDOW_ID = 1;
	private Rect _inventoryWindowRect = new Rect(10, Screen.height/4, 170, 265);
	private int _inventoryRows = 6;
	private int _inventoryCols = 4;
	//private Vector2 _inventoryWindowSlider = Vector2.zero;


	// Use this for initialization
	void Start () {

	}

	private void OnEnable(){
		//Messenger<int>.AddListener("PopulateChest", PopulateChest);
		Messenger.AddListener("DisplayLoot", DisplayLoot);
		Messenger.AddListener("CloseChest", ClearWindow);
		Messenger.AddListener("ToggleInventory", ToggleInventoryWindow);
	}

	private void OnDisable(){
		//Messenger<int>.RemoveListener("PopulateChest", PopulateChest);
		Messenger.RemoveListener("DisplayLoot", DisplayLoot);
		Messenger.RemoveListener("CloseChest", ClearWindow);
		Messenger.RemoveListener("ToggleInventory", ToggleInventoryWindow);
	}
	// Update is called once per frame
	void Update () {

	}

	void OnGUI(){
		GUI.skin = mySkin;
		if(_displayInventoryWindow)
			_inventoryWindowRect = GUI.Window(INVENTORY_WINDOW_ID, _inventoryWindowRect, InventoryWindow, "Inventory", "Inventory Window");
		if(_displayLootWindow)
			_lootWindowRect = GUI.Window(LOOT_WINDOW_ID, new Rect(_offset, Screen.height - (_offset*6 + lootWindowHeight), Screen.width/4 - (_offset*2), lootWindowHeight), LootWindow, "Chest", "Loot Window");
		DisplayToolTip();
	}

	private void LootWindow(int id){
		if(GUI.Button(new Rect(_lootWindowRect.width -20, 0, closeButtonWidth, closeButtonHeight), "x" ) ){
			ClearWindow();
		}

		if(chest==null)
			return;

			if(chest.loot.Count == 0){
				ClearWindow();
				return;
			}

		_lootWindowSlider = GUI.BeginScrollView(new Rect(_offset * 0.5f, 15, _lootWindowRect.width - 10, 70), _lootWindowSlider, new Rect(0, 0, chest.loot.Count * buttonWidth + _offset, buttonHeight + _offset) );

		for(int cnt = 0; cnt < chest.loot.Count; cnt++){
			if(GUI.Button(new Rect( 5 + buttonWidth * cnt, _offset, buttonWidth, buttonHeight), new GUIContent(chest.loot[cnt].Icon, chest.loot[cnt].ToolTip() ), "Inventory Slot Common" )){
				Debug.Log(chest.loot[cnt].ToolTip());
				PlayerCharacter.Inventory.Add(chest.loot[cnt]);
				chest.loot.RemoveAt(cnt);
			}
		}

		GUI.EndScrollView();

		SetToolTip();
	}

	private void DisplayLoot(){
		_displayLootWindow = true;
	}
	/*private void PopulateChest(int x){
		for(int cnt = 0; cnt<x; cnt++)
			loot.Add(new Item());
		_displayLootWindow = true;
	}*/

	private void ClearWindow(){
		loot.Clear();

		chest.OnMouseUp();

		chest = null;
		_displayLootWindow = false;
	}

//Inventory

	private void InventoryWindow(int id){
		int cnt = 0;

		for(int y = 0; y < _inventoryRows; y++){
			for(int x = 0; x < _inventoryCols; x++){
				if(cnt < PlayerCharacter.Inventory.Count){
					GUI.Button(new Rect(5 + (x * buttonWidth), 20 + (y * buttonHeight), buttonWidth, buttonHeight), new GUIContent( PlayerCharacter.Inventory[cnt].Icon, PlayerCharacter.Inventory[cnt].ToolTip() ), "Inventory Slot Common" );
				}else{
					GUI.Label(new Rect(5 + (x * buttonWidth), 20 + (y * buttonHeight), buttonWidth, buttonHeight), "", "Inventory Slot Common");
				}
				cnt++;
			}

		}
		GUI.DragWindow();
		SetToolTip();
	}

	public void ToggleInventoryWindow(){
		_displayInventoryWindow = !_displayInventoryWindow;
	}

	private void SetToolTip(){
		if(Event.current.type == EventType.Repaint && GUI.tooltip != _toolTip){
			if(_toolTip != "")
				_toolTip = "";

			if(GUI.tooltip != "")
				_toolTip = GUI.tooltip;
		}
	}

	private void DisplayToolTip(){
		if(_toolTip != "")
			GUI.Box(new Rect(Screen.width /2 -100, 10, 200, 100), _toolTip);
	}

}
