using System;
using System.Collections.Generic;
using System.Linq;
using Implementations;
using Interfaces;

public enum Target
{
    Player,
    Enemy,
    All
}

public delegate void CombatEngineEventHandler(CombatEngine sender);

public class CombatEngine
{
    public event CombatEngineEventHandler OnPlayerVictory;
    public event CombatEngineEventHandler OnPlayerLoss;
    public event CombatEngineEventHandler OnCombatStart;
    public event CombatEngineEventHandler OnCombatEnd;
    public event CombatEngineEventHandler OnTurnStart;
    public event CombatEngineEventHandler OnCombatantDead;
    public event CombatEngineEventHandler OnStateUpdate;

    private CombatState _state;

    public CombatEngine()
    {
        _state = new CombatState();
    }

    public void StartCombat(Player player, Enemy enemy, ICombatGoal goal, bool resetState=true)
    {
        if (resetState)
        {
            _state.Reset();
            _state.Player = player;
            _state.Enemy = enemy;
            _state.CombatGoal = goal;
        }
        
        OnCombatStart?.Invoke(this);
    }

    private ICombatant SelectTarget(Target target)
    {
        switch (target)
        {
            case Target.Player:
                return _state.Player;
            case Target.Enemy:
                return _state.Enemy;
            default:
                return null;
        }
    }

    private void TriggerStatusEffects(ICombatant combatant)
    {
        foreach (var effect in combatant.StatusEffects)
        {
            effect.Value.TriggerEffect();
        }
        OnStateUpdate?.Invoke(this);
    }

    private void TriggerEffectsOfType(ICombatant combatant, EffectType type)
    {
        foreach (var effect in combatant.StatusEffects)
        {
            if (type == effect.Value.Type)
            {
                effect.Value.TriggerEffect();
            }
        }
        OnStateUpdate?.Invoke(this);
    }
    //This method damages a combatant. If the damage brings them to 0 health or below, Dead flag is set to true.
    public void DamageCombatant(Target target, int damage)
    {
        ICombatant combatant = SelectTarget(target);
        combatant.CurrentHealth -= damage;
        if (combatant.CurrentHealth <= 0)
        {
            combatant.Dead = true;
            OnCombatantDead?.Invoke(this);
        }
        OnStateUpdate?.Invoke(this);
    }
    //This method can be used to change the combatant's health non-lethally. Cannot increase health past max value or reduce it below one.
    public void ChangeHealth(Target target, int change)
    {
        ICombatant combatant = SelectTarget(target);
        combatant.CurrentHealth += change;
        if (combatant.CurrentHealth <= 0)
        {
            combatant.CurrentHealth = 1;
        }

        if (combatant.CurrentHealth > combatant.MaxHealth)
        {
            combatant.CurrentHealth = combatant.MaxHealth;
        }
        OnStateUpdate?.Invoke(this);
    }

    public void ApplyStatusEffect(Target target, IStatusEffect effect)
    {
        ICombatant combatant = SelectTarget(target);
        if (combatant.StatusEffects.ContainsKey(effect.Name))
        {
            combatant.StatusEffects[effect.Name].Value += effect.Value;
            effect.Apply();
            return;
        }
        combatant.StatusEffects[effect.Name] = effect;
        effect.Apply();
        OnStateUpdate?.Invoke(this);
    }


    public void RemoveStatusEffect(Target target, string effectName)
    {
        ICombatant combatant = SelectTarget(target);
        if (combatant.StatusEffects.ContainsKey(effectName))
        {
            combatant.StatusEffects[effectName].EndEffect();
            combatant.StatusEffects.Remove(effectName);
        }
        OnStateUpdate?.Invoke(this);
    }
    
    //Invokes the OnCombatEnd event, then resets the combat state.
    public void EndCombat()
    {
        OnCombatEnd?.Invoke(this);
        _state.Reset();
    }

    private void CombatEndCheck()
    {
        if (_state.Player.Dead)
        {
            OnPlayerLoss?.Invoke(this);
            EndCombat();
        }
        else if (_state.CombatGoal.CheckGoal())
        {
            OnPlayerVictory?.Invoke(this);
            EndCombat();
        }
    }

