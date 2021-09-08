using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using SimpleJSON;


public class MainMenu : MonoBehaviour
{
    private readonly string baseURL = "https://predes2021.herokuapp.com";
    public Text usu;
    public Text highscoreText;
    private int score;
    public string usuarioensesion;

    // Start is called before the first frame update
    void Start()
    {
        highscoreText.text = "Highscore: " + PlayerPrefs.GetFloat("Highscore");
        
        usu.text = Conexion.datos.Nombre;
        NewRecord();

    }

    // Update is called once per frame
    public void ToGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void ToQuiz()
    {
        SceneManager.LoadScene("Quiz");
    }
    public void ToHelp()
    {
        SceneManager.LoadScene("Help");
    }
    public void NewRecord()
    {
        Debug.Log(PlayerPrefs.GetFloat("Highscore"));
        int.TryParse(Conexion.datos.Score, out score);
        if (PlayerPrefs.GetFloat("Highscore") > score)
        {
            Conexion.datos.Score = ""+ PlayerPrefs.GetFloat("Highscore");
            StartCoroutine(CorrutinaNewRecord());
        }
    }
    IEnumerator CorrutinaNewRecord()
    {
        string preguntasURL = baseURL + "/NewRecord/?id=" + Conexion.datos.id + "&score=" + (int)PlayerPrefs.GetFloat("Highscore") + "&preg=" + Conexion.datos.Pregunta;

        UnityWebRequest userInfoRequest = UnityWebRequest.Get(preguntasURL);

        yield return userInfoRequest.SendWebRequest();

        if (userInfoRequest.isNetworkError || userInfoRequest.isHttpError)
        {
            Debug.LogError(userInfoRequest.error);
            //yield break;
        }

    }
}
