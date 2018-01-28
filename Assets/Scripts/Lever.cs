using UnityEngine;

public class Lever : MonoBehaviour 
{
    [SerializeField]
    private Animator doorAnimator;
    [SerializeField]
    private bool shouldStay;
    [SerializeField]
    private Lever[] requiredLevers;

    private bool isTriggered = false;


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.player || CheckCubeness(other.tag))
        {
            isTriggered = true;
            if (requiredLevers.Length == 0)
            {
                doorAnimator.SetBool("isTriggered", true);
            }
            else
            {
                int activeLeverCount = 0;
                for (int i = 0; i < requiredLevers.Length; i++)
                {
                    if (requiredLevers[i].IsTriggered())
                    {
                        activeLeverCount++;
                        if (activeLeverCount >= requiredLevers.Length)
                        {
                            doorAnimator.SetBool("isTriggered", true);
                        }
                    }
                }
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (shouldStay)
        {
            if (other.tag == Tags.player || CheckCubeness(other.tag))
            {
                doorAnimator.SetBool("isTriggered", false);
                isTriggered = false;
            }
        }
        else
        {
            if (requiredLevers.Length != 0)
            {
                if (other.tag == Tags.player || CheckCubeness(other.tag))
                {
                    isTriggered = false;
                }
            }
        }
    }

    private bool CheckCubeness(string tag)
    {
        if (tag.Equals(Tags.greenCube) || tag.Equals(Tags.redCube) || tag.Equals(Tags.blueCube))
        {
            return true;
        }
        return false;
    }

    public bool IsTriggered()
    {
        return isTriggered;
    }
}
