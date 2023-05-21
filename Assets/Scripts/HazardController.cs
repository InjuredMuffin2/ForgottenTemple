using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardController : MonoBehaviour
{
    public int HazardHealthChange;
    void Start()
    {
        if (HazardHealthChange == 0)
        {
            HazardHealthChange = -1;
        }
    }
}
