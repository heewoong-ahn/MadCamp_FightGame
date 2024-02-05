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
            // 랜덤한 위치로 카메라를 이동하여 흔들림 효과를 만듭니다.
            transform.position = originalPosition + Random.insideUnitSphere * shakeIntensity;

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // 흔들림이 끝나면 원래 위치로 복귀합니다.
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
            // 랜덤한 위치로 카메라를 이동하여 흔들림 효과를 만듭니다.
            transform.position = originalPosition + Random.insideUnitSphere * shakeIntensity;

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // 흔들림이 끝나면 원래 위치로 복귀합니다.
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
            // 랜덤한 위치로 카메라를 이동하여 흔들림 효과를 만듭니다.
            transform.position = originalPosition + Random.insideUnitSphere * shakeIntensity;

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // 흔들림이 끝나면 원래 위치로 복귀합니다.
        transform.position = target.position + offset;
    }
}
