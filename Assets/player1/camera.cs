using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;

    private Vector3 originalPosition;
    private float shakeIntensity;
    private float shakeDuration;




    // Start is called before the first frame update
    void Start()
    {
        transform.position = target.position + offset;

    }

    // Update is called once per frame
    void Update()
    {
           /* transform.position = target.position + offset;*/

    }

    public void punched()
    {
            StopCoroutine(ShakeCameraPunched());
            StartCoroutine(ShakeCameraPunched());
    }

    public void kicked()
    {
        StopCoroutine(ShakeCameraKicked());
        StartCoroutine(ShakeCameraKicked());
    }

    public void uppered()
    {
        StopCoroutine(ShakeCameraUppered());
        StartCoroutine(ShakeCameraUppered());
    }

    IEnumerator ShakeCameraPunched()
    {
        float elapsedTime = 0f;

        shakeIntensity = 0.1f;
        shakeDuration = 0.25f;

        originalPosition = transform.position;

        while (elapsedTime < shakeDuration)
        {
            // ������ ��ġ�� ī�޶� �̵��Ͽ� ��鸲 ȿ���� ����ϴ�.
            transform.position = originalPosition + Random.insideUnitSphere * shakeIntensity;

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // ��鸲�� ������ ���� ��ġ�� �����մϴ�.
        transform.position = target.position + offset;
    }

    IEnumerator ShakeCameraKicked()
    {
        Debug.Log("punched");
        float elapsedTime = 0f;
        shakeIntensity = 0.2f;
        shakeDuration = 0.4f;

        originalPosition = transform.position;

        while (elapsedTime < shakeDuration)
        {
            // ������ ��ġ�� ī�޶� �̵��Ͽ� ��鸲 ȿ���� ����ϴ�.
            transform.position = originalPosition + Random.insideUnitSphere * shakeIntensity;

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // ��鸲�� ������ ���� ��ġ�� �����մϴ�.
        transform.position = target.position + offset;
    }

    IEnumerator ShakeCameraUppered()
    {
        Debug.Log("punched");
        float elapsedTime = 0f;
        shakeIntensity = 0.3f;
        shakeDuration = 0.6f;

        originalPosition = transform.position;

        while (elapsedTime < shakeDuration)
        {
            // ������ ��ġ�� ī�޶� �̵��Ͽ� ��鸲 ȿ���� ����ϴ�.
            transform.position = originalPosition + Random.insideUnitSphere * shakeIntensity;

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // ��鸲�� ������ ���� ��ġ�� �����մϴ�.
        transform.position = target.position + offset;
    }
}
