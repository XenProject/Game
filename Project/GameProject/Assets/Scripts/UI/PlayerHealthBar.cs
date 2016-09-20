using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour {
	public bool _isPlayerHealthBar;

	private float _healthBarLength;
	public Image _healthBar;
	private Text _hpText;

	private float _manaBarLength;
	public Image _manaBar;
	private Text _manaText;

	private float _energyBarLength;
	public Image _energyBar;
	private Text _energyText;
	// Use this for initialization
	void Start () {
		Text[] texts = gameObject.GetComponentsInChildren<Text>();
		_hpText = texts[0];
		if(_isPlayerHealthBar){			
			_manaText = texts[1];
			_energyText = texts[2];
		}
		OnEnable();
	}

	// Update is called once per frame
	void Update () {

	}

	public void OnEnable() {
		if(_isPlayerHealthBar){
			Messenger<int, int>.AddListener("Player Health Update", OnChangeHealthBarSize);
			Messenger<int, int>.AddListener("Player Mana Update", OnChangeManaBarSize);
			Messenger<int, int>.AddListener("Player Energy Update", OnChangeEnergyBarSize);
		}else{
			ToogleDisplay(false);
			Messenger<int, int>.AddListener("Enemy Health Update", OnChangeHealthBarSize);
			Messenger<bool>.AddListener("Show Mob HealthBar", ToogleDisplay);
		}
	}

	public void OnDisable() {
		if(_isPlayerHealthBar){
			Messenger<int, int>.RemoveListener("Player Health Update", OnChangeHealthBarSize);
			Messenger<int, int>.RemoveListener("Player Mana Update", OnChangeManaBarSize);
			Messenger<int, int>.RemoveListener("Player Energy Update", OnChangeEnergyBarSize);
		}else{
			Messenger<int, int>.RemoveListener("Enemy Health Update", OnChangeHealthBarSize);
			Messenger<bool>.RemoveListener("Show Mob HealthBar", ToogleDisplay);
		}
	}

	public void OnChangeHealthBarSize(int curHealth, int maxHealth){
		//Debug.Log("Test Listener: CurrentHealth = " + curHealth + " and MaxHealth = " + maxHealth);
		_healthBarLength = curHealth / (float)maxHealth;
		_healthBar.fillAmount = _healthBarLength;
		_hpText.text = curHealth + "/" + maxHealth;
	}

	public void OnChangeManaBarSize(int curMana, int maxMana){
		_manaBarLength = curMana / (float)maxMana;
		_manaBar.fillAmount = _manaBarLength;
		_manaText.text = curMana + "/" + maxMana;
	}

	public void OnChangeEnergyBarSize(int curEnergy, int maxEnergy){
		_energyBarLength = curEnergy / (float)maxEnergy;
		_energyBar.fillAmount = _energyBarLength;
		_energyText.text = curEnergy + "/" + maxEnergy;
	}

	public void SetHealthBar(bool b){
		_isPlayerHealthBar = b;
	}

	private void ToogleDisplay(bool show){
		_healthBar.transform.parent.GetComponent<Image>().enabled = show;
		_healthBar.enabled = show;
		_healthBar.GetComponentInChildren<Text>().enabled = show;
	}

}
