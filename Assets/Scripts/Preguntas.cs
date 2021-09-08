using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleJSON;

public class Preguntas : MonoBehaviour
{
    private readonly string baseURL = "https://predes2021.herokuapp.com";
    private WWW www;
    public string usuarioensesion;
    public Text pregunta, resA, resB, resC, resD, prueba;
    public string respUsu;
    private int score;
    private int pre;

    [System.Serializable]
    public struct estructuraDatosWeb
    {
        public string IdPre;
        public string Pregunta;
        public string Ra;
        public string Rb;
        public string Rc;
        public string Rd;
    }

    static public estructuraDatosWeb datos;
    public estructuraDatosWeb[] lista;

    // Start is called before the first frame update
    void Start()
    {
        Leer();
        
    }
    // Update is called once per frame
    [ContextMenu("Leer")]
    public void Leer()
    {

        StartCoroutine(CorrutinaLeer());
    }

    IEnumerator CorrutinaLeer()
    {
        bool estadoResponse = true;

        string preguntasURL = baseURL + "/Pregunta/?id=" + Conexion.datos.id;

        UnityWebRequest userInfoRequest = UnityWebRequest.Get(preguntasURL);

        yield return userInfoRequest.SendWebRequest();

        if (userInfoRequest.isNetworkError || userInfoRequest.isHttpError)
        {
            Debug.LogError(userInfoRequest.error);
            //yield break;
            estadoResponse = false;
            prueba.text = "error";
        }

        if (estadoResponse)
        {

            JSONNode info = JSON.Parse(userInfoRequest.downloadHandler.text);


            datos.IdPre     = info["id"];
            datos.Pregunta  =   info["pregunta"];
            datos.Ra        =   info["RespuestaA"];
            datos.Rb        =   info["RespuestaB"];
            datos.Rc        =   info["RespuestaC"];
            datos.Rd        =   info["RespuestaD"];

            pregunta.text = datos.Pregunta;
            resA.text = datos.Ra;
            resB.text = datos.Rb;
            resC.text = datos.Rc;
            resD.text = datos.Rd;



        }
    }

    [ContextMenu("Leer")]
    public void siguiente()
    {

        StartCoroutine(CorrutinaSig());
    }

    IEnumerator CorrutinaSig()
    {
        bool estadoResponse = true;

        string preguntasURL = baseURL + "/Siguiente/?id=" + Conexion.datos.id+"&res="+ respUsu;

        UnityWebRequest userInfoRequest = UnityWebRequest.Get(preguntasURL);

        yield return userInfoRequest.SendWebRequest();

        if (userInfoRequest.isNetworkError || userInfoRequest.isHttpError)
        {
            Debug.LogError(userInfoRequest.error);
            //yield break;
            estadoResponse = false;
            prueba.text = "error";
        }
        Leer();
        int.TryParse(Conexion.datos.Pregunta, out pre);
        pre = pre + 1;
        Conexion.datos.Pregunta = ""+pre;
    }
    public void respA()
    {
        respUsu = resA.text;
        siguiente();
    }
    public void respB()
    {
        respUsu = resB.text;
        siguiente();
    }
    public void respC()
    {
        respUsu = resC.text;
        siguiente();
    }
    public void respD()
    {
        respUsu = resD.text;
        siguiente();
    }







    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

}
