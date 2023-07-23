
using System.Collections.Generic;

namespace Interfaces
{
    public abstract class Enemy : ICombatant
    {
        private int _currentHealth;
        private int _maxHealth;
        private bool _dead;
        public IEnemyAction CurrentAction { get; set; }
        private Dictionary<string, IStatusEffect> _statusEffects;

        public string ID { get; }

        public int CurrentHealth
        {
            get => _currentHealth;
            set => _currentHealth = value;
        }

        public int MaxHealth
        {
            get { return _maxHealth; }
            set { _maxHealth = value; }
        }


        public bool Dead
        {
            get => _dead;
            set => _dead = value;
        }

        public Dictionary<string, IStatusEffect> StatusEffects
        {
            get => _statusEffects;
            set => _statusEffects = value;
        }

        public abstract void SelectAction();

    }
}
