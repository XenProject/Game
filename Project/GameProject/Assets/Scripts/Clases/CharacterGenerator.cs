using UnityEngine;
using System.Collections;
using System;						//enum
using UnityEngine.SceneManagement; //LoadScene

public class CharacterGenerator : MonoBehaviour {
	private PlayerCharacter _toon;
	private const int STARTING_POINTS = 15;
	private const int MIN_STARTING_ATTRIBUTE_VALUE = 4;
	private int pointsLeft;

	private const int OFFSET = 50;
	private const int LINE_HEIGHT = 30;

	private const int STAT_LABEL_WIDTH = 150;
	private const int BASEVALUE_LABEL_WIDTH = 50;
	private const int BUTTON_WIDTH = 30;
	private const int BUTTON_HEIGHT = 30;

	private int statStartingPos = 120;

	public GUIStyle myStyle;
	public GUIStyle myStyle2;
	public GUISkin mySkin;

	private Color[] _color;

	public GameObject playerPrefab;
	// Use this for initialization
	void Start () {
		GameObject pc = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		pc.name = "pc";

		_color = new Color[3];
		_color[0] = Color.red;
		_color[1] = Color.blue;
		_color[2] = Color.yellow;

		_toon = pc.GetComponent<PlayerCharacter>();

		pointsLeft = STARTING_POINTS;
		for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++){
			_toon.GetPrimaryAttribute(cnt).BaseValue = MIN_STARTING_ATTRIBUTE_VALUE;
		}
		_toon.StatUpdate();
	}

	// Update is called once per frame
	void Update () {

	}

	void OnGUI(){
		GUI.skin = mySkin;

		DisplayName();
		DispalyPointsLeft();
		DisplayAttributes();
		DisplayVitals();
		DisplaySkills();

		if(_toon.Name == "" || pointsLeft > 0)
			DisplayCreateLabel();
		else
			DisplayCreateButton();
	}

	private void DisplayName(){
		GUI.Label(new Rect(OFFSET, OFFSET, STAT_LABEL_WIDTH, LINE_HEIGHT), "Name:");
		_toon.Name = GUI.TextField(new Rect(OFFSET + STAT_LABEL_WIDTH/2, OFFSET, STAT_LABEL_WIDTH, LINE_HEIGHT), _toon.Name);
	}

	private void DisplayAttributes(){
		for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++){
			GUI.Label( new Rect(OFFSET,																//x
			 										statStartingPos + (cnt * LINE_HEIGHT),//y
													STAT_LABEL_WIDTH, 										//width
													LINE_HEIGHT														//height
							 ),((AttributeName)cnt).ToString()/*, myStyle*/);
			GUI.Label( new Rect(STAT_LABEL_WIDTH + OFFSET, 						 //x
													statStartingPos + (cnt * LINE_HEIGHT), //y
													BASEVALUE_LABEL_WIDTH, 								 //width
													LINE_HEIGHT														 //height
							 ), _toon.GetPrimaryAttribute(cnt).AdjustedBaseValue.ToString());
			if( GUI.Button( new Rect(OFFSET + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH, //x
															statStartingPos + (cnt * BUTTON_HEIGHT), 						//y
															BUTTON_WIDTH, 																			//width
															BUTTON_HEIGHT																				//height
										), "", myStyle)){
				if(_toon.GetPrimaryAttribute(cnt).BaseValue > MIN_STARTING_ATTRIBUTE_VALUE){
					_toon.GetPrimaryAttribute(cnt).BaseValue--;
					pointsLeft++;
					_toon.StatUpdate();
				}
			}
			if( GUI.Button( new Rect(OFFSET + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH + BUTTON_WIDTH, //x
															statStartingPos + (cnt * BUTTON_HEIGHT), 													 //y
															BUTTON_WIDTH, 																										 //width
															BUTTON_HEIGHT																											 //height
										), ""/*, myStyle*/)){
				if(pointsLeft > 0){
					_toon.GetPrimaryAttribute(cnt).BaseValue++;
					pointsLeft--;
					_toon.StatUpdate();
				}
			}
		}
	}

	private void DisplayVitals(){
		Color tmp = mySkin.label.normal.textColor;
		for(int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++){
			mySkin.label.normal.textColor = _color[cnt];
			GUI.Label( new Rect(OFFSET, 																			//x
													statStartingPos + ((cnt + 8) * LINE_HEIGHT),  //y
													STAT_LABEL_WIDTH, 														//width
													LINE_HEIGHT																		//height
							 ),((VitalName)cnt).ToString());
			mySkin.label.normal.textColor = tmp;
			GUI.Label( new Rect(STAT_LABEL_WIDTH + OFFSET, 											//x
													statStartingPos + ((cnt + 8) * LINE_HEIGHT), 		//y
													BASEVALUE_LABEL_WIDTH, 													//width
													LINE_HEIGHT																			//height
							 ), _toon.GetVital(cnt).AdjustedBaseValue.ToString());
		}
	}

	private void DisplaySkills(){
		for(int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++){
			GUI.Label( new Rect(OFFSET + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH + BUTTON_WIDTH * 2 + OFFSET*2,	//x
			 										statStartingPos + (cnt * LINE_HEIGHT), 																						//y
													STAT_LABEL_WIDTH, 																																//width
													LINE_HEIGHT																																				//height
							), ((SkillName)cnt).ToString());
			GUI.Label( new Rect(OFFSET + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH + BUTTON_WIDTH * 2 + OFFSET*2 + STAT_LABEL_WIDTH,		//x
													statStartingPos + (cnt * LINE_HEIGHT), 																																//y
													BASEVALUE_LABEL_WIDTH, 																																								//width
													LINE_HEIGHT																																														//height
							 ), _toon.GetSkill(cnt).AdjustedBaseValue.ToString());
		}
	}

	private void DispalyPointsLeft(){
		GUI.Label(new Rect(OFFSET + STAT_LABEL_WIDTH*2, OFFSET, 300, LINE_HEIGHT), "Points Left:" + pointsLeft.ToString());
	}

	private void DisplayCreateLabel(){
		GUI.Label(new Rect( Screen.width/2 - 50, statStartingPos + 9*LINE_HEIGHT, 150 ,LINE_HEIGHT ), "Creating...", myStyle2);
	}

	private void DisplayCreateButton(){
		if( GUI.Button(new Rect(Screen.width/2 - 50, 														//x
												statStartingPos + 9 * LINE_HEIGHT,  //y
												150, 																					//width
												LINE_HEIGHT																		//height
							), "Create", myStyle2))
		{
			GameSettings gsScript = GameObject.Find("__GameSettings").GetComponent<GameSettings>();

			UpdateCurVitalValues();

			gsScript.SaveCharacterData();

			SceneManager.LoadScene("Level1");
		}
	}

	private void UpdateCurVitalValues(){
		for(int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++){
			_toon.GetVital(cnt).CurValue = _toon.GetVital(cnt).AdjustedBaseValue;
		}
	}

}
