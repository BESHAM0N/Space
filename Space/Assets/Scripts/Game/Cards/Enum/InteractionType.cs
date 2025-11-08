namespace SpaceGame
{
    public enum InteractionType
    {
        Non,
        Bonus,          // даёт бонус к очкам
        Destroys,       // уничтожает следующую карту
        Absorption    // первая заменяет вторую карту
    }
}