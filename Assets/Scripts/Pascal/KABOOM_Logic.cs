using UnityEngine;

public class KABOOM_Logic : MonoBehaviour
{
    Rigidbody temp;
    Vector3 originPos;
    [SerializeField] float powerHEHE = 2000;
    [SerializeField] GameObject buttonTop;
    [SerializeField] float buttonTopTravelDistance = 0.09802601f;
    [SerializeField] float buttonTopTravelDuration = 0.1f;
    private void OnTriggerEnter(Collider other)
    {
        temp = other.gameObject.GetComponent<Rigidbody>();
        MachScharf();
    }
    private void OnTriggerExit(Collider other)
    {
        MachBoom();
    }
    private void MachScharf()
    {
        originPos = buttonTop.transform.position;

        Vector3 newPos = originPos + Vector3.down * buttonTopTravelDistance;

        TransformTransitionSystem.Instance.TransitionPos(buttonTop, newPos, buttonTopTravelDuration);

    }
    private void MachBoom()
    {
        
        temp.AddExplosionForce(powerHEHE, transform.position, 10f);

        buttonTop.transform.position = originPos;
    }
}
