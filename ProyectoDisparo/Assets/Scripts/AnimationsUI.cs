using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsUI : MonoBehaviour
{
    [SerializeField] private GameObject logo;
    [SerializeField] private GameObject inicioGroup;
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.moveX(logo.GetComponent<RectTransform>(), 0, 1.5f).setDelay(1.5f).setEase(LeanTweenType.easeInOutBack).setOnComplete(BajarAlpha);
    }

    private void BajarAlpha()
    {
        LeanTween.alpha(inicioGroup.GetComponent<RectTransform>(), 0, 1f).setDelay(1f);
        inicioGroup.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

}
