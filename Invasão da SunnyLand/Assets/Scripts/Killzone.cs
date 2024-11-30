using UnityEngine;

public class KillZone : MonoBehaviour
{
    public ControllGame genJ; //acessar o script ContollGame

    private void Start()
    {
        genJ = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControllGame>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            VidaPlayer player = collision.GetComponent<VidaPlayer>();
            if (player != null)
            {
                genJ.AbreGameOver();
            }
        }
    }
}
