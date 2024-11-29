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
    [SerializeField] private GameObject menuVitoria;
    [SerializeField] private GameObject menuGameOver;
    [SerializeField] private GameObject menuPlayer;
    [SerializeField] private GameObject menuPause;



    // Start is called before the first frame update
    void Start()
    {
        gameLigado = true;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
       
       
            if (Input.GetKey(KeyCode.Escape))
            {
                AbrePause();
            }
       
    }

    public bool EstadoDoJogo()
    {
        return gameLigado;
    }


    public void MenuTutorial() //Abre o painel tutorial
    {
        menuPrincipal.SetActive(false);
        menuTutorial.SetActive(true);
        Time.timeScale = 1;
    }

    public void FecharMenuTutorial()//Fehca o painel tutorial
    {
        menuTutorial.SetActive(false);
        menuPrincipal.SetActive(true);
        Time.timeScale = 1;
    }

    public void MenuCredito() //Abre o painel credito
    {
        menuPrincipal.SetActive(false);
        menuCredito.SetActive(true);
        Time.timeScale = 1;
    }

    public void FecharMenuCredito() //Fecha o painel Credito
    {
        menuCredito.SetActive(false);
        menuPrincipal.SetActive(true);
        Time.timeScale = 1;
    }


    public void AbreMenuVitória()
    {
        menuVitoria.SetActive(true);
        menuPlayer.SetActive(false);
        Time.timeScale = 0;
        gameLigado = false;
    }

    public void AbreGameOver()
    {
        menuGameOver.SetActive(true);
        menuPlayer.SetActive(false);
        Time.timeScale = 0;
        gameLigado = false;
    }

    public void AbrePause()
    {
        menuPause.SetActive(true);
        Time.timeScale = 0;
        gameLigado = false;
    }

    public void IniciarJogo()
    {
        menuPause.SetActive(false);
        Time.timeScale = 1;
        gameLigado = false;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
