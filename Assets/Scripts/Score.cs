using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private float score = 0.0f;
    private int dificultylevel = 1;
    private int MaxdificultyLevel = 10;
    private int scoreToNextLevel = 20;
    public Text scoreText;
    private bool isDead = false;
    public DeathMenu deathMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
            return;
        if (score >= scoreToNextLevel)
            LevelUp();

        score += Time.deltaTime * dificultylevel;
        scoreText.text = ((int)score).ToString();
    }

    void LevelUp()
    {
        if (dificultylevel == MaxdificultyLevel)
            return;

        scoreToNextLevel *= 2;
        dificultylevel++;
        GetComponent<PlayerMotor>().SetSpeed(dificultylevel);

        Debug.Log(dificultylevel);
        Debug.Log(scoreText.text);
    }
    public void OnDeath()
    {
        isDead = true;
        if(PlayerPrefs.GetFloat("Highscore") < score)
            PlayerPrefs.SetFloat("Highscore", score);

        deathMenu.ToggleEndMenu(score);
    }
}
