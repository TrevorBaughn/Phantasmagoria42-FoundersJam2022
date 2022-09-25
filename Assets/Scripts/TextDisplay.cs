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
    public int messageDelay = 3500;
    int x = 0;
    bool displayed = false;

    public static T[] Shuffle<T>(T[] array)
    {
        var rand = new System.Random();

        int n = array.Length;
        while (n > 1) {
            n--;
            int k = rand.Next(n + 1);
            T value = array[k];
            array[k] = array[n];
            array[n] = value;
        }

        return array;
    }

    public void Load(string savedData)
    {
        JsonUtility.FromJsonOverwrite(savedData, this);
    }

    public async void DisplayIntro()
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
        if (x == random.Length || x == 0)
        {
            random = Shuffle<string>(random);
            x = 0;
        }
        foreach (string text in random[x].Split("\\n"))
        {
            textMesh.text = text;
            await Task.Delay(messageDelay);
        }

        textMesh.text = "";
        x++;
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
