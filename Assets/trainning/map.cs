using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class map : MonoBehaviour
{
    public GameObject canvasObject;
    public GameObject indicator;
    public GameObject Menubackground;

    public GameObject explanation1;
    public GameObject explanation2;

    // Start is called before the first frame update
    void Start()
    {
        canvasObject.SetActive(false);
        indicator.SetActive(false);
        Menubackground.SetActive(false);
        explanation1.SetActive(false);
        explanation2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canvasObject.SetActive(!canvasObject.activeSelf);
            indicator.SetActive(!indicator.activeSelf);
            Menubackground.SetActive(!Menubackground.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            explanation1.SetActive(!explanation1.activeSelf);
            explanation2.SetActive(!explanation2.activeSelf);

        }
    }
}