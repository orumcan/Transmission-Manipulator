using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject gun;
    public Transform gunEnd;
    public float maxRayDistance;
    public Material gunMaterial;
    public Text weaponType;

    private Dictionary<TransmissionType, bool> transmissionDict;
    public TransmissionType selectedTransmissionType = TransmissionType.None;
    private Array transmissionTypes;

    private bool isWeaponAcquired = false;
    private bool isSelected = false;

    RaycastHit hit;

    private void OnEnable()
    {
        Gun.OnGunPicked += TransmissionAcquired;
    }

    void Start()
    {
        transmissionDict = new Dictionary<TransmissionType, bool>();
        transmissionTypes = Enum.GetValues(typeof(TransmissionType));
        foreach (TransmissionType type in transmissionTypes)
        {
            transmissionDict.Add(type, false);
        }
    }

    private void TransmissionAcquired(TransmissionType type)
    {
        gun.SetActive(true);
        isWeaponAcquired = true;
        transmissionDict[type] = true;
        selectedTransmissionType = type;
        ChangeGunColor();
        Debug.Log("Transmission " + type + " " + transmissionDict[type]);
        weaponType.text = type.ToString();
    }

    void Update()
    {
        Interact();
        ChangeTransmission();

        if (isSelected)
        {
            hit.transform.position = gunEnd.position;
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
                        weaponType.text = type.ToString();
                        ChangeGunColor();
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
                                    weaponType.text = type2.ToString();
                                    ChangeGunColor();
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
                        weaponType.text = type2.ToString();
                        ChangeGunColor();
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
                        weaponType.text = type.ToString();
                        ChangeGunColor();
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
                                    weaponType.text = type2.ToString();
                                    ChangeGunColor();
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
                        weaponType.text = type2.ToString();
                        ChangeGunColor();
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

    private void ChangeGunColor()
    {
        if (selectedTransmissionType == TransmissionType.Magnetic)
        {
            gunMaterial.color = Color.green;
            weaponType.color = Color.green;
        }
        else if (selectedTransmissionType == TransmissionType.Heating)
        {
            gunMaterial.color = Color.red;
            weaponType.color = Color.red;
        }
        else if (selectedTransmissionType == TransmissionType.Cooling)
        {
            gunMaterial.color = Color.blue;
            weaponType.color = Color.blue;
        }
    }

    private void Interact()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
                if (hit.rigidbody != null)
                {
                    if (hit.transform.tag == Tags.greenCube && selectedTransmissionType == TransmissionType.Magnetic)
                    {
                        if (hit.distance < maxRayDistance)
                        {
                            Debug.Log("I'm the quick, you're the dead.");
                            isSelected = true;
                        }
                    }
                    else if (hit.transform.tag == Tags.redCube && selectedTransmissionType == TransmissionType.Heating)
                    {
                        hit.transform.localScale = new Vector3(4,4,4);
                    }
                    else if (hit.transform.tag == Tags.blueCube && selectedTransmissionType == TransmissionType.Cooling)
                    {
                        hit.transform.localScale = new Vector3(2,2,2);
                    }
                }
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            isSelected = false;
        }
    }
}
