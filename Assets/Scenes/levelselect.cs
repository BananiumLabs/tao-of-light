using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelselect : MonoBehaviour {

    public int level = 0;
    // EditorBuildSettingsScene[] scenes;

    public void LoadScene (int level) {
        SceneManager.LoadScene (level);
    }
    // Start is called before the first frame update
    void Start () {
        // scenes = EditorBuildSettings.scenes;
        // Debug.Log ("All Scenes : Length : " + allScenes.Length);
        // Scenes = SceneManager.GetScenesInBuildSettings ();
        // Debug.Log ("Amount of Scenes: " + scenes.Length);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown ("space")) {
            Debug.Log ("Scene loading: " + level);
            level++;
            LoadScene (2);

        }
    }

    void OnTriggerEnter2D (Collider2D col) {
        if(col.tag == "Player") {
            print (gameObject.name + " in contact with " + col.GetComponent<Collider2D>().name);
            Debug.Log("Scene loading: " + level);
            level++;
            LoadScene(2);
        }
    } //END FUNCTION ONCOLLISIONENTER

    void OnCollisionExit2D (Collision2D col) {
        print (gameObject.name + " No longer in contact with " + col.collider.name);
    }
}