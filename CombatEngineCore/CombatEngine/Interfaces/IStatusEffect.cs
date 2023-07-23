namespace Interfaces
{
    public enum EffectType
    {
        Burning,
        Poisoned,
        Stunned
    }
    public interface IStatusEffect
    {
        //Unique identifier of a specific effect, should be unique for each
        string Name
        {
            get;
            set;
        }
        //General effect type, used to identify all effects of the same type
        EffectType Type
        {
            get;
        }
        //The combat participant, to whom the effect is applied
        ICombatant Owner
        {
            get;
            set;
        }
    
        //Numerical value signifying the magnitude of this effect instance
        int Value
        {
            get;
            set;
        }

        public void Apply();
        public void TriggerEffect();
        public void EndEffect();
    }
}