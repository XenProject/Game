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

	public string _toolTip = "";
	public string _tmp = "";

	/**************************************/
	/*               Inventory            */
	/*************************************/
	/*public bool _displayInventoryWindow = false;
	private const int INVENTORY_WINDOW_ID = 1;
	private Rect _inventoryWindowRect = new Rect(10, Screen.height/4, 170, 265);
	private int _inventoryRows = 6;
	private int _inventoryCols = 4;

	private float _doubleClickTimer = 0;
	private const float DOUBLE_CLICK_TIMER_THRESHHOLD = .3f;
	private Item _selectedItem = null;
	//private Vector2 _inventoryWindowSlider = Vector2.zero;*/

	/**************************************/
	/*               Character Window            */
	/*************************************/
	/*public bool _displayCharacterWindow = false;
	private const int CHARACTER_WINDOW_ID = 2;
	private Rect _characterWindowRect = new Rect(Screen.width - 180, Screen.height/4, 170, 265);
	private int _characterPanel = 0;
	private string[] _characterPanelNames = new string[] {"Equipment", "Attributes", "Skills"};*/

	private InventoryUI inv;

	// Use this for initialization
	void Start () {
		inv = gameObject.GetComponent<InventoryUI>();
	}

	private void OnEnable(){
		//Messenger<int>.AddListener("PopulateChest", PopulateChest);
		Messenger.AddListener("DisplayLoot", DisplayLoot);
		Messenger.AddListener("CloseChest", ClearWindow);
		//Messenger.AddListener("ToggleInventory", ToggleInventoryWindow);
		//Messenger.AddListener("ToggleCharacterWindow", ToggleCharacterWindow);
	}

	private void OnDisable(){
		//Messenger<int>.RemoveListener("PopulateChest", PopulateChest);
		Messenger.RemoveListener("DisplayLoot", DisplayLoot);
		Messenger.RemoveListener("CloseChest", ClearWindow);
		//Messenger.RemoveListener("ToggleInventory", ToggleInventoryWindow);
		//Messenger.RemoveListener("ToggleCharacterWindow", ToggleCharacterWindow);
	}
	// Update is called once per frame
	void Update () {

	}

	void OnGUI(){
		GUI.skin = mySkin;
		//if(_displayCharacterWindow)
		//	_characterWindowRect = GUI.Window(CHARACTER_WINDOW_ID, _characterWindowRect, CharacterWindow, "Char", "Character Window");
		//if(_displayInventoryWindow)
		//	_inventoryWindowRect = GUI.Window(INVENTORY_WINDOW_ID, _inventoryWindowRect, InventoryWindow, "Inventory", "Inventory Window");
		if(_displayLootWindow)
			_lootWindowRect = GUI.Window(LOOT_WINDOW_ID, new Rect(_offset, Screen.height - (_offset*6 + lootWindowHeight), Screen.width/4 - (_offset*2), lootWindowHeight), LootWindow, "Chest", "Loot Window");
		//DisplayToolTip();
		//Debug.Log("ToolTip:\n" + GUI.tooltip);
	}

	private void LootWindow(int id){
		GUI.skin = mySkin;
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
				//PlayerCharacter.Inventory.Add(chest.loot[cnt]);
				inv.AddItem(chest.loot[cnt]);
				chest.loot.RemoveAt(cnt);
			}
		}

		GUI.EndScrollView();
		//Debug.Log("Loot: " + GUI.tooltip);
		//SetToolTip();
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

	/*private void InventoryWindow(int id){
		int cnt = 0;

		for(int y = 0; y < _inventoryRows; y++){
			for(int x = 0; x < _inventoryCols; x++){
				if(cnt < PlayerCharacter.Inventory.Count){
					if(GUI.Button(new Rect(5 + (x * buttonWidth), 20 + (y * buttonHeight), buttonWidth, buttonHeight), new GUIContent( PlayerCharacter.Inventory[cnt].Icon, PlayerCharacter.Inventory[cnt].ToolTip() ), "Inventory Slot Common" )){
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
								_doubleClickTimer = 0; /***********************If bug switch 0 to Time.time******************//*
							}
						}else{
							_doubleClickTimer = Time.time;
							_selectedItem = PlayerCharacter.Inventory[cnt];
						}
					}
				}else{
					GUI.Label(new Rect(5 + (x * buttonWidth), 20 + (y * buttonHeight), buttonWidth, buttonHeight), "", "Inventory Slot Common");
				}
				cnt++;
			}

		}
		//Debug.Log("Invent:" + GUI.tooltip);
		SetToolTip();
		GUI.DragWindow();
	}

	public void ToggleInventoryWindow(){
		_displayInventoryWindow = !_displayInventoryWindow;
	}*/

	/*private void SetToolTip(){
		if(Event.current.type == EventType.Repaint && GUI.tooltip != _toolTip){
			if(_toolTip != ""){
				Debug.Log("Mouse Out");
			}
			if(GUI.tooltip != ""){
				Debug.Log("Mouse On");
			}

			_toolTip = GUI.tooltip;
		}
	}

	private void DisplayToolTip(){
		if(_toolTip != "")
			GUI.Box(new Rect(Screen.width /2 - 100, 10, 200, 100), _toolTip);
	}*/

	//Character Window

	/*public void CharacterWindow(int id){
		_characterPanel = GUI.Toolbar(new Rect(5, 25, _characterWindowRect.width - 10, 50), _characterPanel, _characterPanelNames);

		switch(_characterPanel){
			case 0:
				DisplayEquipment();
				break;
			case 1:
				DisplayAttributes();
				break;
			case 2:
				DisplaySkills();
				break;
		}

		GUI.DragWindow();
	}

	public void ToggleCharacterWindow(){
		_displayCharacterWindow = !_displayCharacterWindow;
	}

	private void DisplayEquipment(){
		GUI.skin = mySkin;
		if(PlayerCharacter.EquipedWeapon == null){
			GUI.Label(new Rect(5, 80, 40, 40), "", "Inventory Slot Common" );
		}else{
			if(GUI.Button(new Rect(5, 80, 40, 40), new GUIContent( PlayerCharacter.EquipedWeapon.Icon, PlayerCharacter.EquipedWeapon.ToolTip() ), "Inventory Slot Common" )){
				PlayerCharacter.Inventory.Add(PlayerCharacter.EquipedWeapon);
				PlayerCharacter.EquipedWeapon = null;
			}
		}

		SetToolTip();
	}*/

	private void DisplayAttributes(){

	}

	private void DisplaySkills(){

	}

}
