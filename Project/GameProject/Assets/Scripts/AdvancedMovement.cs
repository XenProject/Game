using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]
public class AdvancedMovement : MonoBehaviour {
	public enum State{
		Idle,
		Init,
		Setup,
		Action
	}
	public enum Turn{
		left = -1,
		none = 0,
		right = 1
	}
	public enum Forward{
		back = -1,
		none = 0,
		forward = 1
	}

	public float walkSpeed = 5;
	public float runMultiplier = 2;
	public float strafeSpeed = 2.5f;
	public float rotateSpeed = 250;
	public float gravity = 20;
	public float airTime = 0;
	public float fallTime = .5f;

	public CollisionFlags _collisionFlags;
	private Vector3 _moveDirection;
	private Transform _myTransform;
	private CharacterController _controller;

	private Turn _turn;
	private Forward _forward;
	private Turn _strafe;
	private bool _run;

	private State _state;

	void Awake(){
		_myTransform = transform;
		_controller = GetComponent<CharacterController>();

		_state = AdvancedMovement.State.Init;
	}
	// Use this for initialization
	IEnumerator Start () {
		while(true){
			switch(_state){
				case State.Init:
				break;
				case State.Setup:
				SetUp();
				break;
			}
			yield return null;
		}
		_moveDirection = Vector3.zero;

		//Init();
	}

	// Update is called once per frame
	void Update () {
		ActionPicker();
	}

	private void SetUp(){
		_moveDirection = Vector3.zero;

		_turn = AdvancedMovement.Turn.none;
		_forward = AdvancedMovement.Forward.none;
		_strafe = Turn.none;
		_run = true;

		_state = AdvancedMovement.State.Idle;
	}

	private void ActionPicker(){
		/*if(Mathf.Abs(Input.GetButton("Rotate Player")) > 0){
			_myTransform.Rotate(0, Input.GetAxis("Rotate Player") * Time.deltaTime * rotateSpeed, 0);
		}*/

		if(_controller.isGrounded){
			airTime = 0;
			_moveDirection = new Vector3(0, 0, Input.GetAxis("Move Forward"));
			_moveDirection = _myTransform.TransformDirection(_moveDirection).normalized;
			_moveDirection *= walkSpeed;
		}else{
			if((_collisionFlags & CollisionFlags.CollidedBelow) == 0){
				airTime += Time.deltaTime;

				if(airTime > fallTime){
					Fall();
				}
			}
		}
		_moveDirection.y = gravity * Time.deltaTime;

		_collisionFlags = _controller.Move(_moveDirection);
	}

	public void Idle(){

	}

	public void Fall(){

	}

	public void Walk(){

	}

	public void Run(){

	}

}
