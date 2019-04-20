using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCIdle : MonoBehaviour
{
    private Animator animator;
    public enum Direction { left, right };
    public Direction direction;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(direction)
        {
            case Direction.left:
                animator.SetBool("direction", false);
                break;
            case Direction.right:
                animator.SetBool("direction", true);
                break;
        }
    }
}
