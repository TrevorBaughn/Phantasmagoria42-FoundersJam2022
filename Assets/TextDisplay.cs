using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using UnityEngine;
using TMPro;

public class TextDisplay : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public string[] intro = new string[1];
    public string[] random = new string[1];
    public int debugIndex = 0;
    public int killsPerMessage = 25;
    public int messageDelay = 2000;
    bool displayed = false;

    public void Load(string savedData)
    {
        JsonUtility.FromJsonOverwrite(savedData, this);
    }

    public async Task DisplayIntro()
    {
        foreach (string text in intro)
        {
            textMesh.text = text;
            await Task.Delay(messageDelay);
        }

        textMesh.text = "";
    }

    public async void DisplayRandom()
    {
        var rand = new System.Random();

        // foreach (string text in random[rand.Next(random.Length)].Split("\\n"))
        foreach (string text in random[debugIndex].Split("\\n"))
        {
            textMesh.text = text;
            await Task.Delay(messageDelay);
        }

        textMesh.text = "";
    }

    public void Update()
    {
        if (GameManager.instance.kills == 0 & !displayed)
        {
            DisplayIntro();
            displayed = true;
        }
        else if (GameManager.instance.kills % killsPerMessage == 0 && !displayed)
        {
            DisplayRandom();
            displayed = true;
        }
        else if (GameManager.instance.kills % killsPerMessage == 0) {}
        else if (displayed)
        {
            displayed = false;
        }
    }
}
