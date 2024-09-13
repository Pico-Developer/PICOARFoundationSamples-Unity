using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour
{
    float time = 5f;
    int sceneId = -1;

    // Start is called before the first frame update
    void Start()
    {
        sceneId = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time < 0)
        {
            SceneManager.LoadScene(sceneId == 0 ? 1 : 0);
            time = 5f;
        }

    }
}
