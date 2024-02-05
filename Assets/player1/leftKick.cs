using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leftKick : MonoBehaviour
{
    public enum Type { Foot };
    public Type type;
    public int damage;
    public float rate;
    public BoxCollider meleeArea;
    public TrailRenderer trailEffect;

    void Awake()
    {
        damage = 3;
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
        yield return new WaitForSeconds(0.267f); // 0프레임~10프레임, 1.25배속
        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.133f); // 10프레임~15프레임, 1.25배속       
        meleeArea.enabled = true;

        yield return new WaitForSeconds(0.16f); // 15프레임~21프레임, 1.25배속
        meleeArea.enabled = false;

        yield return new WaitForSeconds(0.16f); // 21프레임~27프레임, 1.25배속
        trailEffect.enabled = false;

       /* yield return new WaitForSeconds(0.44f);*/
    }

}
