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
            transform.Translate(Vector3.up * velocidadeInimigo * Time.deltaTime);

        }
        else if (this.transform.position.y < pontoB.position.y)
        {
            transform.Translate(Vector3.down * velocidadeInimigo * Time.deltaTime);

        }



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Fire")
        {
            Destroy(gameObject);
        }
    }
}

