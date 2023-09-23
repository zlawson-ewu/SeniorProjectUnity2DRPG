using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [SerializeField] GameObject dialogueBox;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI dialogueText;

    [SerializeField] float textSpeed;

    private Queue<string> lines;
    private string currentLine;

    Player_Movement playerMovement;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = Player_Movement.Instance;
        lines = new Queue<string>();
        dialogueText.text = string.Empty;
        dialogueBox.SetActive(false);
    }

    private void Update()
    {
        if (!playerMovement.getIsInteracting())
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(dialogueText.text == currentLine)
            {
                DisplayNextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = currentLine;
            }
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Dialogue started.");

        foreach(string line in dialogue.lines)
        {
            lines.Enqueue(line);
        }
        
        dialogueBox.SetActive(true);
        nameText.text = dialogue.name;

        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        Debug.Log("Next Line");
        SoundManager.Instance.PlaySFX("HoverButtonSFX");
        if (lines.Count > 0)
        {
            currentLine = lines.Dequeue();
            dialogueText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    IEnumerator TypeLine()
    {
        dialogueText.text = "";
        foreach(char character in currentLine.ToCharArray())
        {
            dialogueText.text += character;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void EndDialogue()
    {
        dialogueBox.SetActive(false);
        playerMovement.setIsInteracting(false);
    }
}
