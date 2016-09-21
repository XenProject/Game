using UnityEngine;

public static class ItemGenerator{
	public const float BASE_MELEE_RANGE = 1.5f;
	public const float BASE_RANGED_RANGE = 7.5f;

	private const string MELEE_WEAPON_PATH = "Icons/Weapon/Melee/";

	public static Item CreateItem(){

		Item item = CreateWeapon();

		//private string _name;
		item.Value = Random.Range(1,101);
		item.Rarity = RarityTypes.Common;
		item.MaxDurability = Random.Range(50,65);
		item.CurDurability = item.MaxDurability;

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
		meleeWeapon.TypeOfDamage = DamageType.Physical;

		meleeWeapon.Icon = Resources.Load(MELEE_WEAPON_PATH + meleeWeapon.Name) as Texture2D;

		return meleeWeapon;
	}

}

public enum ItemType{
	Armor,
	Weapon,
	Potion,
	Scroll
}
