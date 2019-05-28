namespace SpaceBattleArena
{
    public interface Spaceship<T>
    {
        RegistrationData registerShip(int numImages, int worldWidth, int worldHeight);

        ShipCommand getNextCommand(Environment<T> env);

        void shipDestroyed(string lastDestroyedBy);

        //public void processError(ErrorData error);
    }
}