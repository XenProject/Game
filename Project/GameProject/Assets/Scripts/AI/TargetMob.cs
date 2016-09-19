using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetMob : MonoBehaviour {
	public List<Transform> targets;
	public Transform selectedTarget;

	// Use this for initialization
	void Start () {
		targets = new List<Transform>();
		selectedTarget = null;

		AddAllEnemies();
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Tab)){
			TargetEnemy();
		}
	}

	public void AddAllEnemies(){
		GameObject[] go = GameObject.FindGameObjectsWithTag("Enemy");

		foreach(GameObject enemy in go){
			AddTarget(enemy.transform);
		}
	}

	public void AddTarget(Transform enemy){
		targets.Add(enemy);
	}


	private void TargetEnemy(){
		if(targets.Count == 0)
			AddAllEnemies();
		if(targets.Count>0){
			if(selectedTarget == null){
				SortTargetsByDistance();
				selectedTarget = targets[0];
				SelectTarget();
			}else{
				int index = targets.IndexOf(selectedTarget);

				if(index < targets.Count - 1){
					index++;
				}else{
					index = 0;
				}
				DeselectTarget();
				selectedTarget = targets[index];
				SelectTarget();
			}
		}
	}

	private void SortTargetsByDistance(){
		Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		targets.Sort(
			delegate(Transform t1, Transform t2){
				return Vector3.Distance(t1.position, playerTransform.position).CompareTo(Vector3.Distance(t2.position, playerTransform.position));
				}
			);
	}

	private void SelectTarget(){
		Transform name = selectedTarget.FindChild("Name");

		if(name == null){
			Debug.LogError("Could not found name on " + selectedTarget.name);
			return;
		}
		name.GetComponent<TextMesh>().text = selectedTarget.GetComponent<Mob>().Name;
		name.GetComponent<MeshRenderer>().enabled = true;
		selectedTarget.GetComponent<Mob>().DisplayHealth();

		Messenger<bool>.Broadcast("Show Mob HealthBar", true);
	}

	private void DeselectTarget(){
		selectedTarget.FindChild("Name").GetComponent<MeshRenderer>().enabled = false;
		selectedTarget = null;
		Messenger<bool>.Broadcast("Show Mob HealthBar", false);
	}

}
