using System;
using Entitas;
using BehaviorDesigner.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using EntrySystem;
using Sirenix.OdinInspector;
using strange.extensions.mediation.impl;

public class GameController : View
{
    public static GameController instance;

    public bool isPlaying = true;
    //Systems CharacterSystems;
    private Systems GameSystem;
    private StateMachineUpdateSystem stateMachineUpdateSystem;
    private TakeDamageSystem takeDamageSystem;
    private ProjectileMoveBezierSystem projectileMoveBezierSystem;
    private DamageTextSystem damageTextSystem;
    private HealthBarUpdateSystem healthBarUpdateSystem;
    private Contexts contexts;


    public bool testloadFlashScene = false;
    private void Awake()
    {
        EntryContextView.Instance.loadFlashScene=testloadFlashScene;
        if (instance == null)
        {
            instance = this;
        }
        
        Application.targetFrameRate = -1;

    }

    public void InitUI()
    {
        GameObject UI1 = Resources.Load<GameObject>(GameResourcePath.UI1);
        GameObject UI2 = Resources.Load<GameObject>(GameResourcePath.UI2);
        Instantiate(UI1);
        Instantiate(UI2);
    }
    public void CreateSystem()
    {
        contexts = Contexts.sharedInstance;
        stateMachineUpdateSystem = new StateMachineUpdateSystem(contexts);
        takeDamageSystem = new TakeDamageSystem(contexts);
        projectileMoveBezierSystem = new ProjectileMoveBezierSystem(contexts);
        damageTextSystem = new DamageTextSystem(contexts);
        healthBarUpdateSystem = new HealthBarUpdateSystem(contexts);
    }
    
    [Button("SetupSystem", ButtonSizes.Gigantic), GUIColor(0.4f, 0.8f, 1),]
    public void SetupSystem()
    {

        GameSystem = new Feature("GameSystem")
                .Add(stateMachineUpdateSystem)
                .Add(takeDamageSystem)
                .Add(projectileMoveBezierSystem)
                .Add(damageTextSystem)
                .Add(healthBarUpdateSystem)
            ;
        DealDmgManager.context = contexts;
        DamageTextManager.context = contexts;
        GameSystem.Initialize();
    }
    void Start()
    {
        if (PlayFlashScene.instance != null)
        {
            PlayFlashScene.instance.HideLoading();
        }
        CreateSystem();
        SetupSystem();
        InitUI();
        Debug.Log("start game controller");

    }
    private void OnDestroy()
    {
        //ExitGameScene();
    }
    void Update()
    {
        if(isPlaying)
            if(GameSystem!=null)
                GameSystem.Execute();
        //GameSystem.Cleanup();
    }

//    private void FixedUpdate()
//    {
//        if(GameSystem!=null)
//            GameSystem.Execute();  
//    }

    private void LateUpdate()
    {
        if(isPlaying)
            if(GameSystem!=null)
                GameSystem.Cleanup();  
    }
    public void ReloadScene()
    {
        /*
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        var contexts = Contexts.sharedInstance;
        contexts.game.DestroyAllEntities();
        contexts.Reset();       
        Contexts.sharedInstance = new Contexts();
        */

        StartCoroutine(delayReloadScene());
    }
    public void ExitGameScene()
    {
        /*
        GameSystem.TearDown();
        GameSystem.DeactivateReactiveSystems();
        GameSystem.ClearReactiveSystems();
        var contexts = Contexts.sharedInstance;
        contexts.game.DestroyAllEntities();        
        contexts.Reset();
        */
        ComponentManagerUtils.ResetAll();
        //Contexts.sharedInstance.Reset();
        Contexts.sharedInstance = new Contexts();
    }
    public void BackToHome()
    {
        PlayFlashScene.instance.Loading("HomeScene", 1.2f, ExitGameScene);
    }
    IEnumerator delayReloadScene()
    {
        if(PlayFlashScene.instance!=null)
            PlayFlashScene.instance.ShowLoading();
        ComponentManagerUtils.ResetAll();
        //Contexts.sharedInstance.Reset();
        Contexts.sharedInstance = new Contexts();
        yield return new WaitForSeconds(1.2f);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);

        

    }
}

