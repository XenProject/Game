using UnityEngine;
using System.Collections;
using System;						//enum

public class CharacterGenerator : MonoBehaviour {
	private PlayerCharacter _toon;
	private const int STARTING_POINTS = 15;
	private const int MIN_STARTING_ATTRIBUTE_VALUE = 4;
	private int pointsLeft;

	private const int OFFSET = 5;
	private const int LINE_HEIGHT = 22;

	private const int STAT_LABEL_WIDTH = 100;
	private const int BASEVALUE_LABEL_WIDTH = 30;
	private const int BUTTON_WIDTH = 22;
	private const int BUTTON_HEIGHT = 22;

	private int statStartingPos = 40;

	public GUIStyle myStyle;
	// Use this for initialization
	void Start () {
		_toon = new PlayerCharacter();
		_toon.Awake();

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
		DisplayName();
		DispalyPointsLeft();
		DisplayAttributes();
		DisplayVitals();
		DisplaySkills();
	}

	private void DisplayName(){
		GUI.Label(new Rect(10, 10, 50, 25), "Name:");
		_toon.Name = GUI.TextField(new Rect(65, 10, 100, 25), _toon.Name);
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
										), "-")){
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
										), "+")){
				if(pointsLeft > 0){
					_toon.GetPrimaryAttribute(cnt).BaseValue++;
					pointsLeft--;
					_toon.StatUpdate();
				}
			}
		}
	}

	private void DisplayVitals(){
		for(int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++){
			GUI.Label( new Rect(OFFSET, 																			//x
													statStartingPos + ((cnt + 7) * LINE_HEIGHT),  //y
													STAT_LABEL_WIDTH, 														//width
													LINE_HEIGHT																		//height
							 ),((VitalName)cnt).ToString());
			GUI.Label( new Rect(STAT_LABEL_WIDTH + OFFSET, 											//x
													statStartingPos + ((cnt + 7) * LINE_HEIGHT), 		//y
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
		GUI.Label(new Rect(250, 10, 100, 25), "Points Left:" + pointsLeft.ToString());
	}
}
