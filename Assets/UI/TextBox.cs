using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBox : MonoBehaviour
{

    private Text txtObj;
    private string text;
    public float scrollSpeed = 10f;
    public float scrollMultiplier = 5f; // When the player clicks A or on the textbox

    // Start is called before the first frame update
    void Start()
    {
        txtObj = GetComponentInChildren<Text>();
        text = txtObj.text;
        txtObj.text = "";

		StartCoroutine(InsertLetters());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)) {
            OnClick();
        }
    }

    public void OnClick() {
       scrollSpeed *= scrollMultiplier;
    }

    IEnumerator InsertLetters() {
        for(int i = 0; i < text.Length; i++) {
            txtObj.text += text.Substring(i, 1);
            yield return new WaitForSeconds(1/scrollSpeed);
        }
    }
}
