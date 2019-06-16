using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Attach to sprite that you want to fade in and out
public class FadeEffect : MonoBehaviour {

	public float startingOpacity = 1f;

	void Start() {
		SpriteRenderer sprite = GetComponent<SpriteRenderer>();
		sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, startingOpacity);
	}

    public void FadeIn() {
        StartCoroutine(FadeToFullAlpha(0.25f, GetComponent<SpriteRenderer>()));
    }
    public void FadeOut() {
        StartCoroutine(FadeToZeroAlpha(0.25f, GetComponent<SpriteRenderer>()));
    }

	public void FadeInOut() {
		StartCoroutine(FadeInOutEnumerator());
	}

	public IEnumerator FadeInOutEnumerator() {
		FadeIn();
		yield return new WaitForSeconds(1f);
		FadeOut();
	}

	private IEnumerator FadeToFullAlpha(float t, SpriteRenderer i)
	{
		i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
		while (i.color.a < 1.0f)
		{
			i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (t));
			yield return new WaitForSeconds(0.1f);
		}
	}

	private IEnumerator FadeToZeroAlpha(float t, SpriteRenderer i)
	{
		i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
		while (i.color.a > 0.0f)
		{
			i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (t));
			yield return new WaitForSeconds(0.1f);
		}
	}
}
