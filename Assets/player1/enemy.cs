using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public int maxHealth;
    public int curHealth;

    Rigidbody rigid;
    BoxCollider boxCollider; 

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Melee")
        {
            leftPunch leftPunch = other.GetComponent<leftPunch>();
            curHealth -= leftPunch.damage;

            Debug.Log("Melee : " + curHealth); 
        }
    }
}
