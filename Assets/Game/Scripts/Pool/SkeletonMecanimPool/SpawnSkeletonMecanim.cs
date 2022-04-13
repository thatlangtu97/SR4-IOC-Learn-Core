using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using Unity.Mathematics;
using UnityEngine;

public class SpawnSkeletonMecanim : MonoBehaviour
{
    public SkeletonMecanim mecanimPrefab;
    public StateMachineController controller;
    public ComponentManager componentManager;
    public Vector3 localScale;
    private void Start()
    {
        SkeletonMecanim tempMecanim = PoolManager.Spawn(mecanimPrefab,controller.transform,Vector3.zero,quaternion.identity, localScale);
        controller.animator = tempMecanim.GetComponent<Animator>();
        componentManager.meshRenderer = tempMecanim.GetComponent<MeshRenderer>();
        controller.SetupAnim(controller.animator);
    }
}
