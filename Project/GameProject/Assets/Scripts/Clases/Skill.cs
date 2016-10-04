public class Skill : ModifiedStat {
	private bool _known;

	public Skill(){
		_known = false;
		ExpToLevel = 25;
		LevelModifier = 1.1f;
	}

	public bool IsKnown{
		get{ return _known;}
		set{ _known = value;}
	}
}
public enum SkillName{
	Crushers,
	Swords,
	Daggers,
	Bows,
	Magic,
	Chainmails,
	Leathers,
	Clothes
}
