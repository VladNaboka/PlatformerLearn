using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class moveTraps : MonoBehaviour
{
    void Start()
    {
        transform.DOMoveX(endValue: transform.position.x - 3f, duration: 1.5f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }
}
