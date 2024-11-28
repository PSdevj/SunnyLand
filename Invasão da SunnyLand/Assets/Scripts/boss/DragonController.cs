using System.Collections;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
public class DragonController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float moveSpeed = 3f;       // Velocidade do Boss
    [SerializeField] float attackRange = 2f;    // Distância para atacar o Player
    [SerializeField] float detectionRange = 10f; // Distância para começar a seguir o Player
    [SerializeField] float attackCooldown = 2f; // Tempo entre ataques
    [SerializeField] Transform player;          // Referência ao Player

    private Animator animator;
    private Rigidbody2D rb;
    private bool isAttacking = false;
    private float attackTimer = 0f;
    private BossHealth bossHealth; // Referência ao script BossHealth

    enum State { Idle, Chase, Attack }
    State currentState = State.Idle;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        bossHealth = GetComponent<BossHealth>(); // Busca o BossHealth no mesmo GameObject

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform; // Procura o player automaticamente
        }
    }

    private void Update()
    {
        // Verifica se o boss está morto
        if (bossHealth != null && bossHealth.isDead)
        {
            rb.velocity = Vector2.zero; // Para qualquer movimento
            return; // Não executa mais lógica
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        switch (currentState)
        {
            case State.Idle:
                IdleState(distanceToPlayer);
                break;
            case State.Chase:
                ChaseState(distanceToPlayer);
                break;
            case State.Attack:
                AttackState(distanceToPlayer);
                break;
        }

        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
    }

    #region States

    private void IdleState(float distanceToPlayer)
    {
        animator.Play("idle_Dragon");

        if (distanceToPlayer <= detectionRange)
        {
            currentState = State.Chase;
        }
    }

    private void ChaseState(float distanceToPlayer)
    {
        animator.Play("walk_Dragon");

        // Movimenta o boss em direção ao player
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

        // Flipar a sprite para olhar na direção do player
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(2.4f, 2.4f, 2.4f);
        }
        else
        {
            transform.localScale = new Vector3(-2.4f, 2.4f, 2.4f);
        }

        // Verifica se está na distância de ataque
        if (distanceToPlayer <= attackRange)
        {
            rb.velocity = Vector2.zero; // Para de se mover ao alcançar o player
            currentState = State.Attack;
        }
    }

    private void AttackState(float distanceToPlayer)
    {
        if (!isAttacking && attackTimer <= 0)
        {
            isAttacking = true;
            rb.velocity = Vector2.zero; // Garante que o boss fique parado durante o ataque
            animator.SetTrigger("Attack");

            /// Reduz a vida do jogador (chama o script do jogador diretamente)
            if (player != null) // Confirma se o jogador existe
            {
                //Forma de dano na animação
            }

            attackTimer = attackCooldown;
            StartCoroutine(EndAttack());
        }
        else
        {
            attackTimer -= Time.deltaTime; // Decrementa o cooldown
        }

        // Volta a seguir o player se ele sair do alcance de ataque
        if (distanceToPlayer > attackRange)
        {
            currentState = State.Chase;
        }
    }

    #endregion

    #region Ataque

    private IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(1f); // Duração da animação de ataque
        isAttacking = false;
    }

    #endregion

    private void OnDrawGizmosSelected()
    {
        // Mostra as áreas de alcance no editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

    public void DamagePlayer()
    {
        VidaPlayer vidaPlayer = player.GetComponent<VidaPlayer>();
        if (vidaPlayer != null)
        {
            vidaPlayer.TomarDano(1);
        }
    }
}
