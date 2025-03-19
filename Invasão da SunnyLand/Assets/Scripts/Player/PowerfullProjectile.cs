using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerfullProjectile : MonoBehaviour
{
    public float speed = 10f; // Velocidade do proj�til
    public int damage = 5; // Dano causado pelo proj�til
    public float lifeTime = 3f; // Tempo de vida do proj�til

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float direction = transform.localScale.x > 0 ? 1f : -1f;
            rb.velocity = transform.right * speed * direction;
        }

        Destroy(gameObject, lifeTime); // Destroi o proj�til ap�s o tempo de vida
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Inimigo"))
        {
            Debug.Log("Proj�til poderoso acertou " + collision.gameObject.name);

            // Aplicar dano ao inimigo
            BossHealth bossHealth = collision.gameObject.GetComponent<BossHealth>();
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(damage);
            }
        }

        // Destroi o proj�til ao colidir com qualquer coisa que n�o seja o jogador
        if (!collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
