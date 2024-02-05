using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leftPunch : MonoBehaviour
{
    public enum Type { Melee };
    public Type type;
    public int damage;
    public float rate;
    public BoxCollider meleeArea;
    public TrailRenderer trailEffect; 

    void Awake()
    {
        damage = 2;
    }
    public void Use()
    {
        if(type == Type.Melee)
        {
            StopCoroutine("punch");
            StartCoroutine("punch");
        }
    }

    //leftPunch �� �޸� collider�� trailer�� Ű�� ����.
    IEnumerator punch()
    {
        yield return new WaitForSeconds(0.1f);

        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.122f);
        meleeArea.enabled = true;

        yield return new WaitForSeconds(0.0777f);
        meleeArea.enabled = false;

        yield return new WaitForSeconds(0.122f);
        trailEffect.enabled = false;
    }
}
