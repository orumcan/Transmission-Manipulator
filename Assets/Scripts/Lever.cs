using UnityEngine;

public class Lever : MonoBehaviour 
{
    [SerializeField]
    private Animator doorAnimator;
    [SerializeField]
    private bool shouldStay;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.player || CheckCubeness(other.tag))
        {
            doorAnimator.SetBool("isTriggered", true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (shouldStay)
        {
            if (other.tag == Tags.player || CheckCubeness(other.tag))
            {
                doorAnimator.SetBool("isTriggered", false);
            }
        }
    }

    private bool CheckCubeness(string tag)
    {
        if ((tag.Equals(Tags.greenCube)) || tag.Equals(Tags.redCube) || tag.Equals(Tags.blueCube))
        {
            return true;
        }
        return false;
    }
}
