namespace SpaceGame
{
    public enum InteractionType
    {
        None,
        Absorption, // первая заменяет вторую карту
        Destroys, // уничтожает следующую карту
        Bonus, // дает бонус к очкам
    }
}