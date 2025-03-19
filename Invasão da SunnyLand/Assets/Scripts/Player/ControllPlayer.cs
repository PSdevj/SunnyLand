using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllPlayer : MonoBehaviour
{
    public Rigidbody2D corpoPlayer;
    public float velocidadePlayer;

    private bool isGrounded = false; // Verifica se est� no ch�o
    private bool doubleJumpAvailable = true; // Controla se o pulo duplo pode ser usado

    public bool isDashing = false;
    private PlayerAbilities playerAbilities; // Refer�ncia ao script de habilidades
    public bool esquerda;

    public Animator animacaoPlayer; //controla a anima��o do player

    public ControllGame genJ; //acessar o script ContollGame

    // Start is called before the first frame update
    void Start()
    {
        corpoPlayer = GetComponent<Rigidbody2D>();
        playerAbilities = GetComponent<PlayerAbilities>();
        animacaoPlayer = GetComponent<Animator>();
        genJ = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControllGame>();
    }

    // Update is called once per frame
    void Update()
    {
    

        if (genJ.EstadoDoJogo() == true)
        {
            Movimentacao();
            Pular();

            // Verifica a dire��o do movimento e vira o player para a esquerda ou direita
            if (velocidadePlayer > 0)
            {
                // Vira para a direita
                transform.localScale = new Vector3(1, 1, 1);
                esquerda = false;
            }
            else if (velocidadePlayer < 0)
            {
                // Vira para a esquerda
                transform.localScale = new Vector3(-1, 1, 1);
                esquerda = true;
            }
        }
       
    }
    void FixedUpdate()
    {
        // Ajusta a posição do player para evitar que ele caia dentro do chão
        if (isGrounded && corpoPlayer.velocity.y < 0)
        {
            corpoPlayer.velocity = new Vector2(corpoPlayer.velocity.x, 0);
        }
    }

    public void Movimentacao()
    {
        // Se o Dash ou agachamento estiverem ativos, ignore o controle normal
        if (isDashing) return;

        velocidadePlayer = Input.GetAxis("Horizontal") * 4.5f;
        corpoPlayer.velocity = new Vector2(velocidadePlayer, corpoPlayer.velocity.y);

        if (velocidadePlayer != 0)
        {
            animacaoPlayer.SetBool("Andando", true); //se a velocidade o player for diferente de ZERO, a anaima��o de correndo vai rodar
        }
        else
        {
            animacaoPlayer.SetBool("Andando", false);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
        { 
            if (collision.gameObject.CompareTag("Chão"))
            {
                isGrounded = true;
                doubleJumpAvailable = true; // Reseta o pulo duplo ao tocar o ch�o
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chão"))
        {
            isGrounded = false; // Jogador saiu do ch�o
        }
    }

    public void Pular()
    {
        
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                corpoPlayer.velocity = Vector2.up * 8; // Primeiro pulo
                StartCoroutine(IgnoreCollisionTemporarily());

            }
            else if (playerAbilities != null && playerAbilities.CanDoubleJump() && doubleJumpAvailable)
            {
                corpoPlayer.velocity = Vector2.up * 8; // Pulo duplo
                doubleJumpAvailable = false; // Marca que o pulo duplo foi usado
                Debug.Log("Pulo duplo realizado!");
            }
            
        }
        if(isGrounded == false) //se o player sair do ch�o ativa a anima��o de pulando
        {
            animacaoPlayer.SetBool("pulando", true); 
        }
        else
        {
            animacaoPlayer.SetBool("pulando", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag ==  "win")
        {
            genJ.AbreMenuVitória();
            
        }

        if(collision.gameObject.tag == "Estrela")
        {
            genJ.AbreMenuHabilidade();
        }
    }
    private IEnumerator IgnoreCollisionTemporarily()
    {
        Collider2D playerCollider = GetComponent<Collider2D>();
        Collider2D groundCollider = GameObject.FindGameObjectWithTag("Chão").GetComponent<Collider2D>();

        Physics2D.IgnoreCollision(playerCollider, groundCollider, true); // Ignora colisão
        yield return new WaitForSeconds(0.5f); // Tempo para atravessar
        Physics2D.IgnoreCollision(playerCollider, groundCollider, false); // Restaura colisão
    }

}
