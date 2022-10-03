using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PromptMessageHandler : UIController
{
    public TextMeshProUGUI textMessage;

    private void Start()
    {
        StartCoroutine(FadeOut(GetComponent<CanvasGroup>(), 1.2f));
    }
}
