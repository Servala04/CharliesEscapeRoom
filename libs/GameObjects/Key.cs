namespace libs
{
    public class Key : GameObject
    {
        public Key() : base()
        {
//Original Key - replaced with NPC
            Type = GameObjectType.Key;
            CharRepresentation =  '⚇';//ᴥ⎈⏳⚇☺❔
            Color = ConsoleColor.DarkGreen;
        }
    }
}
