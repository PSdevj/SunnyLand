/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodigoCamera : MonoBehaviour
{

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SeguirPlayer();
    }


    //Responsável por fazer a câmera acompanhar o Player somente no eixo X
    void SeguirPlayer()
    {
        Vector3 destino = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, destino, 0.1f);
    }
}
*/