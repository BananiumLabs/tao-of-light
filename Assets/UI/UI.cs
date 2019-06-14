using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Text[] texts;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeRoutine(0.25f, texts));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator FadeRoutine(float t, Text[] i) {

        while(true) {
            foreach(Text txt in i) {
                StartCoroutine(FadeTextToZeroAlpha(t, txt));
            }
            yield return new WaitForSeconds(1f);
			foreach (Text txt in i) {
				StartCoroutine(FadeTextToFullAlpha(t, txt));
			}
			yield return new WaitForSeconds(1f);
        }
    }

	public IEnumerator FadeTextToFullAlpha(float t, Text i)
	{
		i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
		while (i.color.a < 1.0f)
		{
			i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (t));
			yield return new WaitForSeconds(0.1f);
		}
	}

	public IEnumerator FadeTextToZeroAlpha(float t, Text i)
	{
		i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
		while (i.color.a > 0.0f)
		{
			i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (t));
			yield return new WaitForSeconds(0.1f);
		}
	}
}
