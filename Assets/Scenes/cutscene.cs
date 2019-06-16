using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cutscene : MonoBehaviour
{
    public TextBox[] textboxes;
    public Animator charAnimController;

    public bool cutsceneInitiated = false;
    private int currTextbox = -1;
    public bool cutsceneCompleted = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && textboxes[currTextbox].completed && cutsceneInitiated) {
            NextTextbox();
        }

        if(Input.GetKeyDown(KeyCode.B) && cutsceneCompleted) {
            textboxes[currTextbox].gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if(col.tag == "Player") {
            if(!cutsceneInitiated) {
                cutsceneInitiated = true;
                NextTextbox();
                charAnimController.Play("Cutscene");
            }
        }
    }

    void NextTextbox() {
        if(currTextbox >= 0)
		    textboxes[currTextbox].gameObject.SetActive(false);

        currTextbox++;
        if(currTextbox == textboxes.Length - 2) {
			cutsceneCompleted = true;
            charAnimController.Play("Idle");
        }

        textboxes[currTextbox].gameObject.SetActive(true);
    }
}
