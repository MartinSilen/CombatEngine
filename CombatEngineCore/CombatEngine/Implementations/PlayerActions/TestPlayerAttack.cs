using System.Collections.Generic;
using Interfaces;

namespace Implementations.PlayerActions;

public class TestPlayerAttack : ICombatAction
{
    public string ActionName { get; }
    public List<ActionTag> Tags { get; set; }
    public CombatEngine Engine { get; set; }
    
    
    public void TriggerPrimaryEffect()
    {
        throw new System.NotImplementedException();
    }

    public void TriggerBasicEffect()
    {
        throw new System.NotImplementedException();
    }
}