using UnityEngine;

public class BossManager : MonoBehaviour
{
    public BossHealth boss; // Referência ao script de vida do boss
    public GameObject winObject; // O GameObject de vitória

    private void Start()
    {
        // Garante que o objeto de vitória está desativado no início
        if (winObject != null)
        {
            winObject.SetActive(false);
        }
    }

    private void Update()
    {
        // Verifica se o boss está morto
        if (boss != null && boss.isDead && winObject != null && !winObject.activeSelf)
        {
            // Ativa o GameObject de vitória
            winObject.SetActive(true);
            Debug.Log("O boss foi derrotado! Ativando o objeto de vitória.");
        }
    }
}
