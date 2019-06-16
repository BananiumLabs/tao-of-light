using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Attach to sprite that you want to fade in and out
public class FadeEffect : MonoBehaviour {

    public void FadeIn() {
        StartCoroutine(FadeToFullAlpha(0.25f, gameObject.GetComponent<SpriteRenderer>()));
    }
    public void FadeOut() {
        StartCoroutine(FadeToFullAlpha(0.25f, gameObject.GetComponent<SpriteRenderer>()));
    }
	public IEnumerator FadeInOut() {
		FadeIn();
		yield return new WaitForSeconds(0.5f);
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
