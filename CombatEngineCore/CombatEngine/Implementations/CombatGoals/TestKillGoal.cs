using Interfaces;

namespace Implementations.CombatGoals;

public class TestKillGoal : ICombatGoal
{
    
    public int GoalValue { get; set; }
    public int CurrentGoalValue { get; set; }
    public GoalType Type { get; }
    public string Description { get; }

    private CombatEngine _engine;

    public TestKillGoal(CombatEngine engine)
    {
        GoalValue = 1;
        CurrentGoalValue = 0;
        Type = GoalType.EnemyDead;
        Description = "Defeat the Enemy!";
        _engine = engine;
        _engine.OnCombatantDead += HandleCombatantDeath;
    }

    private void HandleCombatantDeath(CombatEngine engine)
    {
        if (_engine.GetTargetDead(Target.Enemy))
        {
            CurrentGoalValue++;
        }
    }
    public bool CheckGoal()
    {
        if (CurrentGoalValue >= GoalValue)
        {
            return true;
        }

        return false;
    }
}