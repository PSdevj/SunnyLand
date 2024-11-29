using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    private int currentHealth;

    private Animator animator;
    public bool isDead { get; private set; } = false; // Propriedade somente leitura

    public Slider barraDeVidaBoss;
    private void Awake()
    {
        currentHealth = maxHealth; // Define a vida inicial
        barraDeVidaBoss.maxValue = currentHealth;
        barraDeVidaBoss.value = currentHealth;

        animator = GetComponent<Animator>(); // Referência ao Animator
        barraDeVidaBoss = FindObjectOfType<Slider>();

        
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return; // Evita tomar dano após a morte

        currentHealth -= damage;
        barraDeVidaBoss.value = currentHealth;
        Debug.Log($"Boss tomou {damage} de dano. Vida restante: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return; // Evita múltiplas chamadas para morrer

        isDead = true;
        animator.SetTrigger("Die"); // Garante a transição
        Debug.Log("Boss entrou no estado de morte");

        // Destruir o boss após a animação
        StartCoroutine(DestroyAfterDeath());
    }
    private IEnumerator DestroyAfterDeath()
    {
        yield return new WaitForSeconds(1f); // Tempo para a animação de morte
        Destroy(gameObject); // Remove o boss da cena
    }
}

