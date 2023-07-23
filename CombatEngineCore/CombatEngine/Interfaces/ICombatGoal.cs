namespace Interfaces
{
    public enum GoalType
    {
        TurnCount,
        EnemyDead,
        SpecialAction
    }
    public interface ICombatGoal
    {
        int GoalValue
        {
            get;
            set;
        }

        int CurrentGoalValue
        {
            get;
            set;
        }

        GoalType Type
        {
            get;
        }

        string Description
        {
            get;
        }
    
        //This method is used to determine whether the current combat state fullfills the requirements for the player to win combat.
        public bool CheckGoal();
    }
}