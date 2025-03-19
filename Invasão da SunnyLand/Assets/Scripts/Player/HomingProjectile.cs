using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    public float speed = 5f; // Velocidade do proj�til
    public float rotationSpeed = 200f; // Velocidade de rota��o ao seguir o alvo
    public float lifeTime = 5f; // Tempo de vida do proj�til
    public int damage = 1; // Dano causado ao atingir o alvo
    private Rigidbody2D rb;

    GameObject nearestEnemy = null;
    private Transform target;

    void Start()
    {
        // Encontra o inimigo mais pr�ximo ao ser instanciado
        FindNearestTarget();

        // Destroi o proj�til ap�s um tempo (evita que fique indefinidamente na cena)
        Destroy(gameObject, lifeTime);

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (target == null)
        {
            // Se n�o houver alvo, continue em linha reta
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            Debug.Log("Inimigo nao encontrado");
            return;
        }

        // Dire��o para o alvo
        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();

        // Rotaciona gradualmente na dire��o do alvo
        float rotateAmount = Vector3.Cross(direction, transform.right).z;

        rb.angularVelocity = -rotateAmount * rotationSpeed;

        rb.velocity = transform.right * speed;

    }

    private void FindNearestTarget()
    {
        // Encontra todos os objetos com a tag "Inimigo"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Inimigo");
        float shortestDistance = Mathf.Infinity;
        nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            Debug.LogWarning("Nenhum inimigo encontrado para o proj�til rastreador.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Inimigo"))
        {
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