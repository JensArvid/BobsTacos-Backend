namespace BobsTacosBackend.Models
{
    public class WishlistDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ICollection<MenuItemDto> MenuItems { get; set; }
    }

    public class MenuItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Rating { get; set; }
        public string FoodType { get; set; }
        public string Description { get; set; }
        public int DeliveryTime { get; set; }
        public string Image { get; set; }
    }
}
