using UnityEngine;
using Cysharp.Threading.Tasks;
// 일단은 비상등 깜빡이는 용도로 사용하려 제작되었음.
// 동시에 깜빡거리는거 필요한 곳에서는 모두 사용 가능.

// 켜고 끄는것을 개별조절하려면 startblink()/stopblink()를, 토글로 하려면 toggleblinkconverter()를 쓰시오.
public class BlinkObjectController : MonoBehaviour
{
    public GameObject[] blinkObjects;

    public float blinkIntervalSec = 0.5f;
    private int blinkInterval = 0;
    private bool isBlink = false;
    public bool isBlinkAtStart = false;


    private void Start()
    {
        blinkInterval = (int)(blinkIntervalSec*1000);

        for (int i = 0; i < blinkObjects.Length; i++)
        {
            blinkObjects[i].SetActive(isBlinkAtStart);
        }
        
    }
    public async UniTask BlinkObjectsMethod()
    {
        while (isBlink)
        {
            for (int i = 0; i < blinkObjects.Length; i++)
            {
                blinkObjects[i].SetActive(!blinkObjects[i].activeSelf);
            }
            await UniTask.Delay(blinkInterval);
        }
        for (int i = 0; i < blinkObjects.Length; i++)
        {
            blinkObjects[i].SetActive(false);
        }
    }

    public void StartBlink()
    {
        if (!isBlink)
        {
            if (blinkObjects == null)
            {
                #if UNITY_EDITOR
                Debug.LogWarning("blinkObject is null");
                #endif
                return;
            }
            isBlink = true;
            BlinkObjectsMethod().Forget();
        }
    }

    public void StopBlink()
    {
        isBlink = false;
        if (blinkObjects!=null)
        {
            for (int i = 0; i < blinkObjects.Length; i++)
            {
                blinkObjects[i].SetActive(false);
            }
        }
            //blinkObject.SetActive(false);
    }

    public void ToggleBlinkConverter()
    {
        if (isBlink)
            StopBlink();
        else if (!isBlink)
            StartBlink();
    }
}