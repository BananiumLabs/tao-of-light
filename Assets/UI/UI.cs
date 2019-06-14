using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public Text[] texts;
    public SpriteRenderer fade;

    // Start is called before the first frame update
    void Start()
    {
        fade.color = new Color(1f, 1f, 1f, 0f);
        StartCoroutine(FadeRoutine(0.25f, texts));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)) {
            print("test");
            StartCoroutine(BeginGame(fade));
        }
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

    private IEnumerator BeginGame(SpriteRenderer fade) {
        fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, 0.0f);
        while (fade.color.a < 1f) {
            fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, fade.color.a + 0.1f);
			yield return new WaitForSeconds(0.1f);
        }
        SceneManager.LoadScene("Tutorial");
    }
}
