public class Vital : ModifiedStat {

	private int _curValue;
	// Use this for initialization
	void Start () {
		_curValue = 0;
		ExpToLevel = 50;
		LevelModifier = 1.1f;
	}

	// Update is called once per frame
	public int CurValue{
		get{
			if(_curValue > AdjustedBaseValue)
				_curValue = AdjustedBaseValue;
				return _curValue;
		}
		set{_curValue = value;}
	}
}

public enum VitalName{
	Health,
	Mana,
	Energy
}
