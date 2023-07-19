using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Levelbutton : MonoBehaviour
{
    public int sceneIndex;

    public void OpenScene()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
