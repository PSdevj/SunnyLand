using System.Collections;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(Animator))]
public class DragonController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float attackCooldown = 3f; // Tempo entre ataques
    [SerializeField] private GameObject fireballPrefab; // Prefab da bola de fogo
    [SerializeField] private Transform[] fireballSpawnPoints; // Pontos de spawn para as bolas de fogo
    [SerializeField] private float fireballFallSpeed = 5f; // Velocidade de queda das bolas de fogo
    [SerializeField] private int fireballDamage = 1; // Dano causado pelas bolas de fogo
    [SerializeField] private float attackDuration = 2f; // Duração da animação de ataque

    private Animator animator;
    private bool isAttacking = false;
    private float attackTimer = 0f;
    private BossHealth bossHealth; // Referência ao script BossHealth

    public ControllGame genJ; //acessar o script ContollGame

    private void Awake()
    {
        animator = GetComponent<Animator>();
        bossHealth = GetComponent<BossHealth>(); // Busca o BossHealth no mesmo GameObject
        genJ = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControllGame>();
    }

    private void Update()
    {
        if (genJ.EstadoDoJogo() == true)
        {
            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }

            if (!isAttacking && attackTimer <= 0)
            {
                StartCoroutine(Attack());
            }
        }
        
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack"); // Ativa a animação de cuspir fogo

        yield return new WaitForSeconds(attackDuration / 2); // Sincroniza o spawn com o meio da animação

        SpawnFireballs();

        yield return new WaitForSeconds(attackDuration / 2);
        isAttacking = false;
        attackTimer = attackCooldown;
    }

    private void SpawnFireballs()
    {
        foreach (var spawnPoint in fireballSpawnPoints)
        {
            GameObject fireball = Instantiate(fireballPrefab, spawnPoint.position, Quaternion.identity);

            // Configura a rotação para apontar para baixo
            fireball.transform.rotation = Quaternion.Euler(0, 0, -90); // Orienta a sprite para baixo

            Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.down * fireballFallSpeed;
            }

            FireballDragon fireballScript = fireball.GetComponent<FireballDragon>();
            if (fireballScript != null)
            {
                fireballScript.SetDamage(fireballDamage);
            }
        }
    }
}
