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

    // Variáveis de imunidade
    public bool isImmune = false; // Controla se o player está imune
    public float immunityTime = 1.5f; // Tempo de imunidade em segundos
    private float immunityTimer = 0f; // Temporizador para a imunidade

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

    void Update()
    {
        // Atualiza o temporizador de imunidade
        if (isImmune)
        {
            immunityTimer -= Time.deltaTime;
            if (immunityTimer <= 0)
            {
                isImmune = false; // Desativa a imunidade
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Inimigo") || collision.gameObject.CompareTag("Boss"))
        {
            TomarDano(1); // Aplica 1 de dano (ajuste conforme necessário)
        }
    }

    public void TomarDano(int dano)
    {
        // Se o player estiver imune, não toma dano
        if (isImmune) return;

        vidaDoPlayer -= dano;
        barraDeVidaPlayer.value = vidaDoPlayer;

        StartImmunity();

        if (vidaDoPlayer <= 0)
        {
            genJ.AbreGameOver();
        }
    }
    private void StartImmunity()
    {
        isImmune = true;
        immunityTimer = immunityTime; // Inicia o temporizador
        StartCoroutine(FlashPlayer()); // Inicia o efeito de piscar
    }

    private IEnumerator FlashPlayer()
    {
        while (isImmune)
        {
            // Alterna a visibilidade do sprite (pisca)
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(0.1f); // Tempo entre cada piscada
        }

        // Garante que o sprite fique visível ao final
        spriteRenderer.enabled = true;
    }
}
