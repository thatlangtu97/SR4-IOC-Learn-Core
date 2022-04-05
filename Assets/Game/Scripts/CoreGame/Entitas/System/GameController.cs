using System;
using Entitas;
using BehaviorDesigner.Runtime;
using UnityEngine;
//using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.SceneManagement;
using System.Collections;
using Sirenix.OdinInspector;

public class GameController : MonoBehaviour
{
    Systems CharacterSystems;
    Systems GameSystem;
    public static GameController instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        Application.targetFrameRate = -1;
//        var contexts = Contexts.sharedInstance;
//        GameSystem = new Feature("Game System")
//            .Add(new StateMachineUpdateSystem(contexts))
//            .Add(new TakeDamageSystem(contexts))
//            .Add(new ProjectileMoveBezierSystem(contexts))
//            .Add(new DamageTextSystem(contexts))
//            .Add(new HealthBarUpdateSystem(contexts))
//            ;
//        GameSystem.Initialize();
        
        SetupSystem();
    }
    [Button("SetupSystem", ButtonSizes.Gigantic), GUIColor(0.4f, 0.8f, 1),]

    public void SetupSystem()
    {
        var contexts = Contexts.sharedInstance;
        GameSystem = new Feature("Game System")
                .Add(new StateMachineUpdateSystem(contexts))
                .Add(new TakeDamageSystem(contexts))
                .Add(new ProjectileMoveBezierSystem(contexts))
                .Add(new DamageTextSystem(contexts))
                .Add(new HealthBarUpdateSystem(contexts))
            ;
        DealDmgManager.context = contexts;
        DamageTextManager.context = contexts;
        ObjectPool.instance.CreatePoolEntity(contexts,100);
        GameSystem.Initialize();
    }
    void Start()
    {
        if (PlayFlashScene.instance != null)
        {
            PlayFlashScene.instance.HideLoading();
        }
    }
    private void OnDestroy()
    {
        //ExitGameScene();
    }
    void Update()
    {
        
        //GameSystem.Cleanup();
    }

    private void FixedUpdate()
    {
        if(GameSystem!=null)
            GameSystem.Execute();  
    }

    private void LateUpdate()
    {
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

