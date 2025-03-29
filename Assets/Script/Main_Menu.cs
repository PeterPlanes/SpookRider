using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    public void B_Start()
    {
        SceneManager.LoadScene("Level1");
    }
    public void B_Credits()
    {
        SceneManager.LoadScene("Level1Credit");
    }
    public void B_Quit()
    {
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
