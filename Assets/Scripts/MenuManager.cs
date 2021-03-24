
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Onclick_Start()
    {
        SceneManager.LoadScene(1);
    }
    public void Onclick_Exit()
    {
        Application.Quit(0);
    }
}
