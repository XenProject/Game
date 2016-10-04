using UnityEngine;
using System.Collections;

public static class ItemGenerator{
	public const float BASE_MELEE_RANGE = 1.5f;
	public const float BASE_RANGED_RANGE = 7.5f;

	private const string MELEE_WEAPON_PATH = "Icons/Weapon/Melee/";
	private const string POTION_PATH = "Icons/Potions/";
	private const string CHEST_PATH = "Icons/Armor/Chest/";

	public static Item CreateItem(){

		Item item = CreateWeapon();

		//private string _name;
		item.Value = Random.Range(1,101);
		item.Rarity = RarityTypes.Common;
		item.MaxDurability = Random.Range(50,65);
		item.CurDurability = item.MaxDurability;
		item.Stackable = false;

		return item;
	}

	private static Weapon CreateWeapon(){
		Weapon weapon = CreateMeleeWeapon();

		return weapon;
	}

	private static Weapon CreateMeleeWeapon(){
		Weapon meleeWeapon = new Weapon();

		string[] weaponNames = new string[1];
		weaponNames[0] = "KatanaTest";

		meleeWeapon.Name = weaponNames[Random.Range(0, weaponNames.Length)];

		meleeWeapon.MaxDamage = Random.Range(5,11);
		meleeWeapon.DamageVariance = Random.Range(0.2f,0.7f);
		meleeWeapon.MaxRange = BASE_MELEE_RANGE;
		meleeWeapon.TypeOfDamage = DamageType.Sword;

		meleeWeapon.Icon = Resources.Load(MELEE_WEAPON_PATH + meleeWeapon.Name) as Texture2D;
		meleeWeapon.IconSprite = Resources.Load<Sprite>(MELEE_WEAPON_PATH + meleeWeapon.Name);

		meleeWeapon.AddBuff(new Attribute("Strength"), Random.Range(0,4));
		meleeWeapon.AddBuff(new Attribute("Agility"), Random.Range(0,2));

		return meleeWeapon;
	}

	public static Consumable CreatePotion(){
		Consumable potion = new Consumable();

		//private string _name;
		string[] potionNames = new string[2];
		potionNames[0] = "Health Potion";
		potionNames[1] = "Mana Potion";

		potion.Name = potionNames[Random.Range(0, potionNames.Length)];
		potion.Value = 1;
		potion.Rarity = RarityTypes.Mythical;
		potion.MaxDurability = 0;
		potion.CurDurability = 0;
		potion.Stackable = true;

		potion.Icon = Resources.Load(POTION_PATH + potion.Name) as Texture2D;
		potion.IconSprite = Resources.Load<Sprite>(POTION_PATH + potion.Name);

		return potion;
	}

	public static Clothing CreateChest(){
		Clothing chest = new Clothing(ArmorSlot.Chest);

		//private string _name;
		string[] chestNames = new string[1];
		chestNames[0] = "Chest Test";

		chest.Name = chestNames[Random.Range(0, chestNames.Length)];
		chest.Value = Random.Range(1,101);
		chest.Rarity = RarityTypes.Legendary;
		chest.MaxDurability = Random.Range(50,65);
		chest.CurDurability = chest.MaxDurability;
		chest.Stackable = false;

		chest.Icon = Resources.Load(CHEST_PATH + chest.Name) as Texture2D;
		chest.IconSprite = Resources.Load<Sprite>(CHEST_PATH + chest.Name);

		return chest;
	}

}

public enum ItemType{
	Armor,
	Weapon,
	Potion,
	Scroll
}
