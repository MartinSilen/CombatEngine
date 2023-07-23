using System.Collections.Generic;

namespace Interfaces
{
    public interface IEnemyAction
    {
        string ActionName
        {
            get;
        }
    
        List<ActionTag> Tags
        {
            get;
            set;
        }
    

        CombatEngine Engine
        {
            get;
            set;
        }

        //This method decides the effect of the enemy's action, based on the current combat state
        public void TriggerEffect();
    }
}
