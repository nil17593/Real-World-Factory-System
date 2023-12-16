
namespace HolyCow.FactoryGame
{
    // Define an interface for Factory behavior
    public interface IFactory
    {
        void ProduceGems();
        int GetCurrentLevel();
        void UpgradeFactory();
    }
}