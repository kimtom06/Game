using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayButton : MonoBehaviour
{
public void Play()
    {
        SceneManager.LoadScene("Sesion");
    }
    public void GoToMain()
    {
        SceneManager.LoadScene("Main");
    }
}
