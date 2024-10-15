using UnityEngine;
using UnityEngine.UI;

public class Change360VideoImage : MonoBehaviour
{
    public Button ChangeButton;
    //public GameObject VideoPanel;
    public GameObject[] ImagePanel;
    private int count=0; // 0번은 빈 오브젝트로 채우세요. 세부 코딩하기 귀찮
    public Button ToggleCarButton;
    public GameObject CarObject;



    void Start()
    {
        if (ChangeButton != null)
            ChangeButton.onClick.AddListener(() => ChangeVideoAndImage());
        if (ToggleCarButton != null)
            ToggleCarButton.onClick.AddListener(() => CarObject.SetActive(!CarObject.activeSelf));
        //VideoPanel.SetActive(false);
        foreach (GameObject obj in ImagePanel)
        {
            obj.SetActive(false);
        }
        
        ImagePanel[0].SetActive(true);
    }

    private void ChangeVideoAndImage()
    {
        count++;
        if(count % ImagePanel.Length == 0)
        {
            ImagePanel[ImagePanel.Length - 1].SetActive(false);
            ImagePanel[count % ImagePanel.Length].SetActive(true);
        }
        else
        {
            ImagePanel[count % ImagePanel.Length - 1].SetActive(false);
            ImagePanel[count % ImagePanel.Length].SetActive(true);

        }
        //if (count % ImagePanel.Length == 0)
        //{
        //    foreach (GameObject obj in ImagePanel)
        //    {
        //        obj.SetActive(false);
        //    }
        //}
        //else
        //{
        //    ImagePanel[count % ImagePanel.Length].SetActive(true);
        //    if (count % ImagePanel.Length != 1)
        //        ImagePanel[count % ImagePanel.Length - 1].SetActive(false);

        //}

        //if (VideoPanel.activeSelf ^ ImagePanel.activeSelf)
        //{
        //    VideoPanel.SetActive(!VideoPanel.activeSelf);
        //    ImagePanel.SetActive(!ImagePanel.activeSelf);
        //}
        //else
        //    ImagePanel.SetActive(true);

    }
}
