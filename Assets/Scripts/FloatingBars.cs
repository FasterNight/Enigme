using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingBars : MonoBehaviour
{
    [SerializeField]
    private Slider hpBar;
    [SerializeField]
    private Slider fireBar;
    [SerializeField]
    private Slider iceBar;
    [SerializeField]
    private GameObject camera;
    [SerializeField]
    private Transform target;

    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        hpBar.transform.rotation = camera.transform.rotation;
        fireBar.transform.rotation = camera.transform.rotation;
        iceBar.transform.rotation = camera.transform.rotation;
    }

    public void UpdateHPBar(float currentValue, float maxValue)
    {
        hpBar.value = currentValue / maxValue;
    }
    public void UpdateFireBar(float currentValue, float maxValue)
    {
        fireBar.value = currentValue / maxValue;
    }
    public void UpdateIceBar(float currentValue, float maxValue)
    {
        iceBar.value = currentValue / maxValue;
    }
}
