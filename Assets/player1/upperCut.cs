using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upperCut : MonoBehaviour
{
    public enum Type { Melee };
    public Type type;
    public int damage;
    public float rate;
    public BoxCollider meleeArea;
    public TrailRenderer trailEffect; 

    void Awake()
    {
        damage = 10;
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
        yield return new WaitForSeconds(0.433f); // 16프레임~29프레임

        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.1f); //29프레임~32프레임
        meleeArea.enabled = true;

        yield return new WaitForSeconds(0.067f); //32프레임~34프레임
        meleeArea.enabled = false;

        yield return new WaitForSeconds(0.1f); //34프레임~37프레임
        trailEffect.enabled = false;
    }
}
