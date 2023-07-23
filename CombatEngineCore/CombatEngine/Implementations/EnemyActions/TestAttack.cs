using System.Collections.Generic;
using Interfaces;

namespace Implementations.EnemyActions
{
    public class TestAttack : IEnemyAction
    {
        public string ActionName => "TestAttack";
        public List<ActionTag> Tags { get; set; }
        public CombatEngine Engine { get; set; }
    
        public TestAttack(CombatEngine engine)
        {
            Engine = engine;
            Tags = new List<ActionTag>() { ActionTag.Attack };
        }
        public void TriggerEffect()
        {
            Engine.DamageCombatant(Target.Player, 1);
        }

    }
}
