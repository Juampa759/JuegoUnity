using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleJSON;

public class Conexion : MonoBehaviour
{
    private readonly string baseURL = "https://predes2021.herokuapp.com";
    /// <summary>
    /// serializacion de los datos de Json
    /// </summary>
    [System.Serializable]
    public struct estructuraDatosWeb
    {
        public string id;
        public string usuario;
        public string Nombre;
        public string Apellido;
        public string Correo;
        public string Score;
        public string Pregunta;
        public string Msn;
    }

    static public estructuraDatosWeb datos;

    static public bool estadosesion=false;

    public GameObject user;
    public GameObject pass;
    private WWW www;
    public string usuarioensesion;
    public Text prueba;
    public int score;

    void Start()
    {
        
    }

    // Update is called once per frame
    /// <summary>
    /// metodo leer donde se inicia la corrutina 
    /// </summary>
    [ContextMenu("Leer")]
    public void Leer()
    {
        StartCoroutine(CorrutinaLeer(user.gameObject.GetComponent<InputField>().text, pass.gameObject.GetComponent<InputField>().text));
    }

    /// <summary>
    /// metodo de conexion al webservice a travez de json
    /// </summary>
    /// <param name="usr">parametro de usuario</param>
    /// <param name="pas">parametro de password</param>
    /// <returns></returns>
    IEnumerator CorrutinaLeer(string usr, string pas)
    {
        bool estadoResponse = true;

        string usuarioURL = baseURL +  "/datosjson/?usr="+usr+"&pass="+pas;

        UnityWebRequest userInfoRequest = UnityWebRequest.Get(usuarioURL);

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


            datos.id        = info["id"];
            datos.usuario   = info["usuario"];
            datos.Nombre    = info["Nombre"];
            datos.Apellido  = info["Apellido"];
            datos.Correo    = info["Correo"];
            datos.Score     = info["score"];
            datos.Pregunta  = info["pregunta"];
            datos.Msn       = info["msn"];

            estadosesion = true;

            if (datos.Msn == "Bienvenido")
            {
                Debug.Log("Bienvenido");
                SceneManager.LoadScene("Menu");
            }
            else if (datos.Msn == "Usuario o contraseña no encontrados")
            {
                Debug.Log("Revisa tus datos");
                prueba.text = "Revisa tus datos";

            }
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
