using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ExpirienceBar : MonoBehaviour {

	private float _expBarLength;
	public Image _expBar;
	private Text _expText;
	// Use this for initialization
	void Start () {
		_expText = gameObject.GetComponentInChildren<Text>();
	}

	// Update is called once per frame
	void Update () {

	}

	public void OnEnable(){
		Messenger<int, int>.AddListener("Player Exp Update", OnChangeExpBarSize);
	}

	public void OnDisable(){
		Messenger<int, int>.RemoveListener("Player Exp Update", OnChangeExpBarSize);
	}

	public void OnChangeExpBarSize(int curExp, int maxExp){
		_expBarLength = curExp / (float)maxExp;
		_expBar.fillAmount = _expBarLength;
		_expText.text = curExp + "/" + maxExp;
	}

}