    public void SetPlayerAction(ICombatAction action)
    {
        _state.Player.CurrentAction = action;
    }

    public void SetEnemyAction(IEnemyAction action = null)
    {
        if (action==null)
        {
            _state.Enemy.SelectAction();
        }
        else
        {
            _state.Enemy.CurrentAction = action;
        }
    }

    public bool ResolveReady()
    {
        if (_state.Player.CurrentAction != null && _state.Enemy.CurrentAction != null)
        {
            return true;
        }

        return false;
    }

    public void ResolveAction()
    {
        if (ResolveReady())
        {
            _state.Enemy.CurrentAction.TriggerEffect(); 
        }
        else
        {
            throw new NullReferenceException("Engine tried to resolve a null action");
        }

        _state.Enemy.CurrentAction = null;
        _state.Player.CurrentAction = null;


    }
    
    //This method starts a new turn, incrementing the turn counter, triggering all applied status effects and causing passive actors to trigger their effects.
    public void StartTurn()
    {
        
        TriggerStatusEffects(_state.Player);
        
        TriggerStatusEffects(_state.Enemy);
        foreach (var actor in _state.PassiveActors)
        {
            actor.Value.TriggerPassiveEffect(this);
        }
        CombatEndCheck();
        _state.TurnCounter++;
        OnTurnStart?.Invoke(this);
    }

    public int GetTargetHealth(Target target)
    {
        switch (target)
        {
            case Target.Player:
                return _state.Player.CurrentHealth;
            case Target.Enemy:
                return _state.Enemy.CurrentHealth;
            default:
                throw new ArgumentException("Abnormal parameter of type Target in GetTargetHealth call");
        }
    }
    
    public int GetTargetMaxHealth(Target target)
    {
        switch (target)
        {
            case Target.Player:
                return _state.Player.MaxHealth;
            case Target.Enemy:
                return _state.Enemy.MaxHealth;
            default:
                throw new ArgumentException("Abnormal parameter of type Target in GetTargetMaxHealth call");
        }
    }

    public string GetGoalDescription()
    {
        return _state.CombatGoal.Description;
    }

    public int GetGoalValue()
    {
        return _state.CombatGoal.CurrentGoalValue;
    }

    public int GetGoalTargetValue()
    {
        return _state.CombatGoal.GoalValue;
    }

    public List<string> GetTargetConditionNames(Target target)
    {
        List<string> conditions = new List<string>();
        switch (target)
        {
            case Target.Player:
                foreach (var condition in _state.Player.StatusEffects)
                {
                    conditions.Add(condition.Key);
                }

                return conditions;
            case Target.Enemy:
                foreach (var condition in _state.Player.StatusEffects)
                {
                    conditions.Add(condition.Key);
                }

                return conditions;
            default:
                throw new ArgumentException("Abnormal parameter of type Target in GetTargetCondition call");
        }
    }

    public int GetConditionValueOnTarget(Target target, string conditionName)
    {
        switch (target)
        {
            case Target.Player:
                foreach (var condition in _state.Player.StatusEffects)
                {
                    if (condition.Key == conditionName)
                    {
                        return condition.Value.Value;
                    }
                }

                return 0;
            case Target.Enemy:
                foreach (var condition in _state.Enemy.StatusEffects)
                {
                    if (condition.Key == conditionName)
                    {
                        return condition.Value.Value;
                    }
                }

                return 0;
            default:
                throw new ArgumentException("Abnormal parameter of type Target in GetConditionValueOnTarget call");
        }
    }

    public bool GetTargetDead(Target target)
    {
        switch (target)
        {
            case Target.Player:
                return _state.Player.Dead;
            case Target.Enemy:
                return _state.Enemy.Dead;
            default:
                throw new ArgumentException("Abnormal parameter of type Target in GetTargetDead call");
        }
    }

    public List<string> GetPassiveActorNames()
    {
        List<string> actors = new List<string>();
        foreach (var actor in _state.PassiveActors)
        {
            actors.Add(actor.Key);
        }

        return actors;
    }

}
