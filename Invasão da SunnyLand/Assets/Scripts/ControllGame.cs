using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControllGame : MonoBehaviour
{

    public bool gameLigado = false;

    [SerializeField] private GameObject menuPrincipal;
    [SerializeField] private GameObject menuCredito;
    [SerializeField] private GameObject menuTutorial;


    // Start is called before the first frame update
    void Start()
    {
        gameLigado = true;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool EstadoDoJogo()
    {
        return gameLigado;
    }


    public void MenuTutorial()
    {
        menuPrincipal.SetActive(false);
        menuTutorial.SetActive(true);
        Time.timeScale = 1;
    }

    public void FecharMenuTutorial()
    {
        menuTutorial.SetActive(false);
        menuPrincipal.SetActive(true);
        Time.timeScale = 1;
    }

    public void MenuCredito()
    {
        menuPrincipal.SetActive(false);
        menuCredito.SetActive(true);
        Time.timeScale = 1;
    }

    public void FecharMenuCredito()
    {
        menuCredito.SetActive(false);
        menuPrincipal.SetActive(true);
        Time.timeScale = 1;
    }




    public void LoadScene(string nomeCena) //Irá chamar a cena pelo nome.
    {
        SceneManager.LoadScene(nomeCena);
    }


    public void RestartGame() //Reiniciar a fase
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel() //Avançar para a próxima fase
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex * 1);
    }

}
