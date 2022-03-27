using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor.Drawers;
using UnityEngine;
[CreateAssetMenu(fileName = "PassiveCollection", menuName = "CoreGame/PassiveCollection")]
public class PassiveCollection : SerializedScriptableObject
{
    [ListDrawerSettingsAttribute(ListElementLabelName = "name", ShowIndexLabels =false,ShowPaging = false,DraggableItems = false)]
    [HideReferenceObjectPicker]
    public List<BasePassive> Passives;
}
