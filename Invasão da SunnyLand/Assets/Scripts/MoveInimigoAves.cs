using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInimigoAves : MonoBehaviour
{

    private bool moveInimigo = false;

    public float velocidadeInimigo = 3f;
    public Transform pontoA;
    public Transform pontoB;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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

