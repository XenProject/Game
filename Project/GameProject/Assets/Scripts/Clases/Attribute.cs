public class Attribute : BaseStat {

	public Attribute(){
		ExpToLevel = 50;
		LevelModifier = 1.1f;
	}

	
}

public enum AttributeName{
	Vitality,
	Agility,
	Intelligence
}
