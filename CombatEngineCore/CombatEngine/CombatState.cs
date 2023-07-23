using System.Collections.Generic;
using Implementations;
using Interfaces;

public class CombatState
{
    public Player Player;
    public Enemy Enemy;
    public Dictionary<string, IPassiveActor> PassiveActors;
    public ICombatGoal CombatGoal;
    public int TurnCounter;

    public void Reset()
    {
        Player = null;
        Enemy= null;
        PassiveActors = null;
        CombatGoal = null;
        TurnCounter = 0;
    }
}
