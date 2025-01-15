using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTest : MonoBehaviour
{
    public void OnStartButton()
    {
        SceneManager.LoadScene(1);
    }
    public void OnMenuButton()
    {
        SceneManager.LoadScene(0);
    }
}
