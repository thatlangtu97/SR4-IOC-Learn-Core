using System.Collections;
using System.Collections.Generic;
using strange.extensions.signal.impl;
using UnityEngine;

public class ShowEquipmentCompareSignal : Signal<ParameterEquipmentCompare>
{
}

public class ParameterEquipmentCompare : ParameterPopup
{
    public EquipmentData leftData;
    public EquipmentData rightData;
    public CompareType compareType;
    public ParameterEquipmentCompare(){ }

    public ParameterEquipmentCompare(CompareType compareType ,EquipmentData data)
    {
        this.compareType = compareType;
        switch (compareType)
        {
            case CompareType.Left:
                this.leftData = data;
                break;
            case CompareType.Right:
                this.rightData = data;
                break;
        }
    }

    public void Clear()
    {
        leftData = null;
        rightData = null;
    }
    public enum CompareType
    {
        Left,
        Right,
    }
}
