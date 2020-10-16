using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;// variavel do tipo levelmanager utilizada como auxiliar para abrir os leveis ou servir como base do checkpoint

    public GameObject player;// variavel referencia ao jogador

    public bool checkPoint;// variavel que checa se existe um checkpoint
    public Transform checkpointPos; // variavel que guarda a posiçao do novo checkpoint



    private void Awake()
    {
        instance = this;
    }

    public void SetcheckPoint(CheckPoint NewCheckpoint)// função que pega as informaçoes do script CheckPoint e o transforma em coordenadas a ser guardada no checkpointPos
    {
        if (NewCheckpoint != null)
        {
            checkPoint = true;
            checkpointPos = NewCheckpoint.transform;
        }
    }
    public void Restart()//função para respawnar o jogador no ultimo checkpoint salvo
    {
        player.transform.position = checkpointPos.position;

    }
}
