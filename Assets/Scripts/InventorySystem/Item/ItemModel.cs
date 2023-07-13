namespace InventorySystem.Item
{
    public class ItemModel
    {
        public int Id { get; set; }
        
        public ItemType ItemType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Speed { get; set; }
        public int Health { get; set; }
        
        public string SpritePath { get; set; }
    }
}