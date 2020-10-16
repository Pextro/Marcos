using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int vida;
    public string nome;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(nome))
        {
            vida = vida - 1;
        }
        if(vida <= 0)
        {
            Destroy(gameObject);
        }
    }
}
