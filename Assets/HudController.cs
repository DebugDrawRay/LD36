using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;

public class HudController : MonoBehaviour
{
    public GameObject[] characters;
    private List<GameObject> currentCharacters = new List<GameObject>();
    public RectTransform[] characterPositions;
    private int displayedCharacters;
    public Image sight;
    public Image sound;

    public static HudController instance;

    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    public void DisplayCharacter(AncientTool.Input input)
    {
        if (displayedCharacters < characterPositions.Length)
        {
            GameObject display = Instantiate(characters[(int)input].gameObject);
            display.GetComponent<RectTransform>().position = characterPositions[displayedCharacters].position;
            display.GetComponent<RectTransform>().SetParent(characterPositions[displayedCharacters]);
            display.GetComponent<RectTransform>().localScale = new Vector3(5, 5, 5);
            currentCharacters.Add(display);
            displayedCharacters++;
        }
    }

    public void ConfirmCharacters(bool valid)
    {
        if(valid)
        {

        }
        else
        {

        }

        for(int i = 0; i < currentCharacters.Count; i++)
        {
            Destroy(currentCharacters[i]);
        }
        currentCharacters = new List<GameObject>();
        displayedCharacters = 0;
    }

    public void UpdateVisual(float amount)
    {

    }

    public void UpdateSound(float amount)
    {

    }

}
