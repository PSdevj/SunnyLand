using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInimigoAves : MonoBehaviour
{

    private bool moveInimigo = false;

    public float velocidadeInimigo = 3f;
    public Transform pontoA;
    public Transform pontoB;

    public ControllGame genJ; //acessar o script ContollGam

    // Start is called before the first frame update
    void Start()
    {
        genJ = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControllGame>();
    }

    // Update is called once per frame
    void Update()
    {
        if (genJ.EstadoDoJogo() == true)
        {
            if (this.transform.position.y > pontoA.position.y)
            {
                moveInimigo = true;

            }
            else if (this.transform.position.y < pontoB.position.y)
            {
                moveInimigo = false;

            }
            if (moveInimigo)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - velocidadeInimigo * Time.deltaTime);
            }
            else if (!moveInimigo)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + velocidadeInimigo * Time.deltaTime);
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Projétil")
        {
            Destroy(gameObject);
        }
    }
}

