using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HealthUp : MonoBehaviour
{
    public int HealthAdd;
    void Start()
    {
        HealthAdd = 1;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerVariableSet.HealthPoints = PlayerVariableSet.HealthPoints + HealthAdd;
            GameObject.Destroy(this.gameObject);
        }
    }
}
