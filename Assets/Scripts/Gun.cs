using UnityEngine;

public class Gun : MonoBehaviour 
{
    [SerializeField]
    private TransmissionType transmissionType;

    public delegate void TransmissionEvent(TransmissionType type);
    public static event TransmissionEvent OnGunPicked;


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.player)
        {
            if (OnGunPicked != null)
            {
                OnGunPicked(transmissionType);
                Destroy(gameObject);
            }
        }
    }
    
}
