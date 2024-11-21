using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllPlayer : MonoBehaviour
{

    public Rigidbody2D corpoPlayer;
    public float velocidadePlayer;

    
   

    // Start is called before the first frame update
    void Start()
    {
        corpoPlayer = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movimentação();
        Pular();

        
    }



    public void Movimentação()
    {
        velocidadePlayer = Input.GetAxis("Horizontal") * 3.5f;
        corpoPlayer.velocity = new Vector2(velocidadePlayer, corpoPlayer.velocity.y);
    }

    public void Pular()
    {
        if (Input.GetButtonDown("Jump"))
        {
            corpoPlayer.velocity = Vector2.up * 8;
        }
    }
      
    
}
