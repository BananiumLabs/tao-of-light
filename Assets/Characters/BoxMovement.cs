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

    // Start is called before the first frame update
    void Start () {
        rb2D = gameObject.GetComponent<Rigidbody2D> ();
    }

	void Update()
	{
		if (player.m_State == CharacterController2D.State.WaitingForInput && colliding)
		{
			Vector2 dir = player.m_Facing;

			if (dir == Vector2.left)
			{
				gameObject.transform.position = new Vector2(Mathf.Floor(transform.position.x * (1 / TILE_SIZE)) / (1 / TILE_SIZE), transform.position.y);
			}
			else if (dir == Vector2.right)
			{
				gameObject.transform.position = new Vector2(Mathf.Ceil(transform.position.x * (1 / TILE_SIZE)) / (1 / TILE_SIZE), transform.position.y);
			}
			else if (dir == Vector2.up)
			{
				gameObject.transform.position = new Vector2(transform.position.x, Mathf.Ceil(transform.position.y * (1 / TILE_SIZE)) / (1 / TILE_SIZE));
			}
			else if (dir == Vector2.down)
			{
				gameObject.transform.position = new Vector2(transform.position.x, Mathf.Floor(transform.position.y * (1 / TILE_SIZE)) / (1 / TILE_SIZE));
			}

			rb2D.velocity = Vector2.zero;
            colliding = false;
		}
	}

    void OnCollisionEnter2D(Collision2D col) {
        colliding = true;
    }
}