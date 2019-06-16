using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public FadeEffect black;
    public FadeEffect white;
    public CharacterController2D character;
    public Vector2 initialYinPosition;

    private bool isYang = true;
    private bool isSwapping = false;

    private Vector2 yangPosition;
    private Vector2 yinPosition;

    // Start is called before the first frame update
    void Start()
    {
        yinPosition = initialYinPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B) && !isSwapping) {
            StartCoroutine(Swap());
        }
    }

    IEnumerator Swap() {
        isSwapping = true;
        if(isYang) { // light to dark
            GameObject.Find("Yang").GetComponent<SpriteRenderer>().enabled = false;
            yangPosition = character.transform.position;
            black.FadeIn();
            yield return new WaitForSeconds(1);
			character.transform.position = yinPosition;
            black.FadeOut();
            yield return new WaitForSeconds(1);
            GameObject.Find("Yin").GetComponent<SpriteRenderer>().enabled = true;

        }
        else {
			GameObject.Find("Yin").GetComponent<SpriteRenderer>().enabled = false;
			yinPosition = character.transform.position;
			white.FadeIn();
			yield return new WaitForSeconds(1);
			character.transform.position = yangPosition;
			white.FadeOut();
			yield return new WaitForSeconds(1);
			GameObject.Find("Yang").GetComponent<SpriteRenderer>().enabled = true;
        }
		isSwapping = false;
        isYang = !isYang;
    }
}
