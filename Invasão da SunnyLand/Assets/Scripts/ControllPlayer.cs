using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllPlayer : MonoBehaviour
{

    public Rigidbody2D corpoPlayer;
    public float velocidadePlayer;

    private bool isGrounded = false; // Verifica se está no chão
    private bool doubleJumpAvailable = true; // Controla se o pulo duplo pode ser usado

    private PlayerAbilities playerAbilities; // Referência ao script de habilidades


    public Animator animacaoPlayer; //controla a animação do player

    public ControllGame genJ; //acessar o script ContollGame


    //responsável pelo tiro básico do player.
    public GameObject laserPlayer;
    public Transform localLaserPlayer;
    public float tempoMaximoTiro;
    public float tempoAtualTiro;


    // Start is called before the first frame update
    void Start()
    {
        corpoPlayer = GetComponent<Rigidbody2D>();
        playerAbilities = GetComponent<PlayerAbilities>(); // Certifique-se de que o script está no mesmo GameObject
        animacaoPlayer = GetComponent<Animator>();
        genJ = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControllGame>();


        tempoAtualTiro = tempoMaximoTiro;
    }

    // Update is called once per frame
    void Update()
    {
        Movimentação();
        Pular();

        // Verifica a direção do movimento e vira o player para a esquerda ou direita
        if (velocidadePlayer > 0)
        {
            // Vira para a direita
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (velocidadePlayer < 0)
        {
            // Vira para a esquerda
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (tempoAtualTiro <= 0)
        {
            Atirar();
        }
        tempoAtualTiro -= Time.deltaTime;


    }



    public void Movimentação()
    {
        velocidadePlayer = Input.GetAxis("Horizontal") * 3.5f;
        corpoPlayer.velocity = new Vector2(velocidadePlayer, corpoPlayer.velocity.y);

        if (velocidadePlayer != 0)
        {
            animacaoPlayer.SetBool("Andando", true); //se a velocidade o player for diferente de ZERO, a anaimação de correndo vai rodar
        }
        else
        {
            animacaoPlayer.SetBool("Andando", false);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chão"))
        {
            isGrounded = true;
            doubleJumpAvailable = true; // Reseta o pulo duplo ao tocar o chão
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chão"))
        {
            isGrounded = false; // Jogador saiu do chão
        }
    }

    public void Pular()
    {
        
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                corpoPlayer.velocity = Vector2.up * 8; // Primeiro pulo
               
            }
            else if (playerAbilities != null && playerAbilities.CanDoubleJump() && doubleJumpAvailable)
            {
                corpoPlayer.velocity = Vector2.up * 8; // Pulo duplo
                doubleJumpAvailable = false; // Marca que o pulo duplo foi usado
                Debug.Log("Pulo duplo realizado!");
            }
            
        }
        if(isGrounded == false) //se o player sair do chão ativa a animação de pulando
        {
            animacaoPlayer.SetBool("pulando", true); 
        }
        else
        {
            animacaoPlayer.SetBool("pulando", false);
        }
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag ==  "win")
        {
            genJ.AbreMenuVitória();
            
        }
    }


    public void Atirar()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(laserPlayer, localLaserPlayer.position, localLaserPlayer.rotation);
            tempoAtualTiro = tempoMaximoTiro;
        
        }
  
    }

    
}
