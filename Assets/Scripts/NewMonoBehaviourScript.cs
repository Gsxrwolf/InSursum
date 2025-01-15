using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public GameObject player;
    public GameObject startPoint;
    private void OnTriggerEnter(Collider other)
    {
        player.transform.position = startPoint.transform.position;
    }
}
