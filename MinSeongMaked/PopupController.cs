using UnityEngine;
using Cysharp.Threading.Tasks;

public class PopupController:MonoBehaviour
{
    public GameObject popupObject;
    public float showTimeInSec = 2.0f;
    private int calculatedTimeToMiliSec = 0;

    private void Start()
    {
        if (popupObject == null)
        {
            #if UNITY_EDITOR
            Debug.LogWarning("popupObject is null");
            #endif
        }
        else
            popupObject.SetActive(false);
        
        calculatedTimeToMiliSec = (int)showTimeInSec * 1000;
    }

    private async UniTask ShowPopupUniTask(int time)
    {
        if (popupObject == null || popupObject.activeSelf)
        {
            #if UNITY_EDITOR
            Debug.LogWarning("popupObject is null or popup is aleady show");
            #endif
            return;
        }
        popupObject.SetActive(true);
        await UniTask.Delay(time);
        popupObject.SetActive(false);
    }
    public void ShowTimedPopup()
    {
        ShowPopupUniTask(calculatedTimeToMiliSec).Forget();
    }
}

    /*자연스럽게 켜지고 꺼지는건 나중에 하자.
    //public GameObject popupObject;
    public Sprite popupSprite;
    public TextMeshProUGUI popupTMPro;
    public String popupText;
    public float showTime = 3.0f;

    private void Start()
    {
        if (popupSprite == null)
        {
            #if UNITY_EDITOR
            Debug.LogWarning("popup panel is null");
            #endif
        }
        else
        {
            popupSprite.~~.SetActive(false);
        }
    }
    private async UniTask ShowPopup(String text, float time)
    {
        float elapsedTime = 0.0f;
        //make fade effect
        while (elapsedTime < showTime)
        {
            elapsedTime += Time.deltaTime;
            popupSprite.Color = new Color32(0,0,0, convertint(elapsedTime/showTime));
        }
    }
    */