using System.Collections;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
public class LoboController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float moveSpeed = 3f;       // Velocidade do Boss
    [SerializeField] float attackRange = 2f;    // Dist�ncia para atacar o Player
    [SerializeField] float detectionRange = 10f; // Dist�ncia para come�ar a seguir o Player
    [SerializeField] float attackCooldown = 2f; // Tempo entre ataques
    [SerializeField] Transform player;          // Refer�ncia ao Player

    private Animator animator;
    private Rigidbody2D rb;
    private bool isAttacking = false;
    private float attackTimer = 0f;
    private BossHealth bossHealth; // Refer�ncia ao script BossHealth

    enum State { Idle, Chase, Attack }
    State currentState = State.Idle;

    public ControllGame genJ; //acessar o script ContollGame

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        bossHealth = GetComponent<BossHealth>(); // Busca o BossHealth no mesmo GameObject
        genJ = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControllGame>();

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform; // Procura o player automaticamente
        }

        // Ajusta a dire��o inicial do boss para olhar na dire��o do player
        if (player.position.x > transform.position.x && transform.localScale.x < 0)
        {
            Flip();
        }
        else if (player.position.x < transform.position.x && transform.localScale.x > 0)
        {
            Flip();
        }
    }

    private void Update()
    {
        if (genJ.EstadoDoJogo() == true)
        {
            // Verifica se o boss est� morto
            if (bossHealth != null && bossHealth.isDead)
            {
                rb.velocity = Vector2.zero; // Para qualquer movimento
                return; // N�o executa mais l�gica
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
            
    }

    #region States

    private void IdleState(float distanceToPlayer)
    {
        animator.Play("idle1_Wolf");

        if (distanceToPlayer <= detectionRange)
        {
            currentState = State.Chase;
        }
    }

    private void ChaseState(float distanceToPlayer)
    {
        animator.Play("walk_Wolf");

        // Movimenta o boss em dire��o ao player
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

        // Flipar a sprite para olhar na dire��o do player
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(10.62414f, 10.62414f, 10.62414f);
        }
        else
        {
            transform.localScale = new Vector3(-10.62414f, 10.62414f, 10.62414f);
        }

        // Verifica se a dire��o precisa de ajuste no flip
        if ((direction.x > 0 && transform.localScale.x < 0) || (direction.x < 0 && transform.localScale.x > 0))
        {
            Flip();
        }

        // Verifica se est� na dist�ncia de ataque
        if (distanceToPlayer <= attackRange)
        {
            rb.velocity = Vector2.zero; // Para de se mover ao alcan�ar o player
            currentState = State.Attack;
        }
    }
    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Inverte apenas o eixo X
        transform.localScale = scale;
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
                //Forma de dano na anima��o
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
        yield return new WaitForSeconds(1f); // Dura��o da anima��o de ataque
        isAttacking = false;
    }

    #endregion

    private void OnDrawGizmosSelected()
    {
        // Mostra as �reas de alcance no editor
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
