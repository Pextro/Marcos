using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    public int damage;// variavel de dano
    public bool destroyOnDamage;// varaivel que verifica se está aplicando o dano

    void DoDamage(Damageable damageable)// função que causa dano ao adversario e em seguida se destroi
    {
        damageable.TakeDamage(damage);
        if (destroyOnDamage)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)// Função de dano aplicada ao colidir em outro objeto
    {
        Damageable damageable = other.gameObject.GetComponent<Damageable>();
        if(damageable != null)
        {
            DoDamage(damageable);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)// Função de dano aplicada ao atravessar um objeto
    {
        Damageable damageable = other.GetComponent<Damageable>();
        if (damageable != null)
        {
            DoDamage(damageable);
        }
    }
}
