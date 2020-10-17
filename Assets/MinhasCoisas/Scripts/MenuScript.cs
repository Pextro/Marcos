using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    Button butao;
    
 void Start()
    {
        butao = GameObject.Find("Play").GetComponent<Button>();
        butao.onClick.AddListener(Iniciodejogo);
    }
    // Update is called once per frame
    public void Iniciodejogo()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
