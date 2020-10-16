using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)//função que guarda o valor de sua unidade dentro da variavel instance
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LevelManager.instance.SetcheckPoint(this);
        }
    }
}
