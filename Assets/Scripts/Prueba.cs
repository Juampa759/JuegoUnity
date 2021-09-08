using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Prueba : MonoBehaviour
{
    private readonly string URL = "http://localhost:8000/listapreguntas/";
    public string[] preguntasData;
    public Text titulo, Ba, Bb, Bc, Bd;
    public Text msn;
    public int i;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        WWW preguntas = new WWW(URL);
        yield return preguntas;
        string preguntasDataString = preguntas.text;
        preguntasData = preguntasDataString.Split(';');

        i = 0; 

        titulo.text = GetValueData(preguntasData[i], "Pregunta:");
        Ba.text = GetValueData(preguntasData[i], "A:");
        Bb.text = GetValueData(preguntasData[i], "B:");
        Bc.text = GetValueData(preguntasData[i], "C:");
        Bd.text = GetValueData(preguntasData[i], "D:");

        print(preguntasData.Length);
    }

    string GetValueData(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains("|"))
        {
            value = value.Remove(value.IndexOf("|"));
        }
        return value;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void siguiente()
    {
        i++;
        if(i < preguntasData.Length-1)
        {
            PreguntasData preguntasdata = new PreguntasData();
            preguntasdata.pre = titulo.text;
            preguntasdata.res = Bb.text;

            string data = JsonUtility.ToJson(preguntasdata);

            StartCoroutine(registrar());




            //File.WriteAllText(Application.dataPath + "/saveFile.json", json);

            titulo.text = GetValueData(preguntasData[i], "Pregunta:");
            Ba.text = GetValueData(preguntasData[i], "A:");
            Bb.text = GetValueData(preguntasData[i], "B:");
            Bc.text = GetValueData(preguntasData[i], "C:");
            Bd.text = GetValueData(preguntasData[i], "D:");
        }
        else
        {
            titulo.text = "Gracias por responder";
            Ba.text = "";
            Bb.text = "";
            Bc.text = "";
            Bd.text = "";
        }
    }
    private class PreguntasData
    {
        public string pre;
        public string res;
    }


    IEnumerator registrar()
    {//hilo que ejecuta por otra parte
        WWW conexion = new WWW("http://127.0.0.1:8000/prueba/" + titulo.text);
        yield return (conexion);
        Debug.Log(conexion.text);
        
        //string[] datos = conexion.text.Split('|');
        //Debug.Log(conexion.text);
        /*
        if (conexion.text == "402")
        {

        }
        else
        {
            if (datos.Length > 0)
            {
                nombreusr = datos[0];
                apellidousr = datos[1];
            }
            else
            {
                print("error");
                msnError.SetActive(true);
                GameObject.Find("mensaje").GetComponent<Text>().text = "Error";
                registoUser.SetActive(false);
            }
        }
        */


    }
    public void ToGame()
    {
        SceneManager.LoadScene("Menu");
    }


}
