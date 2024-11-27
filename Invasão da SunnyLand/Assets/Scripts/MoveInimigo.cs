using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInimigo : MonoBehaviour
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
        if (this.transform.position.x > pontoA.position.x)
        {
            moveInimigo = true;
            transform.localScale = new Vector3(1, 1, 1);

        }
        else if (this.transform.position.x < pontoB.position.x)
        {
            moveInimigo = false;
            transform.localScale = new Vector3(-1, 1, 1);

        }

        if (moveInimigo)
        {
            transform.position = new Vector2(transform.position.x - velocidadeInimigo * Time.deltaTime, transform.position.y);
        }
        else if (!moveInimigo)
        {
            transform.position = new Vector2(transform.position.x + velocidadeInimigo * Time.deltaTime, transform.position.y);
        }

       
        }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fire")
        {
            Destroy(collision.gameObject);
        }
    }
}
