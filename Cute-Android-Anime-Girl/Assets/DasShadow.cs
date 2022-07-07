using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DasShadow : MonoBehaviour
{
    PlayerMovement Player;
    void Start()
    {
        Player = FindObjectOfType<PlayerMovement>(); 
        transform.localScale = Player.localScale;
        StartCoroutine("destroy");
    }
    IEnumerator destroy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
