using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveLaserBasico : MonoBehaviour
{
   
    //Código responsável pela velocidade da pera
    public float speed = 8f;
    public float lifetime = 5f; // Tempo antes do projétil desaparecer

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime); // Destroi o projétil após o tempo definido

    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(Vector3.right * speed * Time.deltaTime);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
