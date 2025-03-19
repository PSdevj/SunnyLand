using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VidaPlayer : MonoBehaviour
{

    public int vidaDoPlayer;
    public int vidaMaximaPlayer = 10;
    public Slider barraDeVidaPlayer;
    public SpriteRenderer spriteRenderer; // Referência ao SpriteRenderer

    private Color corOriginal;

    public ControllGame genJ; //acessar o script ContollGame

    // Start is called before the first frame update
    void Start()
    {
        if (barraDeVidaPlayer == null)
        {
            barraDeVidaPlayer = FindObjectOfType<Slider>();
            Debug.LogError("Barra de Vida do Player n�o foi atribu�da no Inspector!");
            return;
        }

        // Inicializa o SpriteRenderer e salva a cor original
        spriteRenderer = GetComponent<SpriteRenderer>();
        corOriginal = spriteRenderer.color;

        vidaDoPlayer = vidaMaximaPlayer;
        barraDeVidaPlayer.maxValue = vidaDoPlayer;
        barraDeVidaPlayer.value = vidaDoPlayer;

        genJ = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControllGame>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Inimigo")
        {
            vidaDoPlayer --;
            barraDeVidaPlayer.value = vidaDoPlayer;
            StartCoroutine(MudarCorTemporariamente());
        }

        if(vidaDoPlayer <= 0)
        {
            genJ.AbreGameOver();
        }

        if (collision.gameObject.tag == "Boss")
        {
            vidaDoPlayer--;
            barraDeVidaPlayer.value = vidaDoPlayer;
            StartCoroutine(MudarCorTemporariamente());
        }
    }

    public void TomarDano(int dano)
    {
        vidaDoPlayer -= dano;
        if (barraDeVidaPlayer != null)
        {
            barraDeVidaPlayer.value = vidaDoPlayer;
            StartCoroutine(MudarCorTemporariamente());
        }

        if (vidaDoPlayer <= 0)
        {
            genJ.AbreGameOver();
        }
    }

    private IEnumerator MudarCorTemporariamente()
    {
        // Muda a cor para vermelho
        spriteRenderer.color = Color.red;

        // Espera 0.2 segundos
        yield return new WaitForSeconds(0.2f);

        // Volta para a cor original
        spriteRenderer.color = corOriginal;
    }
}
