using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VidaPlayer : MonoBehaviour
{

    public int vidaDoPlayer;
    public int vidaMaximaPlayer = 10;

    public Slider barraDeVidaPlayer;

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

        vidaDoPlayer = vidaMaximaPlayer;
        barraDeVidaPlayer.maxValue = vidaDoPlayer;
        barraDeVidaPlayer.value = vidaDoPlayer;

        genJ = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControllGame>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Inimigo")
        {
            vidaDoPlayer --;
            barraDeVidaPlayer.value = vidaDoPlayer;
        }

        if(vidaDoPlayer <= 0)
        {
            genJ.AbreGameOver();
        }
    }

    public void TomarDano(int dano)
    {
        vidaDoPlayer -= dano;
        if (barraDeVidaPlayer != null)
        {
            barraDeVidaPlayer.value = vidaDoPlayer;
        }

        if (vidaDoPlayer <= 0)
        {
            Debug.Log("O jogador morreu!");
        }
    }

}
