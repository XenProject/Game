public class Attribute : BaseStat {
	private string _name;

	public Attribute(){
		_name = "";
		ExpToLevel = 50;
		LevelModifier = 1.05f;
	}

	public string Name{
		get{ return _name; }
		set{ _name = value; }
	}

}

public enum AttributeName{
	Vitality,
	Agility,
	Intelligence,
	Might,
	Karma,
	Speed
}
