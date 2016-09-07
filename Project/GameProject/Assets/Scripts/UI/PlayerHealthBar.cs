using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour {
	public bool _isPlayerHealthBar;

	private float _healthBarLength;
	private Image _healthBar;
	private Text _hpText;
	// Use this for initialization
	void Start () {
		_healthBar = gameObject.GetComponent<Image>();
		_hpText = gameObject.GetComponentInChildren<Text>();

		OnEnable();
	}

	// Update is called once per frame
	void Update () {

	}

	public void OnEnable() {
		if(_isPlayerHealthBar)
			Messenger<int, int>.AddListener("Player Health Update", OnChangeHealthBarSize);
		else
			Messenger<int, int>.AddListener("Enemy Health Update", OnChangeHealthBarSize);
	}

	public void OnDisable() {
		if(_isPlayerHealthBar)
			Messenger<int, int>.RemoveListener("Player Health Update", OnChangeHealthBarSize);
		else
			Messenger<int, int>.RemoveListener("Enemy Health Update", OnChangeHealthBarSize);
	}

	public void OnChangeHealthBarSize(int curHealth, int maxHealth){
		Debug.Log("Test Listener: CurrentHealth = " + curHealth + " and MaxHealth = " + maxHealth);
		_healthBarLength = curHealth / (float)maxHealth;
		_healthBar.fillAmount = _healthBarLength;
		_hpText.text = curHealth + "/" + maxHealth;
	}

	public void SetHealthBar(bool b){
		_isPlayerHealthBar = b;
	}

}
