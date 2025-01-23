using UnityEngine;
using UnityEngine.UI;

public class ToggleImage : MonoBehaviour
{
    private Image image;
    public Sprite sprite1;
    public Sprite sprite2;

    private void Start()
    {
        image = this.GetComponent<Image>();
        image.sprite = sprite1;
    }

    public void ToggleImageChange()
    {
        image.sprite = image.sprite == sprite1 ? image.sprite = sprite2 : image.sprite = sprite1;
    }
      public void SetImage(int _index)
    {
        if(_index == 0)
        {
            image.sprite = sprite1;
        }
        else
        {
            image.sprite = sprite2;
        }
    }
}
