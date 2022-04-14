using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "StateMachineCollection", menuName = "CoreGame/Collection/StateMachineCollection")]
public class StateMachineCollection : ScriptableObject
{
    public List<StateClone> States;
    public Dictionary<NameState, State> dictionaryState = new Dictionary<NameState, State>();

    public void Create()
    {
        foreach (StateClone tempState in States) {
            CreateStateFactory(tempState);
        }
    }
    protected void CreateStateFactory(StateClone stateClone)
    {
        State state = Instantiate(stateClone.StateToClone);
        if (!dictionaryState.ContainsKey(stateClone.NameState))
        {
            dictionaryState.Add(stateClone.NameState, state);
        }
        else
        {
            dictionaryState[stateClone.NameState] = state;
        }
    }
}
