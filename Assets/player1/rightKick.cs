using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightKick : MonoBehaviour
{
    public enum Type { Foot };
    public Type type;
    public int damage;
    public float rate;
    public BoxCollider meleeArea;
    public TrailRenderer trailEffect;

    void Awake()
    {
        damage = 5;
    }
    
    public void Use()
    {
        if (type == Type.Foot)
        {
            StopCoroutine("kick");
            StartCoroutine("kick");
        }
    }

    //leftPunch �� �޸� collider�� trailer�� Ű�� ����.
    IEnumerator kick()
    {
        yield return new WaitForSeconds(0.267f);
        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.173f);        
        meleeArea.enabled = true;

        yield return new WaitForSeconds(0.111f);
        meleeArea.enabled = false;

        yield return new WaitForSeconds(0.12f);
        trailEffect.enabled = false;

       /* yield return new WaitForSeconds(0.44f);*/
    }

}
