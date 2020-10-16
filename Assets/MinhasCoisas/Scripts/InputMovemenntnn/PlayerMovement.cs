using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;// velocidade do personagem
    
    public float JumpForce = 3;// velocidade de pulo
    public bool isJumping; // verifica se está pulando
    public float jumpHoldDuration = 0.15f; //variavel padrão limite de pulo
    private float jumpTime;//Utilizado para fazer referencia ao tempo real e limitar o pulo
    public float jumpHoldForce = 1;//potencia limite para o pulo com o botão  pressionado
    public bool isOnGround;//variavel q verifica se o jogador ta no chão

    public float coyoteDuration = 0.1f;//variavel da duraçao do pulo do coyote
    private float coyoteTime;//variavel auxiliar ao sistema de pulo do coyote

    public float leftFootOffset = -0.3f;//configuração de posição do Raycast
    public float rightFootOffset = 0.3f;//configuração de posição do Raycast
    public float groundOffset = 0.5f;//configuração de posição do Raycast
    public float groundDistance = 0.22f;//configuração de posição do Raycast
    public LayerMask groundLayer;//Layer alvo a ser tocado pelo raycast
    
    [HideInInspector]
    public int direction = -1;// variavel utilizada para mudar a direção do jogador 
    public bool jumpHeld;//variavel do botão de pulo pressionado
    private bool jumpStarted;//verifica se o personagem pulou
    public float horizontal;// variavel pra pegar o eixo horizontal


    private Rigidbody2D rb; // variavel pra acessar o ridbody 2d
    private Animator animator;//variavel referencia ao sistema do animator
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // pegando o valor do rigdbody do personagem caso n tenha pego
        animator = GetComponent<Animator>();//recebe o componente animator caso nao exista no personagem
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() // instancia a função para funcionar no jogo
    {
        GroundMovement();
        AirMovement();
        PhysicsCheck();
    }

    void PhysicsCheck()// função do raycast q interage com o solo para complementar no sistema de pulo, parte do sistema que impede de pular infinito
    {
        isOnGround = false;

        RaycastHit2D leftFoot = RayCast(new Vector2(leftFootOffset, -groundOffset), Vector2.down, groundDistance, groundLayer);
        RaycastHit2D rightFoot = RayCast(new Vector2(rightFootOffset, -groundOffset), Vector2.down, groundDistance, groundLayer);

        if(leftFoot || rightFoot)
        {
            isOnGround = true;
        }

        animator.SetBool("OnGround", isOnGround);
    }

    void GroundMovement() // função para adicionar movimento ao personagem e gira-lo
    {
        float xVelocity = speed * horizontal;

        rb.velocity = new Vector2(xVelocity, rb.velocity.y);

        if(direction * xVelocity > 0)
        {
            Flip();
        }

        if (isOnGround)
        {
            coyoteTime = Time.time + coyoteDuration;
        }

        animator.SetFloat("Speed", Mathf.Abs(xVelocity));
    }

    void AirMovement() // função que verifica se está pulando
    {

        if (jumpStarted && (isOnGround || coyoteTime > Time.time))
        {
            isJumping = true;
            jumpStarted = false;

            rb.velocity = Vector2.zero;

            rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);

            jumpTime = Time.time + jumpHoldDuration;

            coyoteTime = Time.time;
        }
        if (isJumping)
        {
            if (jumpHeld)
            {
                rb.AddForce(Vector2.up * jumpHoldForce, ForceMode2D.Impulse);
            }

            if(jumpTime <= Time.time)
            {
                isJumping = false;
            }
        }

        jumpStarted = false;
    }

    void Flip() //função pra inverter a direção do player
    {
        direction *= -1;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void Move(InputAction.CallbackContext ctx) // função referencia para o inputactions, altera o valor de movimento do input
    {
        horizontal = ctx.ReadValue<float>();
    }

    public void Jump(InputAction.CallbackContext ctx) // função referencia para o inputactions, altera valores das variaveis seguintes complementando a função AirMovement 
    {
        if (ctx.started)
        {
            jumpStarted = true;
        }
        jumpHeld = ctx.performed;
    }

    RaycastHit2D RayCast(Vector2 offset, Vector2 rayDirection, float length, LayerMask layerMask)// Verifica se o player ta colidindo com o chão
    {
        Vector2 pos = transform.position;

        RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDirection, length, layerMask);

        Color color = hit ? Color.red : Color.green;

        Debug.DrawRay(pos + offset, rayDirection * length, color);

        return hit;
    }
}
