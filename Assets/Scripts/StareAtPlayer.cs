using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StareAtPlayer : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
        else
        {
            if (player.transform.position.x > transform.position.x)
            {
                GetComponent<NPCIdle>().direction = NPCIdle.Direction.right;
            }
            else
            {
                GetComponent<NPCIdle>().direction = NPCIdle.Direction.left;
            }
        }
    }
}
