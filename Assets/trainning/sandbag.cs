using System.Collections;
using UnityEngine;

public class sandbag : MonoBehaviour
{
    private bool isBeingHit = false; // ��鸲 ���¸� ��Ÿ���� �÷���
    public float shakeDuration = 0.5f; // ��鸲 ���� �ð�
    public float shakeIntensity = 0.1f; // ��鸲 ����

    public sounds soundControllerScript;
    public ParticleSystem blood;

    private Vector3 initialPosition = new Vector3(-0.8517556f, 1.49f, 6.88f); // �ʱ� ��ġ
    private float timer = 0.0f;

    private void Start()
    {
        blood.Stop();
    }

    private void Update()
    {
        timer += Time.deltaTime; // Ÿ�̸� ������Ʈ
        // Ÿ�̸Ӱ� checkInterval�� ������� Ȯ���ϰ� �̵� ó��
        if (timer>10.0f)
        {
            MoveToInitialPosition(); // �ʱ� ��ġ�� �̵�
            timer = 0.0f;
        }
    }

    private void MoveToInitialPosition()
    {
        Debug.Log("back");
        transform.position = initialPosition; // �ʱ� ��ġ�� �̵�
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Melee" || other.tag =="R_Melee")
        {

            shakeDuration = 0.25f;
            shakeIntensity = 0.1f;
            soundControllerScript.playPunched();
            StopCoroutine(Shake());
            StartCoroutine(Shake());


        }

        if (other.tag == "Foot" || other.tag == "L_Foot" || other.tag == "Knee")
        {
            shakeIntensity = 0.2f;
            shakeDuration = 0.4f;
            soundControllerScript.playKicked();
            StopCoroutine(Shake());
            StartCoroutine(Shake());


        }

        if (other.tag == "BigMelee")
        {

            shakeIntensity = 0.3f;
            shakeDuration = 0.6f;
            soundControllerScript.playUppered();
            StopCoroutine(Shake());
            StartCoroutine(Shake());


        }

    }

    // ��鸮�� �ڷ�ƾ
    private IEnumerator Shake()
    {
        isBeingHit = true;

        Vector3 originalPosition = transform.position;

        float elapsedTime = 0f;
        blood.Play();

        while (elapsedTime < shakeDuration)
        {
            // ������ ��鸲 ��ġ ����
            Vector3 shakeOffset = Random.insideUnitSphere * shakeIntensity;

            // ���� ��ġ�� ��鸲 ��ġ�� ���Ͽ� ����
            transform.position = originalPosition + shakeOffset;

            elapsedTime += Time.deltaTime;

            yield return null;
        }
        blood.Stop();

        // ��鸲�� ������ ���� ��ġ�� ����
        transform.position = originalPosition;

        isBeingHit = false;
    }
}