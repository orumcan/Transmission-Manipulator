using UnityEngine;

public class Lever : MonoBehaviour 
{
    [SerializeField]
    private Animator doorAnimator;
    [SerializeField]
    private bool shouldStay;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.player || other.tag == Tags.cube)
        {
            doorAnimator.SetBool("isTriggered", true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (shouldStay)
        {
            if (other.tag == Tags.player || other.tag == Tags.cube)
            {
                doorAnimator.SetBool("isTriggered", false);
            }
        }
    }
}
