using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    private GameObject gameOver;
    private Unit playerUnitComponent;
    // Start is called before the first frame update
    void Start()
    {
        this.playerUnitComponent = GameObject.Find("Player").GetComponent<Unit>();
        this.gameOver = GameObject.Find("GameOver");
        this.gameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerUnitComponent.GetHealth() <= 0)
        {
            this.gameOver.SetActive(true);
            if (Input.GetKeyDown(InputSettings.GoToMenu))
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                SceneManager.LoadScene(0); // 0 --> Menu
            }
        }
    }
}
