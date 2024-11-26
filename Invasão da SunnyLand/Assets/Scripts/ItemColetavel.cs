using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemColetavel : MonoBehaviour
{

    public int gema = 0;
    public Text gemaText;

    // Start is called before the first frame update
    void Start()
    {
        ModText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ModText()
    {
        gemaText.text = gema.ToString() + "X";
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Gema")
        {
            Destroy(collision.gameObject);
            gema++;
            ModText();
        }
    }


}
