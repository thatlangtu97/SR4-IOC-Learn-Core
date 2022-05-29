using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentCompareView : AbsPopupView
{
    public EquipmentDetailView leftDetailView, rightDetailView;
    private ParameterEquipmentCompare param;
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }
    
    
    public override bool EnableBack()
    {
        return true;
    }

    public override void Hide()
    {       
        if(leftDetailView.gameObject.activeInHierarchy)
            leftDetailView.Hide();
        if(rightDetailView.gameObject.activeInHierarchy)
            rightDetailView.Hide();
        base.Hide();
    }

    protected override void OnShowPopup<T>(T parameter)
    {
        param = parameter as ParameterEquipmentCompare;
        switch (param.compareType)
        {
            case ParameterEquipmentCompare.CompareType.Left:
                if (param.leftData != null)
                {
                    leftDetailView.SetupData(param.leftData);
                    //leftDetailView.gameObject.SetActive(true);
                    ActionBufferManager.Instance.ActionDelayFrame(
                        delegate
                        {
                            leftDetailView.Show();
                        },1 );
                }
                break;
            case ParameterEquipmentCompare.CompareType.Right:
                if (param.rightData != null)
                {
                    rightDetailView.SetupData(param.rightData);
                    //rightDetailView.gameObject.SetActive(true);
                    ActionBufferManager.Instance.ActionDelayFrame(
                        delegate
                        {
                            rightDetailView.Show();
                        },1 );
                }
                break;
        }
    }
}
