using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlashlightAdvanced : MonoBehaviour
{
    public Light light1;
    public TMP_Text text;

    public TMP_Text batteryText;

    public float lifetime = 100;

    public float batteries = 0;

    public AudioSource flashON;
    public AudioSource flashOFF;

    private bool on;
    private bool off;


    void Start()
    {
        light1 = GetComponent<Light>();

        off = true;
        light1.enabled = false;

    }



    void Update()
    {
        text.text = lifetime.ToString("0") + "%";
        batteryText.text = batteries.ToString();

        if (Input.GetButtonDown("flashlight") && off) {
            flashON.Play();
            light1.enabled = true;
            on = true;
            off = false;
        }

        else if (Input.GetButtonDown("flashlight") && on) {
            //flashOFF.Play();
            light1.enabled = false;
            on = false;
            off = true;
        }

        if (on) {
            lifetime -= 0.5f * Time.deltaTime;
        }

        if (lifetime <= 0) {
            light1.enabled = false;
            on = false;
            off = true;
            lifetime = 0;
        }

        if (lifetime > 100) {
            lifetime = 100;
        }

        if (Input.GetButtonDown("reload") && batteries >= 1) {
            batteries -= 1;
            lifetime += 50;
        }

        if (Input.GetButtonDown("reload") && batteries == 0) {
            return;
        }

        if (batteries < 0) {
            batteries = 0;
        }
    }

}