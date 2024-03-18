namespace BobsTacosBackend.Models
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public float Price { get; set; }

        public int Rating { get; set; }

        public string FoodType { get; set; }

        public string Description { get; set; }
        public int deliverytime { get; set; }


    }
}
