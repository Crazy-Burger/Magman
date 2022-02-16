using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    
    public GameObject completeLevelUI;
    public float restartDelay = 2f;
    private GameMaster gm;
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {

            //CompeleteLevel();
            gm.lastCheckPointPos.x = -9;
            gm.lastCheckPointPos.y = -1.7f;
            //After build each level, go to File>>build setting, drag each scene in order
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
            //Restart();
            //Invoke("Restart", restartDelay);

        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CompeleteLevel()
    {
        Debug.Log("LEVEL CLEAR!");
        completeLevelUI.SetActive(true);
    }
}
