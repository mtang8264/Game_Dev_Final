using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float allowance;
    public bool leftBound;
    public float leftBoundDistance;
    public bool rightBound;
    public float rightBoundDistance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.x - transform.position.x > allowance && (!rightBound || transform.position.x < rightBoundDistance))
        {
            transform.position = new Vector3(player.position.x - allowance, transform.position.y, -10);
        }
        else if (player.position.x - transform.position.x < -1 * allowance && (!leftBound || transform.position.x > leftBoundDistance))
        {
            transform.position = new Vector3(player.position.x + allowance, transform.position.y, -10);
        }
    }
}
