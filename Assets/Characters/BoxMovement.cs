using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// PLACE THIS ON THE BOX OBJECTS
public class BoxMovement : MonoBehaviour {

    int direction;
    const float TILE_SIZE = 0.32f;
    // LATEST recorded impact direction.

    private Rigidbody2D rb2D;
    public CharacterController2D player;
    // Start is called before the first frame update
    void Start () {
        rb2D = gameObject.GetComponent<Rigidbody2D> ();
    }

    void Update() {
        if(player.m_State == CharacterController2D.State.WaitingForInput) {
			gameObject.transform.position = new Vector2(Mathf.Round(transform.position.x * (1 / TILE_SIZE)) / (1 / TILE_SIZE), Mathf.Round(transform.position.y * (1 / TILE_SIZE)) / (1 / TILE_SIZE));
        }
    }

}