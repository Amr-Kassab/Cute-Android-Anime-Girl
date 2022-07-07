using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashingSequences : MonoBehaviour
{
    public float ShadowInsatantiationTime;
    public GameObject Shadow;
    PlayerMovement player;
    bool ShadowInstantiated = false;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.isDashing && !ShadowInstantiated)
        {
            StartCoroutine("ShadowDashing");
        }
    }
    IEnumerator ShadowDashing()
    {
        ShadowInstantiated = true;
        while (player.isDashing)
        {
            Instantiate(Shadow , gameObject.transform.position , Quaternion.identity);
            yield return new WaitForSeconds(ShadowInsatantiationTime);
        }
        ShadowInstantiated = false;
    }
}
