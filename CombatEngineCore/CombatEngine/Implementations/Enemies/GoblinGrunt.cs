using Interfaces;

namespace Implementations.Enemies
{
    public class GoblinGrunt : Enemy
    {
        public GoblinGrunt()
        {
            CurrentHealth = 5; 
            MaxHealth = 5;
        }

    public override void SelectAction()
        {
            throw new System.NotImplementedException();
        }
    }
}
