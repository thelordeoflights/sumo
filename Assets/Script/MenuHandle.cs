using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MenuHandle : MonoBehaviour
{
    public GameProperties gameProperties;
    public void Startgame()
    {
        SceneManager.LoadScene(1);
        gameProperties.isAlive = true;
    }
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameProperties.isAlive = true;
    }
    public void Returntomenu()
    {
        SceneManager.LoadScene(0);
    }

}
