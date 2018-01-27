using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float weaponRange = 50f;
    public Transform gunEnd;
    private Camera fpsCam;

    private Dictionary<TransmissionType, bool> transmissionDict;
    public TransmissionType selectedTransmissionType = TransmissionType.None;
    private Array transmissionTypes;

    private bool isWeaponAcquired = false;

    void Start()
    {
        fpsCam = GetComponentInParent<Camera>();
        transmissionDict = new Dictionary<TransmissionType, bool>();
        transmissionTypes = Enum.GetValues(typeof(TransmissionType));
        foreach (TransmissionType type in transmissionTypes)
        {
            transmissionDict.Add(type, false);
        }
    }    
   
    private void TransmissionAcquired(TransmissionType type)
    {
        transmissionDict[type] = true;
        selectedTransmissionType = type;
        Debug.Log("Transmission " + type + " " + transmissionDict[type]);
    }

    void Update()
    {
        Interact();
        ChangeTransmission();
        OnTestButtonPressed();
    }

    public void OnTestButtonPressed()
    {
        if (Input.GetButtonDown("TypeChange"))
        {
            TransmissionAcquired(TransmissionType.Heating);
            TransmissionAcquired(TransmissionType.Vibration);
            TransmissionAcquired(TransmissionType.Cooling);
            TransmissionAcquired(TransmissionType.Levitation);
            isWeaponAcquired = true;
        }
    }

    private void ChangeTransmission()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && isWeaponAcquired)
        {
            if ((int)selectedTransmissionType < transmissionTypes.Length - 1)
            {
                for (int i = (int)selectedTransmissionType + 1; i <= transmissionTypes.Length - 1; i++)
                {
                    TransmissionType type = (TransmissionType)transmissionTypes.GetValue(i);
                    Debug.Log(type + " " + transmissionDict[type]);

                    if (transmissionDict[type] == true)
                    {
                        selectedTransmissionType = type;
                        break;
                    }
                    else
                    {
                        if (i >= transmissionDict.Count - 1)
                        {
                            for (int j = 1; j < transmissionTypes.Length - 1; j++)
                            {
                                TransmissionType type2 = (TransmissionType)transmissionTypes.GetValue(j);
                                if (transmissionDict[type2] == true)
                                {
                                    selectedTransmissionType = type2;
                                    break;
                                }
                                else
                                {
                                    selectedTransmissionType++;
                                }
                            }
                        }
                        else
                        {
                            selectedTransmissionType++;
                        }
                    }
                }
            }
            else
            {
                for (int j = 1; j < transmissionTypes.Length - 1; j++)
                {
                    TransmissionType type2 = (TransmissionType)transmissionTypes.GetValue(j);
                    if (transmissionDict[type2] == true)
                    {
                        selectedTransmissionType = type2;
                        break;
                    }
                    else
                    {
                        selectedTransmissionType++;
                    }
                }
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && isWeaponAcquired)
        {
            if ((int)selectedTransmissionType > 1)
            {
                for (int i = (int)selectedTransmissionType - 1; i >= 1; i--)
                {
                    TransmissionType type = (TransmissionType)transmissionTypes.GetValue(i);
                    Debug.Log(type + " " + transmissionDict[type]);

                    if (transmissionDict[type] == true)
                    {
                        selectedTransmissionType = type;
                        break;
                    }
                    else
                    {
                        if (i <= 1)
                        {
                            for (int j = transmissionTypes.Length - 1; j > 0; j--)
                            {
                                TransmissionType type2 = (TransmissionType)transmissionTypes.GetValue(j);
                                if (transmissionDict[type2] == true)
                                {
                                    selectedTransmissionType = type2;
                                    break;
                                }
                                else
                                {
                                    selectedTransmissionType--;
                                }
                            }
                        }
                        else
                        {
                            selectedTransmissionType--;
                        }
                    }
                }

            }
            else
            {
                selectedTransmissionType = (TransmissionType)transmissionTypes.Length - 1;

                for (int j = transmissionTypes.Length - 1; j < 0; j--)
                {
                    TransmissionType type2 = (TransmissionType)transmissionTypes.GetValue(j);
                    if (transmissionDict[type2] == true)
                    {
                        selectedTransmissionType = type2;
                        break;
                    }
                    else
                    {
                        selectedTransmissionType--;
                    }
                }
            }
        }


    }

    private void Interact()
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
