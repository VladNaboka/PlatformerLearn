using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButtonImage : MonoBehaviour
{
    public Sprite newButtonImage;
    public Button button;

    public void ChangeImage()
    {
        button.image.sprite = newButtonImage;
    }
}
