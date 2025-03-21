using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public string abilityName; // Nome da habilidade que o item desbloqueia

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Verifica se o player coletou
        {
            PlayerAbilities playerAbilities = other.GetComponent<PlayerAbilities>();
            if (playerAbilities != null)
            {
                playerAbilities.UnlockAbility(abilityName); // Desbloqueia a habilidade no player
            }

            Destroy(gameObject); // Destroi o item ap�s a coleta
        }
    }
}
