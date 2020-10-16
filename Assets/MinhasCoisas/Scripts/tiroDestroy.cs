using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tiroDestroy : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
