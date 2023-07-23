
using System.Collections.Generic;
using Interfaces;

namespace Implementations
{
    public class StunnedCombatAction : ICombatAction
    {
        private const string Name = "Stunned";
        private List<ActionTag> _tags = new List<ActionTag>();

        public string ActionName => Name;

        public List<ActionTag> Tags { get => _tags; set => _tags = value; }
        public CombatEngine Engine { get; set; }
        public void TriggerPrimaryEffect()
        {
            return;
        }

        public void TriggerBasicEffect()
        {
            return;
        }
    }
}
