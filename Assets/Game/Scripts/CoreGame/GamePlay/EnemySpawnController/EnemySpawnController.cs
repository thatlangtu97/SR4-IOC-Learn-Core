using System.Collections;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using UnityEngine;

public class EnemySpawnController : View
{
    private static EnemySpawnController instance;
    public static EnemySpawnController Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject dataManager = new GameObject();
                dataManager.name = "EnemySpawnController";
                instance = dataManager.AddComponent<EnemySpawnController>();
                //DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }

    protected override void Awake()
    {
        base.Awake();
    }
//    public void SpawnEnemyInited(int currentLevel) {
//        if (LevelMapConfigManager.instance.isInBonusLevel)
//        {
//            StartCoroutine(SpawnBonuEnemyInited(LevelMapConfigManager.instance.currentZone * -1));
//            //SpawnBonusEnemyInited();
//            return;
//        }         
//        if (dictionaryWave.ContainsKey(currentLevel))
//        {
//            List<GameObject> listEnemy = dictionaryWave[currentLevel];
//            WaveData data = dictionaryWaveData[currentLevel];
//
//            //GameObject prefab;
//            SpawnPosType postype = SpawnPosType.Random;
//            //enemyPrefabsDict.TryGetValue(data.EnemyID, out prefab);
//            //Debug.Log(prefab + ":" + data.EnemyID + " " + listEnemy.Count + LevelMapConfigManager.instance.currentLevel);
//            
//            //Debug.Log(data.Amount);
//            LevelCreator.instance.currEnemyCount += listEnemy.Count;
//            EnemyConfigData enemyconfigdata;
//            enemyDataDict.TryGetValue(data.EnemyID, out enemyconfigdata);
//            float spawnTime = 0;
//            
//            for(int i=0;i< listEnemy.Count; i++)
//            {
//               
//                SpawnTypeContainer spawncontain = listEnemy[i].GetComponent<SpawnTypeContainer>();
//                postype = spawncontain.posType;
//                Vector2 pos = randomPos(postype, enemyconfigdata.Minspawn, enemyconfigdata.Maxspawn,spawncontain.offsetX);
//                
//                //Debug.Log(listEnemy[i].name + " " +data.Amount + " " + listEnemy.Count);
//
//
//                float timedelay = Random.Range(data.TimeSpawnMin, data.TimeSpawnMax);
//                spawnTime += timedelay;
//                StartCoroutine(DelaySpawnEnemyInited(spawnTime, listEnemy[i], pos));
//
//                GameObject fxspawn;
//                if (postype == SpawnPosType.Air)
//                {
//                    //fxspawn = spawnOnAirFx;
//                }
//                else if (postype == SpawnPosType.Door_Air_Left)
//                {
//                    fxspawn = airSpawnFx;
//                    //StartCoroutine(delaySpawnFx(spawnTime - 0.3f, prefab, /*pos*/listEnemy[i].transform.position, enemyconfigdata.Size, fxspawn));
//                    StartCoroutine(DelaySpawnFxForEnemyInit(spawnTime - 0.3f, pos/*listEnemy[i].transform.position*/, enemyconfigdata.Size, fxspawn));
//                }
//                else
//                {
//                    fxspawn = spawnFx;
//                    //StartCoroutine(delaySpawnFx(spawnTime - 0.3f, prefab, /*pos*/listEnemy[i].transform.position, enemyconfigdata.Size, fxspawn));
//                    StartCoroutine(DelaySpawnFxForEnemyInit(spawnTime - 0.3f, pos/*listEnemy[i].transform.position*/, enemyconfigdata.Size, fxspawn));
//             
//                }
//
//
//
//            }
//
//        }
//    }
    
}
