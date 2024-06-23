using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] float vidas;
    [SerializeField] Slider sliderVidas;
    public int LifeCounter;
    public bool isBoss = false;

    private void Start()
    {
        sliderVidas.maxValue = vidas;
        sliderVidas.value = sliderVidas.maxValue;

        if (isBoss)
        {
            sliderVidas.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            LoseLife(20, true);
        }
        Debug.Log(vidas);
    }

    public void LoseLife(float damageDealt, bool canGetDamaged)
    {
        if (canGetDamaged)
        {
            vidas -= damageDealt;
            sliderVidas.value = vidas;

            if (vidas <= 0)
            {
                if (!isBoss) 
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

    public float Vidas => vidas;

    public void ShowHealthBar()
    {
        if (isBoss)
        {
            sliderVidas.gameObject.SetActive(true);
        }
    }

    public void HideHealthBar()
    {
        if (isBoss)
        {
            sliderVidas.gameObject.SetActive(false);
        }
    }
}
