using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllPlayer : MonoBehaviour
{
    public Rigidbody2D corpoPlayer;
    public float velocidadePlayer;

    private bool isGrounded = false; // Verifica se est� no ch�o
    private bool doubleJumpAvailable = true; // Controla se o pulo duplo pode ser usado

    private PlayerAbilities playerAbilities; // Refer�ncia ao script de habilidades


    public Animator animacaoPlayer; //controla a anima��o do player

    public ControllGame genJ; //acessar o script ContollGame


    //respons�vel pelo tiro b�sico do player.
    public GameObject laserPlayer;
    public Transform localLaserPlayer;
    public float tempoMaximoTiro;
    public float tempoAtualTiro;


    // Start is called before the first frame update
    void Start()
    {
        corpoPlayer = GetComponent<Rigidbody2D>();
        playerAbilities = GetComponent<PlayerAbilities>();
        animacaoPlayer = GetComponent<Animator>();
        genJ = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControllGame>();


        tempoAtualTiro = tempoMaximoTiro;
    }

    // Update is called once per frame
    void Update()
    {
        Movimenta��o();
        Pular();

        // Verifica a dire��o do movimento e vira o player para a esquerda ou direita
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



    public void Movimenta��o()
    {
        velocidadePlayer = Input.GetAxis("Horizontal") * 3.5f;
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
        if (collision.gameObject.CompareTag("Ch�o"))
        {
            isGrounded = true;
            doubleJumpAvailable = true; // Reseta o pulo duplo ao tocar o ch�o
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ch�o"))
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
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag ==  "win")
        {
            genJ.AbreMenuVit�ria();
            
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
