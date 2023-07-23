using System.Collections.Generic;
using Interfaces;

namespace Implementations
{
    public class Player : ICombatant
    {
    
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; }
    
        public ICombatAction CurrentAction { get; set; }
        public bool Dead { get; set; }
        public Dictionary<string, IStatusEffect> StatusEffects { get; set; }

        public Player(int health, int maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = health;
        }
    }
}
