using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject gameOverText;
    public bool gameOver = false;
    public static GameController instance;
    public float scrollSpeed = -1.5f;
    private float score = 0;
    public Text scoreText;
    public bool Restart { get; set; }

	// Use this for initialization
	void Awake () {
		if (instance == null)
        {
            instance = this;
        }else if (instance != this)
        {
            Destroy(gameObject);
        }
	}
    // Update is called once per frame
    void Update () {
		if (gameOver && Restart)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (!gameOver && score % 10 ==0)
        {
            UpdateSpeed();
        }
	}

    public void PlayerScore()
    {
        if (gameOver)
            return;
        score += 0.1f;
        scoreText.text = "Score: " + ((int)score).ToString();
    }

    public void UpdateSpeed()
    {
        scrollSpeed -= 1f;
    }

    public void PlayerDied()
    {
        gameOverText.SetActive(true);
        gameOver = true;
    }
}
