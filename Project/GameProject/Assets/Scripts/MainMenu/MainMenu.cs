using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; //LoadScene
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
	private const int CHAR_SLOTS = 3;

	private GameObject charPanel;

	public GameObject CharButton;
	public GameObject CharTitle;

	public List<GameObject> charSlots = new List<GameObject>();

	// Use this for initialization
	void Start () {

		if(PlayerPrefs.HasKey("Player Name")){
			charPanel = GameObject.Find("CurrentCharacters");

			for(int cnt = 0; cnt < 1; cnt++){
				charSlots.Add(Instantiate(CharButton));
				charSlots[cnt].transform.SetParent(charPanel.transform);

				GameObject title = Instantiate(CharTitle);
				title.transform.SetParent(charSlots[cnt].transform);
				title.GetComponent<RectTransform>().localPosition = Vector2.zero;
				title.GetComponent<Text>().text = PlayerPrefs.GetString("Player Name") + "      Lvl " + PlayerPrefs.GetInt("Level").ToString();
			}
		}

	}

	// Update is called once per frame
	void Update () {

	}

	public void ExitGame(){
		Application.Quit();
	}

	public void CreateCharacter(){
		SceneManager.LoadScene("Character Generator");
	}

	public void LoadCharacter(){
		SceneManager.LoadScene("Level1");
	}
}
