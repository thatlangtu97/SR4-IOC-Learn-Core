using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RewardGameplayPopup : AbsPopupView
{
    
    
    public Button backToHomeBtn;
    protected override void Awake()
    {
        base.Awake();
        backToHomeBtn.onClick.AddListener(BackToHome);
    }

    public void BackToHome()
    {
        //SceneManager.LoadScene("HomeScene");
        PlayFlashScene.instance.ShowLoading();
        ActionBufferManager.Instance.ActionDelayTime(()=> SceneManager.LoadScene("HomeScene"),1.2f);
    }
}