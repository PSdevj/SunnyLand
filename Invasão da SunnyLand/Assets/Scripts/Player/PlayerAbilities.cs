using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [SerializeField] private float dashSpeed = 15f; // Velocidade do dash
    [SerializeField] private float dashDuration = 0.2f; // Duração do dash
    [SerializeField] private float dashCooldown = 1f; // Tempo de espera entre dashes

    private bool canDash = false;
    public bool isDashing = false; // Indica se está no meio de um dash

    private bool canDoubleJump = false;
    private bool canShoot = false;
    private string currentProjectile = "Default";
    private float nextDashTime = 0f; // Tempo para permitir o próximo dash

    public GameObject projectilePowerfull;
    public GameObject projectile;

    public GameObject homingprojectile;
    public ControllPlayer controllPlayer;

    public Transform shootPoint;
    public float projectileSpeed;

    public float fireRate = 0.5f; // Tempo entre os tiros (em segundos)
    private float nextFireTime = 0f; // Controle de quando o próximo tiro pode acontecer
    private void Awake()
    {
        controllPlayer = GetComponent<ControllPlayer>();
    }

    void Update()
    {
        // Verifica se o botão esquerdo do mouse foi pressionado
        if (canShoot && Input.GetMouseButtonDown(0)) // 0 = botão esquerdo
        {
            Atirar();
        }
        // Verifica se o Shift esquerdo foi pressionado
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash();
        }
    }
    public void UnlockAbility(string abilityName)
    {
        switch (abilityName)
        {
            case "Atirar":
                canShoot = true;
                Debug.Log("Habilidade de atirar desbloqueada!");
                break;

            case "Pulo Duplo":
                canDoubleJump = true;
                Debug.Log("Habilidade de pulo duplo desbloqueada!");
                break;

            case "Projétil Rastreador":
                currentProjectile = "Homing";
                canShoot = true;
                Debug.Log("Projétil rastreador desbloqueado! Agora você não precisa se preocupar em mirar nos seus inimigos!!");
                break;

            case "Dash":
                canDash = true;
                Debug.Log("Habilidade de Dash desbloqueada!");
                break;

            case "Projétil Poderoso":
                currentProjectile = "Powerfull";  
                canShoot = true;
                Debug.Log("Projétil poderoso desbloqueado! Agora você da 5 de dano a mais!!");
                break;

            default:
                Debug.LogWarning("Habilidade desconhecida: " + abilityName);
                break;
        }
    }
    private void Atirar()
    {
        // Verifica se já passou o tempo necessário para o próximo tiro
        if (Time.time >= nextFireTime)
        {
            // Atualiza o tempo do próximo tiro permitido
            nextFireTime = Time.time + fireRate;

            // Determina a direção do disparo com base na orientação do jogador
            float shootDirection = transform.localScale.x > 0 ? 1f : -1f;
            GameObject currentProjectileInstance;

            switch (currentProjectile)
            {
                case "Homing":
                    currentProjectileInstance = Instantiate(homingprojectile, shootPoint.position, shootPoint.rotation);

                    if (controllPlayer.esquerda)
                    {
                        currentProjectileInstance.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    //else
                    //{
                    //    projectile.GetComponent<SpriteRenderer>().flipX = false;
                    //}
                    break;

                case "Powerfull":
                    currentProjectileInstance = Instantiate(projectilePowerfull, shootPoint.position, shootPoint.rotation);
                    break;

                default:
                    // Instancia o projétil na posição correta
                    currentProjectileInstance = Instantiate(projectile, shootPoint.position, Quaternion.identity);
                    break;
            }

            // Configura a velocidade do projétil
            Rigidbody2D rb = currentProjectileInstance.GetComponent<Rigidbody2D>();
            if (rb != null && currentProjectile != "Homing")
            {
                rb.velocity = new Vector2(shootDirection * projectileSpeed, 0); // Movimento horizontal
            }

            // Ajusta a escala do projétil para a direção correta
            currentProjectileInstance.transform.localScale = new Vector3(
                Mathf.Abs(currentProjectileInstance.transform.localScale.x) * shootDirection,
                currentProjectileInstance.transform.localScale.y,
                currentProjectileInstance.transform.localScale.z
            );
        }
        else
        {
            Debug.Log("Esperando cooldown para atirar novamente!");
        }
    }

    public bool CanDoubleJump()
    {
        return canDoubleJump; // Retorna se o pulo duplo está desbloqueado
    }

    private void Dash()
    {
        if (!canDash || Time.time < nextDashTime) return;

        // Marca que o Dash está em execução
        isDashing = true;

        // Define o próximo Dash permitido
        nextDashTime = Time.time + dashCooldown;

        // Salva o valor atual da gravidade e desativa temporariamente
        float originalGravity = controllPlayer.corpoPlayer.gravityScale;
        controllPlayer.corpoPlayer.gravityScale = 0f;

        // Movimentação direta (simula Dash sem usar física)
        float dashDirection = controllPlayer.esquerda ? -1f : 1f;
        Vector3 dashVector = new Vector3(dashDirection * dashSpeed * dashDuration, 0, 0);
        transform.position += dashVector;

        // Ativa a animação de Dash
        controllPlayer.animacaoPlayer.SetTrigger("rolar");

        // Espera a animação começar antes de aplicar a força
        StartCoroutine(EndDash(originalGravity));
    }

    private bool IsGrounded()
    {
        // Substitua por sua lógica de verificação de chão
        return controllPlayer.corpoPlayer.velocity.y == 0 || Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
    }

    private IEnumerator EndDash(float originalGravity)
    {
        yield return new WaitForSeconds(dashDuration);

        isDashing = false; // Reseta o estado de Dash
        controllPlayer.corpoPlayer.gravityScale = originalGravity; // Restaura gravidade
        controllPlayer.animacaoPlayer.SetBool("Andando", Mathf.Abs(controllPlayer.velocidadePlayer) > 0);
    }
}
