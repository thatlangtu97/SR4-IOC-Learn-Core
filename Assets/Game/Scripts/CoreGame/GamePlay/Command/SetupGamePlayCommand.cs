using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.impl;
using UnityEngine;

public class SetupGamePlayCommand : Command
{
    public override void Execute()
    {
        EnemySpawnController.Instance.Setup();
    }
}
