    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class underKick : MonoBehaviour
{
    public enum Type { Foot };
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
        if (type == Type.Foot)
        {
            StopCoroutine("kick");
            StartCoroutine("kick");
        }
    }

    IEnumerator kick()
    {
        yield return new WaitForSeconds(0.1f); // 8프레임~14프레임
        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.167f); // 14프레임~16프레임 
        meleeArea.enabled = true;

        yield return new WaitForSeconds(0.133f); // 16프레임~20프레임
        meleeArea.enabled = false;

        yield return new WaitForSeconds(0.333f); // 20프레임~24프레임
        trailEffect.enabled = false;

       /* yield return new WaitForSeconds(0.44f);*/
    }

}
