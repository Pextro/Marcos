using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : MonoBehaviour
{
    public Key key;//
    public Sprite doorOpen;

    private SpriteRenderer sprite;
    private BoxCollider2D boxcollider;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        boxcollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Iventario.inventario.checkKey(key))
            {
                sprite.sprite = doorOpen;
                boxcollider.enabled = false;
                Destroy(gameObject);
            }
        }
    }
}
