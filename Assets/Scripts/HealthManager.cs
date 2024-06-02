using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] float vidas;
    [SerializeField] Slider sliderVidas;
    public int LifeCounter;
    public float seconds;
    bool estaRestandoVida;
    GameObject player;

    private void Start()
    {
        sliderVidas.maxValue = vidas;
        sliderVidas.value = sliderVidas.maxValue;
        seconds = 0;
        estaRestandoVida = false;
        player = GameObject.FindWithTag("Player");
    }
    private void Update()
    {
        seconds += Time.deltaTime;
        if (Input.GetKey(KeyCode.K) || estaRestandoVida)
        {
            DegbugLife();
        }
    }
    public void LoseLife(float damageDealt)
    {
        vidas -= damageDealt;
        sliderVidas.value = vidas;

        if (vidas <= 0)
        {
            GetComponent<ManageRespawn>().HasToRespawn = true;
            vidas = sliderVidas.maxValue;
            sliderVidas.value = vidas;

            LifeCounter++;
            GameFlowController.Instance.SetCounterLife(LifeCounter);
        }
    }
    public void DegbugLife()
    {
        estaRestandoVida = true;
        if (seconds >= 1)
        {
            Debug.Log(seconds);
            LoseLife(20);
            seconds = 0;
        }
    }
}
