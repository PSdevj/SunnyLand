using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballDragon : MonoBehaviour
{
    private int damage;

    public void SetDamage(int value)
    {
        damage = value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            VidaPlayer vidaPlayer = collision.GetComponent<VidaPlayer>();
            if (vidaPlayer != null)
            {
                vidaPlayer.TomarDano(damage);
            }
            Destroy(gameObject);
        }
        // Destroi o projétil ao colidir com qualquer coisa que não seja o jogador
        if (!collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject); // Destroi a bola de fogo se sair da tela
    }
}
