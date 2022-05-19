using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.UI;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipPopup : AbsPopupView
{
    public Transform itemViewContainer;
    public Button btnClose;
    public RectTransform parent;
    
    public ItemView itemView;
    public Vector3 ScreenSize;
    protected override void Awake()
    {
        base.Awake();
        GameObject prefab = PrefabUtils.LoadPrefab(GameResourcePath.ITEM_VIEW);
        itemView = Instantiate(prefab, itemViewContainer).GetComponent<ItemView>();
        itemView.transform.localPosition=Vector3.zero;
        btnClose.onClick.AddListener(HidePopup);
        parent.gameObject.SetActive(false);
    }

    public override void ShowPopupByCmd()
    {
        ToolTipPopupParameter parameter = parameterPopup as ToolTipPopupParameter;
        parent.gameObject.SetActive(false);
        itemView.Show(parameter.rewardLogic);
        ScreenSize =  transform.position;
        SetupPositionFolowScreen(parameter.position);
        base.ShowPopupByCmd();
        
        ActionBufferManager.Instance.ActionDelayFrame(()=>
        {
            parent.transform.position = parameter.position;
            parent.gameObject.SetActive(true);
        }
        ,1);
    }

    protected override void OnShowPopup<T>(T parameter)
    {
        throw new System.NotImplementedException();
    }

    public void SetupPositionFolowScreen(Vector3 position)
    {
        if (position.x >= ScreenSize.x)
        {
            if (position.y >= ScreenSize.y)
            {
                parent.pivot = new Vector2(1,1);
            }
            else
            {
                parent.pivot = new Vector2(1,0);
            }
        }
        else
        {
            if (position.y >= ScreenSize.y)
            {
                parent.pivot = new Vector2(0,1);
                
            }
            else
            {
                parent.pivot = new Vector2(0,0);
            }
        }
    }
    
}
