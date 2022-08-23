using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dialogues : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textDialogue;
    [SerializeField] string[ ] dialogues = new string[]{"WOW", "AMAZING", "NICE", "YES"}; 
    
    private string emptyString = "";
    private int _randomText;
    
    private void Update()
    {
        _randomText = Random.Range(0, dialogues.Length);
    }

    public void Dialogue( )
    {
       WriteText();
       Invoke("CleanText", 3);
    }
    

    void WriteText()
    {
        if (textDialogue.text != emptyString)
        {
            CleanText();
        }
        textDialogue.text = dialogues[_randomText];
    }

    void CleanText()
    {
        textDialogue.text = emptyString;
    }
}
