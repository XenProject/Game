using UnityEngine;

public class Jewerly : BuffItem {
	private JewerlySlot _slot;

	public Jewerly(){
		_slot = JewerlySlot.Other;
	}

	public Jewerly(JewerlySlot slot){
		_slot = slot;
	}

	public JewerlySlot Slot{
		get{ return _slot; }
		set{ _slot = value; }
	}
}

public enum JewerlySlot{
	EarRings,
	Necklaces,
	Rings,
	Other
}
