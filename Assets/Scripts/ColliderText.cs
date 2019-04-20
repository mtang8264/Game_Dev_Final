using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ColliderText : MonoBehaviour
{
    public string text;
    public TextMeshPro textMesh;
    public int characters;
    public float timePerCharacter;
    private float timer;
    public bool triggered;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        if(triggered)
        {
            if(timer <= 0)
            {
                timer = timePerCharacter;
                characters++;
                if (characters > text.Length)
                    characters = text.Length;
            }
            timer -= Time.deltaTime;
        }
        else if(characters > 0)
        {
            characters--;
        }

        textMesh.text = text.Substring(0, characters);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        triggered = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        triggered = false;
    }
}
