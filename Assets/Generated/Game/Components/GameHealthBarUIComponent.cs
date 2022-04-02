//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public HealthBarUIComponent healthBarUI { get { return (HealthBarUIComponent)GetComponent(GameComponentsLookup.HealthBarUI); } }
    public bool hasHealthBarUI { get { return HasComponent(GameComponentsLookup.HealthBarUI); } }

    public void AddHealthBarUI(HPBarUI newHpBarUI) {
        var index = GameComponentsLookup.HealthBarUI;
        var component = (HealthBarUIComponent)CreateComponent(index, typeof(HealthBarUIComponent));
        component.hpBarUI = newHpBarUI;
        AddComponent(index, component);
    }

    public void ReplaceHealthBarUI(HPBarUI newHpBarUI) {
        var index = GameComponentsLookup.HealthBarUI;
        var component = (HealthBarUIComponent)CreateComponent(index, typeof(HealthBarUIComponent));
        component.hpBarUI = newHpBarUI;
        ReplaceComponent(index, component);
    }

    public void RemoveHealthBarUI() {
        RemoveComponent(GameComponentsLookup.HealthBarUI);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherHealthBarUI;

    public static Entitas.IMatcher<GameEntity> HealthBarUI {
        get {
            if (_matcherHealthBarUI == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.HealthBarUI);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherHealthBarUI = matcher;
            }

            return _matcherHealthBarUI;
        }
    }
}
