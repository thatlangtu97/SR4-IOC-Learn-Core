using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "DataZone", menuName = "CoreGame/ZoneData")]
public class ZoneData : SerializedScriptableObject
{
//    public bool tutorialMap;
    public List<WaveData> waveList;
    public List<LevelData> levelList;
//    public List<GameObject> mapList;
//    public List<GameObject> mapListTutorial;
//
//    public float bonusLevelAppearPercent;
//    public float incrementalBonusStage;
//    public int maxBonusStage;
//    public float healDropRate;
//    public float incrementalHealDropRate;
//
//    //public BonusData bonus;
//    public List<int> openWallLevels;
//    //public List<int> passiveGetLevels;
//    public List<int> changeBackGroundLevels;
//    public List<int> introLevels;
//    //public List<ArtifactConfig> artifactConfigs;
//    public List<Intro> introBoss;
//    public List<Sprite> bossIcon;
//    public int numberOfStageToInit;
//    public List<int> initEnemyPerStage;
//
//    public int numberOfStageToInitBoss;
//    public List<int> initEnemyWithBossStage;
//
//    //public List<Tape> dialogueSequenceList;
    public List<GameObject> enemyList;
    [Button("SetupEnemy", ButtonSizes.Gigantic), GUIColor(0.4f, 0.8f, 1),]
    void FindEnemyPrefab()
    {
        enemyList = new List<GameObject>();
        foreach (var VARIABLE in waveList)
        {
            GameObject prefab = Resources.Load<GameObject>("PrefabCharacter/" + VARIABLE.EnemyID);
            if (!enemyList.Contains(prefab))
            {
                enemyList.Add(prefab);
            }
        }
    }
}

[Serializable]
public class WaveData
{
    public string WaveID;
    public string EnemyID;
    public int Amount;
    public float Time;
    public float TimeSpawnMin;
    public float TimeSpawnMax;
}

[Serializable]
public class LevelData
{
    public string LevelID;
    public float atkScale;
    public float hpScale;
    public float goldScale;
    public float hpReduceDropChance;
    public float EXP;
    public float crystalScale;
    public string[] WaveIDs;
}


[System.Serializable]
public struct Intro
{
    public float delay;
    public GameObject prefab;
    public float duration;
}

