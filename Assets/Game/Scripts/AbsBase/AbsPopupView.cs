using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbsPopupView : View
{
	[Inject] public PopupManager popupManager { get; set; }
	public UiViewController UiViewController;
	public ParameterPopup parameterPopup;
	public Button[] closeBtns;
	
	AutoFIllPanelInParent autoFIllPanelInParent;
	public void SetParameter(ParameterPopup parameterPopup)
	{
		this.parameterPopup = parameterPopup;
	}

	protected override void Awake()
	{
		base.Awake();
		base.CopyStart();
		SetClose();
	}

	void SetClose()
	{
		foreach (var btn in closeBtns)
		{
			btn.onClick.AddListener(Hide);
		}
	}

	protected override void Start()
	{
		base.Start();
//		if (uILayer != UILayer.NODE)
//		{
//			transform.parent = popupManager.GetUILayer(uILayer);
//			autoFIllPanelInParent = GetComponent<AutoFIllPanelInParent>();
//			autoFIllPanelInParent.AutoFill();
//		}
	}
//	public virtual void ShowPopupByCmd()
//	{
//		base.CopyStart();
//		NotifyShowPopup();
//		popupManager.ShowPopup(this);
//	}
//	public virtual void ShowPopup()
//	{
//		UiViewController.Show();
//	}
//	public void HidePopup()
//	{
//		UiViewController.Hide();
//	}
	public void Hide()
	{
		UiViewController.Hide();
	}
	public void NotifyShowPopup()
	{

	}

	public abstract bool EnableBack();

	public void ShowPopup<T>(T parameter) where T : ParameterPopup
	{
		NotifyShowPopup();
		OnShowPopup(parameter);
//		if (!(popupManager.GetCurrentPopup() == this.GetType()))
//		{
//			UiViewController.Show();
//		}
		popupManager.ShowPopup(this);
		UiViewController.Show();
	}
	protected abstract void OnShowPopup<T>(T parameter) where T : ParameterPopup;
}
public class ParameterPopup
{

}
