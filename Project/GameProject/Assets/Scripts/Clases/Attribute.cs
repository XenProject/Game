public class Attribute : BaseStat {

	public Attribute(){
		ExpToLevel = 50;
		LevelModifier = 1.05f;
	}

	public Attribute(string name){
		Name = name;
	}

}

public enum AttributeName{
	Strength,
	Agility,
	Intelligence
}
