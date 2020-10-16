 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    public Transform ShotSpawner;
    public Rigidbody2D shot;//variavel possuidora do prefab do tiro
    public Rigidbody2D ChargeFire;
    public float shotSpeed = 15;// variavel da velocidade do tiro
    public float fireRate = 0.15f;// variavel usada no calculo para a frequencia de intervalo pra cada tiro
    public float totalCharge = 3;// variavel do tamanho maximo aumentavel pelo tiro carregado
    public float totalChargeTime = 2;// variavel utilizada no calculo para o tiro carregado, o seu valor determina o tempo levado para se chegar ao tamanho maximo

    public float charging = 1;// variavel q armazena o tamanho atual da bala
    private float nextFire;// varaivel utilizada no sistema de delay entre os tiros
    private InputAction.CallbackContext shootPhase;// variavel usada como referencia entre esse scrípt e o input actions
    private PlayerMovement playerMovement;// variavel para fazer referencia entre os dois scripts, para mudar o valor de direçao da bala
    private Animator animator;// referencia para o sistema de animação
    void Start()//função de start para pegar os componentes que talvez não estejam no player
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    void Update()//sistema de carregamento do tiro
    {
        if (shootPhase.started)
        {
            charging += Time.deltaTime * ((totalCharge - 1) / totalChargeTime);
        }

        charging = Mathf.Clamp(charging, 1, totalCharge);
    }

    public void Onshoot(InputAction.CallbackContext ctx)//função do tiro, chama a funçao do input actions pra atirar
    {
        shootPhase = ctx;
        if (shootPhase.canceled)
        {
            Shoot();
        }
    }

    void Shoot()// função que instancia o prefab do tiro junto de um timer entre cada tiro
    {
        if (Time.time < nextFire)
            return;

        animator.SetTrigger("Shoot");

        nextFire = Time.time + fireRate;
        Rigidbody2D newChargeFire = Instantiate(ChargeFire, ShotSpawner.position, Quaternion.identity);
        Rigidbody2D newShot = Instantiate(shot, ShotSpawner.position, Quaternion.identity);       
        newShot.velocity = Vector2.left * shotSpeed * playerMovement.direction;

        newShot.transform.localScale *= charging;
        charging = 1;
    }

}
