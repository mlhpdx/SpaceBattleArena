namespace SpaceBattleArena
{
    public abstract class BasicSpaceship : Spaceship<BasicGameInfo>
    {
        abstract public RegistrationData registerShip(int numImages, int worldWidth, int worldHeight);

        abstract public ShipCommand getNextCommand(BasicEnvironment env);

        public ShipCommand getNextCommand(Environment<BasicGameInfo> env)
        {
            return getNextCommand(new BasicEnvironment(env));
        }

        public void shipDestroyed(string lastDestroyedBy) { }
    }
}