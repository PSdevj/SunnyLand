using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjetilPlayer : MonoBehaviour
{
    public float speed;
    public float lifetime = 5f; // Tempo antes do projétil desaparecer
    private void Start()
    {
        Destroy(gameObject, lifetime); // Destroi o projétil após o tempo definido
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime); // Movimenta o projétil
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Lógica para impacto
        Debug.Log($"Projétil atingiu: {collision.gameObject.name}");
        Destroy(gameObject); // Destroi o projétil após colidir
    }
}
