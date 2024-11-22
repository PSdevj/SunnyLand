using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    private bool canDoubleJump = false;
    private bool canShoot = false;

    public GameObject projectilePrefab;

    public Transform shootPoint;
    public float projectileSpeed;

    public float fireRate = 0.5f; // Tempo entre os tiros (em segundos)
    private float nextFireTime = 0f; // Controle de quando o próximo tiro pode acontecer
    // Update is called once per frame
    void Update()
    {
        // Verifica se o botão esquerdo do mouse foi pressionado
        if (canShoot && Input.GetMouseButtonDown(0)) // 0 = botão esquerdo
        {
            Atirar();
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

                /*case "":
                     = true;
                    Debug.Log("Habilidade de  desbloqueada!");
                    break;

                case "":
                     = true;
                    Debug.Log("Habilidade de  desbloqueada!");
                    break;

                case "":
                     = true;
                    Debug.Log("Habilidade de  desbloqueada!");
                    break;

                default:
                    Debug.LogWarning("Habilidade desconhecida: " + abilityName);
                    break;*/
        }
    }
    private void Atirar()
    {
        //Verifica se o player está virado para a direita
        if(transform.localScale.x > 0)
        {
            // Verifica se já passou o tempo necessário para o próximo tiro
            if (Time.time >= nextFireTime)
            {
                // Atualiza o tempo do próximo tiro permitido
                nextFireTime = Time.time + fireRate;

                // Instanciar o projétil
                GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);

                // Adicionar movimento ao projétil
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 shootDirection = shootPoint.right; // Direção do disparo
                    rb.velocity = shootDirection * projectileSpeed;
                }
                else
                {
                    Debug.LogWarning("O projétil não possui um Rigidbody2D!");
                }
            }
            else
            {
                Debug.Log("Esperando cooldown para atirar novamente!");
            }
        }
        else
        {
            Debug.Log("Não pode atirar porque o jogador está virado para a esquerda");
        }
    }
    public bool CanDoubleJump()
    {
        return canDoubleJump; // Retorna se o pulo duplo está desbloqueado
    }
}
