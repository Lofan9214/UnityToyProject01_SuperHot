using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    enum State
    {
        Awake,
        InGame,
        GameOver,
    }

    private State state;

    private PlayerMovement move;
    private PlayerInput input;

    public TextMeshProUGUI startText;
    public TextMeshProUGUI gameOverText;
    public GameObject crossHair;

    private float initialFixedDT = 0.02f;

    public int enemyCount;

    private void Awake()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        move = player.GetComponent<PlayerMovement>();
        input = player.GetComponent<PlayerInput>();
        input.enabled = false;
        state = State.Awake;
        Time.timeScale = 0f;
        Time.fixedDeltaTime = initialFixedDT * Time.timeScale;

        enemyCount = 0;
    }

    private void FixedUpdate()
    {
        if (state != State.InGame)
        {
            return;
        }

        if (move.isGrounded && input.AxisInput < 1e-5)
        {
            Time.timeScale = 0.1f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        Time.fixedDeltaTime = initialFixedDT * Time.timeScale;
    }

    private void Update()
    {
        switch (state)
        {
            case State.Awake:
                if (Input.GetButtonDown("Fire1"))
                {
                    state = State.InGame;
                    input.enabled = true;
                    startText.gameObject.SetActive(false);
                    crossHair.SetActive(true);
                    Time.timeScale = 0.1f;
                    Time.fixedDeltaTime = initialFixedDT * Time.timeScale;
                }
                break;
            case State.InGame:
                if (enemyCount == 0)
                {
                    state = State.GameOver;
                    Time.timeScale = 0f;
                    Time.fixedDeltaTime = initialFixedDT * Time.timeScale;

                    crossHair.SetActive(false);
                    gameOverText.gameObject.SetActive(true);
                    gameOverText.text = "Cleared!\nClick To Restart";
                }
                break;
            case State.GameOver:
                if (Input.GetButtonDown("Fire1"))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                break;
        }
    }

    public void PlayerDie()
    {
        state = State.GameOver;
        Time.timeScale = 0f;
        Time.fixedDeltaTime = initialFixedDT * Time.timeScale;
        gameOverText.gameObject.SetActive(true);
        gameOverText.text = "Game Over!\nClick To Restart";
    }
}
