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

    private void Start()
    {
        sliderVidas.maxValue = vidas;
        sliderVidas.value = sliderVidas.maxValue;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            LoseLife(20,true);
        }
        Debug.Log(vidas);
    }
    public void LoseLife(float damageDealt,bool canGetDamaged)
    {
        if (canGetDamaged)
        {
            vidas -= damageDealt;
            sliderVidas.value = vidas;

            if (vidas <= 0)
            {
                GetComponent<ManageRespawn>().HasToRespawn = true;
                vidas = sliderVidas.maxValue;
                sliderVidas.value = vidas;

                LifeCounter++;

                //GameFlowController.Instance.SetCounterLife(LifeCounter); /////activar en la entrega
            }
        }
    }
}
