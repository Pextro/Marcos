using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iventario : MonoBehaviour
{
    public static Iventario inventario;
    public List<Key> keys;
    void Awake()
    {
        if (inventario == null)
        {
            inventario = this;
        }
        else if (inventario != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void addKey(Key key)
    {
        keys.Add(key);
    }

    public bool checkKey(Key key)
    {
        for (int i = 0; i < keys.Count; i++)
        {
            if (keys[i] == key)
            {
                return true;
            }
        }
        return false;
    }
}
