using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class HudController : MonoBehaviour
{
    public GameObject[] characters;
    private List<GameObject> currentCharacters = new List<GameObject>();
    public RectTransform[] characterPositions;
    private int displayedCharacters;
    public Image sight;
    public Image sound;
    public Image cooldownBar;
    public Image healthBar;

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
            for (int i = 0; i < currentCharacters.Count; i++)
            {
                GameObject character = currentCharacters[i];
                Tween color = character.GetComponent<Image>().DOColor(Color.green, .1f).SetLoops(4, LoopType.Yoyo);
                color.OnComplete(() => { Destroy(character); currentCharacters.Remove(character); });
            }
        }
        else
        {
            for (int i = 0; i < currentCharacters.Count; i++)
            {
                GameObject character = currentCharacters[i];
                Tween color = character.transform.DOShakePosition(.25f, 15, 100, 100);
                color.OnComplete(() => { Destroy(character); currentCharacters.Remove(character); });
            }
        }
        displayedCharacters = 0;
    }

    public void UpdateVisual(float amount)
    {
        Color currentColor = new Color(amount, amount, amount, 1);
        sight.color = currentColor;
    }

    public void UpdateSound(float amount)
    {
        Color currentColor = new Color(amount, amount, amount, 1);
        sound.color = currentColor;
    }

    public void UpdateCooldown(float amount)
    {
        cooldownBar.fillAmount = amount;
    }

    public void UpdateHealth(float amount)
    {
        Color originalColor = healthBar.color;
        Tween color = healthBar.DOColor(Color.white, 1).SetEase(Ease.OutElastic);
        color.OnComplete(() => healthBar.color = originalColor);
        DOTween.To(() => healthBar.fillAmount, x => healthBar.fillAmount = x, amount, 1);
    }
}
