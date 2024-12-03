using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MasherNumber : MonoBehaviour
{
    private TMP_Text T_text;
    public string S_text;
    private RectTransform RT_rectTransform;

    private void Awake()
    {
        T_text = GetComponent<TMP_Text>();

        RT_rectTransform = GetComponent<RectTransform>();
    }
    private void Update()
    {
        T_text.text = S_text;
        RT_rectTransform.anchoredPosition = new Vector2(RT_rectTransform.anchoredPosition.x, RT_rectTransform.anchoredPosition.y + 1f);
    }

    public void DestroySelf() 
    {
        Destroy(gameObject);
    }
}
