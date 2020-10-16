using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    public float vel = 2.4f;
    Rigidbody2D rb;
    public Transform Player;
    Transform inimigoLocal;
    bool face;
    public Animator anima;
    bool andar = false;
    public float distancia;
    //ataque
    float proximoataque;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inimigoLocal = GetComponent<Transform>();
        anima = GetComponent<Animator>();
    }

    void Update()
    {
        distancia = Vector2.Distance(this.inimigoLocal.position, Player.position);

        if ((Player.transform.position.x > this.inimigoLocal.position.x) && !face)
        {
            Flip();
        }
        else if ((Player.transform.position.x < this.inimigoLocal.position.x) && face)
        {
            Flip();
        }

        Bater();

    }
    private void FixedUpdate()
    {
        if ((andar) && distancia > 2.8f)
        {
            if (Player.transform.position.x < this.inimigoLocal.position.x)
            {
                rb.velocity = new Vector2(-vel, rb.velocity.y);
            }
            if (Player.transform.position.x > this.inimigoLocal.position.x)
            {
                rb.velocity = new Vector2(vel, rb.velocity.y);
            }

        }

    }
    void Flip()
    {
        face = !face;

        Vector3 scala = this.inimigoLocal.localScale;
        scala.x *= -1;
        this.inimigoLocal.localScale = scala;
    }
    private void OnTriggerEnter2D(Collider2D outro)
    {
        if (outro.gameObject.CompareTag("Player"))
        {
            andar = true;
        }
    }

    private void OnTriggerExit2D(Collider2D outro)
    {
        if (outro.gameObject.CompareTag("Player"))
        {
            andar = false;
        }
    }

    void Bater()
    {
        if (distancia <= 2 && Time.time > proximoataque)
        {
            proximoataque = Time.time + 1;
        }
    }



}

