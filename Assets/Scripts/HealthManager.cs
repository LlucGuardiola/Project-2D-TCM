using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] float vidas;
    [SerializeField] Slider sliderVidas;

    private void Start()
    {
        sliderVidas.maxValue = vidas;
        sliderVidas.value = sliderVidas.maxValue;
    }
    public void LoseLife(float damageDealt)
    {
        vidas -= damageDealt;
        sliderVidas.value = vidas;

        if (vidas <= 0)
        {
            GetComponent<PlayerMovement>().HasToRespawn = true;
            vidas = sliderVidas.maxValue;
            sliderVidas.value = vidas;
        }
    }
}
