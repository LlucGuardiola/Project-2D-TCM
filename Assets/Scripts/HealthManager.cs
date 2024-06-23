using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] float vidas;
    [SerializeField] public float vidas_Boss;
    [SerializeField] Slider sliderJugador;
    [SerializeField] Slider sliderEnemigo;
    
  
    private GameObject player;
    public int LifeCounter;
    public bool isBoss = false;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        sliderJugador.maxValue = vidas;
        sliderJugador.value = sliderJugador.maxValue;

        sliderEnemigo.maxValue = vidas_Boss;
        sliderEnemigo.value = sliderEnemigo.maxValue;
    }

    public void LoseLife(float damageDealt, bool canGetDamaged)
    {
        if (canGetDamaged)
        {
            vidas -= damageDealt;
            sliderJugador.value = vidas;

            if (vidas <= 0)
            {
                    player.GetComponent<ManageRespawn>().HasToRespawn = true;
                    vidas = sliderJugador.maxValue;
                    sliderJugador.value = vidas;
                    LifeCounter++;
                    GameFlowController.Instance.SetCounterLife(LifeCounter);
            }
        }
    }
    public void LoseLifeBoss(float damageDealt, bool canGetDamaged)
    {
        if (canGetDamaged)
        {
            vidas_Boss -= damageDealt;
            sliderEnemigo.value = vidas_Boss;  
        }
    }
    public void ShowHealthBar()
    {
            sliderEnemigo.gameObject.SetActive(true);
    }

    public void HideHealthBar()
    {
        if (isBoss)
        {
            sliderEnemigo.gameObject.SetActive(false);
        }
    }
    public void Dead()
    {
            Destroy(gameObject);
    }
}
