using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// PLACE THIS ON THE BOX OBJECTS
public class BoxMovement : MonoBehaviour {

	int direction;
	bool colliding = false;
	const float TILE_SIZE = 0.32f;
	// LATEST recorded impact direction.

	private Rigidbody2D rb2D;
	public CharacterController2D player;

	public enum State {
		Moving,
		Pushing,
		WaitingForInput
	}
	private const float MovementBlockSize = 0.32f;
	private const float MovingSpeedPPS = 2.0f;
	private const float TimeToMoveOneBlock = MovementBlockSize / MovingSpeedPPS;

	public Vector2 m_MovingFrom;
	public Vector2 m_MovingTo;
	public float m_MoveTimer;
	public State m_State;
	// Start is called before the first frame update
	void Start () {
		rb2D = gameObject.GetComponent<Rigidbody2D> ();
		m_State = State.WaitingForInput;
	}

	void Update () {
		if (m_State == State.Moving) {
			MoveUpdate ();
		}

		if (m_State == State.WaitingForInput) {
			InputUpdate ();
		}

		// if (player.m_State == CharacterController2D.State.WaitingForInput && colliding) {
		// 	Vector2 dir = player.m_Facing;

		// 	if (dir == Vector2.left) {
		// 		gameObject.transform.position = new Vector2 (Mathf.Floor (transform.position.x * (1 / TILE_SIZE)) / (1 / TILE_SIZE), transform.position.y);
		// 	} else if (dir == Vector2.right) {
		// 		gameObject.transform.position = new Vector2 (Mathf.Ceil (transform.position.x * (1 / TILE_SIZE)) / (1 / TILE_SIZE), transform.position.y);
		// 	} else if (dir == Vector2.up) {
		// 		gameObject.transform.position = new Vector2 (transform.position.x, Mathf.Ceil (transform.position.y * (1 / TILE_SIZE)) / (1 / TILE_SIZE));
		// 	} else if (dir == Vector2.down) {
		// 		gameObject.transform.position = new Vector2 (transform.position.x, Mathf.Floor (transform.position.y * (1 / TILE_SIZE)) / (1 / TILE_SIZE));
		// 	}

		// 	rb2D.velocity = Vector2.zero;
		colliding = false;
	}

	private void MoveUpdate () {
		// Move towards our target position
		m_MoveTimer += Time.deltaTime;

		var ratio = m_MoveTimer / TimeToMoveOneBlock;

		var pos = Vector2.Lerp (m_MovingFrom, m_MovingTo, ratio);
		gameObject.transform.position = pos;

		if (ratio >= 1.0f) {
			m_State = State.WaitingForInput;
			m_MoveTimer = Mathf.Repeat (m_MoveTimer, TimeToMoveOneBlock);
		}
	}

	private void InputUpdate () {
		m_MovingFrom = gameObject.transform.position;

		if (colliding) {

			// We may not be allowed to move, however, if that would cause a collision with the default colliders
			var pos = gameObject.transform.position;
			var hit = Physics2D.Raycast (pos, player.m_Facing, MovementBlockSize, 1 << LayerMask.NameToLayer ("Default"));

			if (hit) {
				m_State = State.WaitingForInput;
				m_MoveTimer = 0.0f;
			} else {
			m_State = State.Moving;
			m_MovingTo = m_MovingFrom + player.m_Facing * MovementBlockSize;
			// m_MovingTo.x = RoundToGrid (m_MovingTo.x);
			}
		} else {
			// No input means we aren't even trying to move
			m_MoveTimer = 0.0f;
			// m_Animator.SetBool ("Moving", false);
		}
	}

	void OnCollisionEnter2D (Collision2D col) {
		print (col.gameObject.ToString ());
		if (col.gameObject.tag == "Player") {
			colliding = true;
			print ("colliding is true");
		}

	}

	private float RoundToGrid (float value) {
		if (value % MovementBlockSize == 0.00) {
			return value;
		} else {
			return value + (value % MovementBlockSize);
		}
	}
}