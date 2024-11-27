using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    private bool canDoubleJump = false;
    private bool canShoot = false;
    private string currentProjectile = "Default";

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

            case "Projétil Rastreador":
                currentProjectile = "Homing";
                Debug.Log("Projétil rastreador desbloqueado!");
                break;

                /*case "":
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
        // Verifica se já passou o tempo necessário para o próximo tiro
        if (Time.time >= nextFireTime)
        {
            // Atualiza o tempo do próximo tiro permitido
            nextFireTime = Time.time + fireRate;

            // Determina a direção do disparo com base na orientação do jogador
            float shootDirection = transform.localScale.x > 0 ? 1f : -1f;

            // Instancia o projétil na posição correta
            GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

            switch (currentProjectile)
            {
                case "Homing":
                    projectile = Instantiate(Resources.Load<GameObject>("HomingProjectile"), shootPoint.position, shootPoint.rotation);
                    break;
            }

            // Configura a velocidade do projétil
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = new Vector2(shootDirection * projectileSpeed, 0); // Movimento horizontal
            }

            // Ajusta a escala do projétil para a direção correta
            projectile.transform.localScale = new Vector3(
                Mathf.Abs(projectile.transform.localScale.x) * shootDirection,
                projectile.transform.localScale.y,
                projectile.transform.localScale.z
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
}
