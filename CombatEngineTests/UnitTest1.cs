using Implementations;
using Implementations.CombatGoals;
using Implementations.Enemies;

namespace CombatEngineTests;

public class Tests
{
    private CombatEngine _engine;
    [SetUp]
    public void Setup()
    {
        CombatEngine engine = new CombatEngine();
        TestKillGoal goal = new TestKillGoal(engine);
        engine.StartCombat(new Player(5,5), new GoblinGrunt(), goal);
        _engine = engine;
    }

    [Test]
    public void RunTests()
    {
        Assert.Pass();
    }
    [Test]
    public void CopyTest()
    {
        var savedState = _engine.GetTargetHealth(Target.Enemy);
        _engine.DamageCombatant(Target.Enemy, 1);
        Assert.AreEqual(5, savedState);
        Assert.AreEqual(4,_engine.GetTargetHealth(Target.Enemy));
    }

    [Test]
    public void TestKillGoal()
    {
        _engine.DamageCombatant(Target.Enemy, 5);
        Assert.AreEqual(1, _engine.GetGoalValue());
    }
}