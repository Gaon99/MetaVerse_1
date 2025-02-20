using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSelection : MonoBehaviour
{
    public string Scenename;

    public void LoadMiniGame()
    {
        if (Scenename == "StackScene")
        {
            SetDisplay900();
        }
        else
        {
            SetDisplay1600();
        }
        SceneManager.LoadScene(Scenename);
    }

    private void SetDisplay900()
    {
        Screen.SetResolution(1080, 1920, false);
    }
    private void SetDisplay1600()
    {
        Screen.SetResolution(1920, 1080, false);
    }
}