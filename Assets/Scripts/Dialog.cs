using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    public Animator arrow;
    public TextMeshPro text;
    public bool present;
    public bool active;
    [Header("DIALOG STUFF")]
    public int currentLine;
    public int currentCharacter;
    public float timePerCharacter;
    public float timer;
    [Header("SCRIPT")]
    public string[] dialog;
    private bool secondPressCheck = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(arrow == null)
        {
            arrow = GameObject.Find("Arrow").GetComponent<Animator>();
        }
        else
        {
            if(!active)
                arrow.SetBool("Visible", present);
        }

        if((present && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))) && !active)
        {
            arrow.SetBool("Visible", false);
            active = true;
            timer = timePerCharacter;
            currentLine = 0;
            currentCharacter = 0;
            secondPressCheck = true;
        }

        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.UpArrow))
            secondPressCheck = false;

        if(active)
        {
            GameObject.Find("Player").GetComponent<PlayerControls>().active = false;

            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                Debug.Log("Tick");
                currentCharacter++;
                timer = timePerCharacter;
            }
            if (currentCharacter > dialog[currentLine].Length)
                currentCharacter = dialog[currentLine].Length;

            if((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && !secondPressCheck)
            {
                if (currentCharacter < dialog[currentLine].Length)
                    currentCharacter = dialog[currentLine].Length;
                else
                {
                    currentLine++;
                    currentCharacter = 0;
                    if(currentLine >= dialog.Length)
                    {
                        currentLine = 0;
                        active = false;
                        GameObject.Find("Player").GetComponent<PlayerControls>().active = true;
                        return;
                    }
                }
            }

            text.text = dialog[currentLine].Substring(0, currentCharacter);
        }
        else
        {
            if(text.text.Length > 0)
                text.text = text.text.Substring(0, text.text.Length - 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        present = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        present = false;
    }
}
