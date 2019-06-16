using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public FadeEffect black;
    public FadeEffect white;
    public CharacterController2D character;
    public GameObject sprite;
    public Vector2 initialYinPosition;
    public RuntimeAnimatorController[] controllers;

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
            sprite.GetComponent<Animator>().runtimeAnimatorController = controllers[0];
            sprite.SetActive(false);
            yangPosition = character.transform.position;
            black.FadeIn();
            yield return new WaitForSeconds(1);
			character.transform.position = yinPosition;
            black.FadeOut();
            yield return new WaitForSeconds(1);
            sprite.SetActive(true);

        }
        else {
			sprite.GetComponent<Animator>().runtimeAnimatorController = controllers[1];
			sprite.SetActive(false);
			yinPosition = character.transform.position;
			white.FadeIn();
			yield return new WaitForSeconds(1);
			character.transform.position = yangPosition;
			white.FadeOut();
			yield return new WaitForSeconds(1);
			sprite.SetActive(true);
        }
		isSwapping = false;
        isYang = !isYang;
    }
}
