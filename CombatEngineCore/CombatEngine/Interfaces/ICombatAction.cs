using System.Collections.Generic;

namespace Interfaces
{
    public enum ActionTag
    {
        Attack,
        Defence,
        Fire,
        Water,
        Earth,
        Air
    }

    public interface ICombatAction
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
        //This method stores the effect of the action that is triggered regardless of the enemy action
        public void TriggerPrimaryEffect();

        //This method stores the basic effect of the action, which should be applied in case no other effect does
        public void TriggerBasicEffect();
    }
}