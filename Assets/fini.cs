using UnityEngine;
using UnityEngine.SceneManagement;

public class fini : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(2);
    }
}
