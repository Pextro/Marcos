using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : Damageable
{

    private int defaultLayer;//variavel condizente ao layer invencible                             
    public override void Death()//função que chama a função de restart do levelManager para respawnar o jogador
    {
        if (LevelManager.instance.checkPoint)
        {
            Debug.Log("Checkpoint");
            LevelManager.instance.Restart();
            Respawn();
        }
        else
        {

            Invoke("ReloadScene", 0.1f);
        }
    }
    new void Start()
    {
        base.Start();
        defaultLayer = gameObject.layer;
    }

    public void SetInvincible(bool state)
    {
        if (state)
        {
            gameObject.layer = defaultLayer;

        }
        else
        {
            UIManager.instance.UpdateHealthBar(currentHealth);
            gameObject.layer = 11; 
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
