using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShoot : MonoBehaviour {
    public float weaponRange = 50f;
    public Transform gunEnd;
    private Camera fpsCam;

	void Start ()
    {
        fpsCam = GetComponentInParent<Camera>();
	}
	
	void Update ()
    {
        if (Input.GetButtonDown("Fire1"))
        {       
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
            {
                if (hit.transform.tag == "Shootable")
                {
                    Debug.Log("I'm the quick, you're the dead.");
                    
                }
            }

        }


    }
}
