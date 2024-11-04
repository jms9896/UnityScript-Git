using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using System;


public class ButtonInteractionComponent : MonoBehaviour
{

    [Header("버튼 interaction 관련")]
    private Button button;
    private Image buttonImage;
    private Sprite buttonSpriteBackup;
    public Sprite buttonClickedSprite;
    private RectTransform buttonObjectRectTransform;
    [Tooltip ("true면 버튼 클릭시 작동함")]
    public bool isInteraction = true;
    public float scaleTimes = 1.03f;
    [Tooltip ("Y축으로 움직여야하는 특별한 상황에서만 true로 하세요.")]
    public bool isMovingY = false;
    public float clickedDistance = 20.0f;
    public float clickedSec = 1.0f;

    [Space(10)]
    [Tooltip ("버튼 lock 관련(더블터치 방지)")]
    public bool wantDisableDoubleClicked = true;

    [Space(10)]
    [Tooltip ("버튼 사라지게함")]
    public bool isRemainButton = true;
    public float remainTimes = 0.5f;


    private void Start()
    {
        StartButtonSettings();
    }

    private void StartButtonSettings()
    {
        button = this.gameObject.GetComponent<Button>();
        buttonImage = this.gameObject.GetComponent<Image>();
        buttonSpriteBackup = buttonImage.sprite;
        buttonObjectRectTransform = this.gameObject.GetComponent<RectTransform>();

        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        //더블클릭방지
        ButtonLock().Forget();
        //이미지 onclicked로 변경, 복구
        ButtonImageChange().Forget();
        //위치 y값 이동, 복구
        ButtonClickedMovement().Forget();
        //크기 변경 - 확대, 복구
        ButtonObjectScale().Forget();
        //isRemainButton에 따라 button 사라짐
        HideButton().Forget();
    }

    private async UniTask ButtonImageChange()
    {
        if (!isInteraction || buttonClickedSprite == null)
            return;

        buttonImage.sprite = buttonClickedSprite;
        await UniTask.Delay(TimeSpan.FromSeconds(clickedSec));
        buttonImage.sprite = buttonSpriteBackup;
    }

    private async UniTask ButtonLock()
    {
        if (!wantDisableDoubleClicked)
            return;

        button.interactable = false;
        await UniTask.Delay(TimeSpan.FromSeconds(clickedSec));
        button.interactable = true;
    }

    private async UniTask ButtonObjectScale()
    {
        if (!isInteraction)
            return;
        
        float elapsedTime = 0.0f;
        Vector3 buttonScaleBackup = buttonObjectRectTransform.localScale;
        Vector3 targetScale = buttonObjectRectTransform.localScale * scaleTimes;

        while (elapsedTime < clickedSec / 2)
        {
            elapsedTime += Time.deltaTime;
            buttonObjectRectTransform.localScale = Vector3.Lerp(buttonScaleBackup, targetScale, elapsedTime / (clickedSec / 2));
            await UniTask.Yield();
        }
        elapsedTime = 0.0f;
        while (elapsedTime < clickedSec / 2)
        {
            elapsedTime += Time.deltaTime;
            buttonObjectRectTransform.localScale = Vector3.Lerp(targetScale, buttonScaleBackup, elapsedTime / (clickedSec / 2));
            await UniTask.Yield();
        }
        buttonObjectRectTransform.localScale = buttonScaleBackup;
    }

    private async UniTask ButtonClickedMovement()
    {
        if (!isInteraction)
            return;

        float elapsedTime = 0.0f;
        Vector3 buttonObjectPositionBackup = buttonObjectRectTransform.localPosition;

        if (!isMovingY) // 일반적인 z축 방향으로 움직일 때
        {
            float buttonPositionZ = buttonObjectPositionBackup.z;
            float buttonPositionZBackup = buttonObjectPositionBackup.z;

            while (elapsedTime < clickedSec/2)
            {
                elapsedTime += Time.deltaTime;
                buttonPositionZ = Mathf.Lerp(buttonPositionZBackup, buttonPositionZBackup+clickedDistance, elapsedTime/ (clickedSec/2));
                buttonObjectRectTransform.localPosition = new Vector3(buttonObjectPositionBackup.x,buttonObjectPositionBackup.y,buttonPositionZ);
                await UniTask.Yield();
            } 
            elapsedTime = 0.0f;
            while (elapsedTime < clickedSec/2)
            {
                elapsedTime += Time.deltaTime;
                buttonPositionZ = Mathf.Lerp(buttonPositionZBackup+clickedDistance, buttonPositionZBackup, elapsedTime/ (clickedSec/2));
                buttonObjectRectTransform.localPosition = new Vector3(buttonObjectPositionBackup.x,buttonObjectPositionBackup.y,buttonPositionZ);
                await UniTask.Yield();
            }
            buttonObjectRectTransform.localPosition = buttonObjectPositionBackup;
        }
        else if (isMovingY) // y축 방향으로 움직 때
        {
            float buttonPositionY = buttonObjectPositionBackup.y;
            float buttonPositionyBackup = buttonObjectPositionBackup.y;

            while (elapsedTime < clickedSec/2)
            {
                elapsedTime += Time.deltaTime;
                buttonPositionY = Mathf.Lerp(buttonPositionyBackup, buttonPositionyBackup+clickedDistance, elapsedTime/ (clickedSec/2));
                buttonObjectRectTransform.localPosition = new Vector3(buttonObjectPositionBackup.x,buttonPositionY, buttonObjectPositionBackup.z);
                await UniTask.Yield();
            } 
            elapsedTime = 0.0f;
            while (elapsedTime < clickedSec/2)
            {
                elapsedTime += Time.deltaTime;
                buttonPositionY = Mathf.Lerp(buttonPositionyBackup+clickedDistance, buttonPositionyBackup, elapsedTime/ (clickedSec/2));
                buttonObjectRectTransform.localPosition = new Vector3(buttonObjectPositionBackup.x,buttonPositionY, buttonObjectPositionBackup.z);
                await UniTask.Yield();
            }
            buttonObjectRectTransform.localPosition = buttonObjectPositionBackup;
        }
    }


    private async UniTask HideButton()
    {
        if (isRemainButton)
            return;
        await UniTask.Delay(TimeSpan.FromSeconds(clickedSec));
        float elapsedTime = 0.0f;
        float buttonImageAlpha = buttonImage.color.a;
        Color color = buttonImage.color;
        button.interactable = false;
        while(elapsedTime < remainTimes)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(buttonImageAlpha, 0, elapsedTime/remainTimes);
            buttonImage.color = color;
            await UniTask.Yield();
        }
        buttonImage.color = new Color(buttonImage.color.r, buttonImage.color.g, buttonImage.color.b, buttonImageAlpha);
        button.interactable = true;
        this.gameObject.SetActive(isRemainButton);
    }
}
