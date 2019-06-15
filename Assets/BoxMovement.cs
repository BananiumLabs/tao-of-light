using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMovement : MonoBehaviour
{   

    
    private Rigidbody2D rb2D;
    // Start is called before the first frame update
    void Start(){
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        Debug.Log(c.GetContact(1));
         // We then get the opposite (-Vector3) and normalize it
        
        // And finally we add force in the direction of dir and multiply it by force. 
        // This will push back the player
       
        transform.position = new Vector2(0.32f,0.32f);
        Debug.Log("COllision");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
