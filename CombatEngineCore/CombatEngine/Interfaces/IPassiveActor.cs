namespace Interfaces
{
    public interface IPassiveActor
    {
        public string ActorName
        {
            get;
        }
        public Target ActionTarget
        {
            get;
            set;
        }
        public void TriggerPassiveEffect(CombatEngine engine);
    }
}
