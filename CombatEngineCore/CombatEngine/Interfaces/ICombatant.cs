using System.Collections.Generic;

namespace Interfaces
{
    public enum Team
    {
        Player,
        Enemy
    }
    public interface ICombatant
    {
        int CurrentHealth
        {
            get;
            set;
        }

        int MaxHealth
        {
            get;
        }

        bool Dead
        {
            get;
            set;
        }

        Dictionary<string, IStatusEffect> StatusEffects
        {
            get;
            set;
        }
    }
}