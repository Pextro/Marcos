using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image healthBar;// variavel do tipo imagem 

    public static UIManager instance;

    private void Awake()
    {
        instance = this;
    }
    public void UpdateHealthBar(float health)//função que mostra a barra de vida igual a vida atual do jogador
    {
        healthBar.fillAmount = health / 10;
    }
}
