using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightPunch : MonoBehaviour
{
    public enum Type { Melee };
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
        if(type == Type.Melee)
        {
            StopCoroutine("punch");
            StartCoroutine("punch");
        }
    }

    //leftPunch �� �޸� collider�� trailer�� Ű�� ����.
    IEnumerator punch()
    {
        yield return new WaitForSeconds(0.133f); // 8프레임~12프레임

        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.2f); //12프레임~18프레임
        meleeArea.enabled = true;

        yield return new WaitForSeconds(0.1f); //18프레임~21프레임
        meleeArea.enabled = false;

        yield return new WaitForSeconds(0.133f); //21프레임~25프레임
        trailEffect.enabled = false;
    }
}
