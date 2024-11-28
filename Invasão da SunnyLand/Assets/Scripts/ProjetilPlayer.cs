using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjetilPlayer : MonoBehaviour
{
    public float speed;
    public float lifetime = 5f; // Tempo antes do projétil desaparecer
    public int damage = 1; // Dano causado pelo projétil
    private void Start()
    {
        Destroy(gameObject, lifetime); // Destroi o projétil após o tempo definido
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se atingiu o boss
        if (collision.gameObject.CompareTag("Inimigo"))
        {
            BossHealth bossHealth = collision.gameObject.GetComponent<BossHealth>();
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(damage);
            }

            // Destroi o projétil após o impacto
            Destroy(gameObject);
        }
    }
}

