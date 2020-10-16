using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Damageable : MonoBehaviour
{
    public int maxHealth; //variavel de vida maxima 
    public int currentHealth;// variavel de vida atual

    public float invicibleTime;// variavel de tempo de invencibilidade

    public UnityEvent OnDamage, OnFinishDamage, OnDeath;// variavel do tipo evento para criar as funções seguintes

    private bool canTakeDamage = true;// variavel que identifica se é possivel atacar ou não

    private SpriteRenderer spriterenderer;// variavel referencia ao spriterenderer
    private Color defaultColor;//variavel do tipo Cor para ser implementado ao sistema de levar dano
    protected void Start()//definindo valores iniciais
    {
        currentHealth = maxHealth;
        spriterenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriterenderer.color;
    }

    public void TakeDamage(int amount)//função de receber dano e morrer 
    {
        if (!canTakeDamage)
            return;

        canTakeDamage = false;
        currentHealth -= amount;
        OnDamage.Invoke();
        StartCoroutine(TakingDamage());
        if(currentHealth <= 0)
        {
            OnDeath.Invoke();
            Death();
        }
    }

    IEnumerator TakingDamage()// sistema pro delay ao receber dano, intervalo entre os danos recebidos
    {
        float timer = 0;
        while(timer < invicibleTime)
        {
            spriterenderer.color = Color.clear;
            yield return new WaitForSeconds(0.05f);
            spriterenderer.color = defaultColor;
            yield return new WaitForSeconds(0.05f);
            timer += 0.1f;
        }

        spriterenderer.color = defaultColor;
        canTakeDamage = true;
        OnFinishDamage.Invoke();
    }

    public abstract void Death();

    public void Respawn()
    {
        Debug.Log("Respawn");
        currentHealth = maxHealth;
        canTakeDamage = true;
        TakeDamage(0);
    }
}
