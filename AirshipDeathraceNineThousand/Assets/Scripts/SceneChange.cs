using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public int sceneToChangeTo = 0;

    public void changeScene()
    {
        SceneManager.LoadScene(sceneToChangeTo);
    }
}
