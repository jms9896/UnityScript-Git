using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonPreventDoubleTouch : MonoBehaviour
{
    public Button[] PreventButtons;
    public float DelayTimes = 1.0f;

    private void Start()
    {
        foreach (Button button in PreventButtons)
        {
            button.onClick.AddListener(() => OnButtonClick(button));
        }
    }
    public void OnButtonClick(Button clickedButton)
    {
        StartCoroutine(DisableButtonTemporarily(clickedButton));
    }

    IEnumerator DisableButtonTemporarily(Button button)
    {
        button.interactable = false;
        yield return new WaitForSeconds(DelayTimes);
        button.interactable = true;
    }
}