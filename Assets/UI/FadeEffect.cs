using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FadeIn() {
        StartCoroutine(FadeToFullAlpha(0.25f, gameObject.GetComponent<SpriteRenderer>()))
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
