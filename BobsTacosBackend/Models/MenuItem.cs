namespace BobsTacosBackend.Models
{
    public class MenuItem
    {
        public int id { get; set; }
        public string name { get; set; }

        public float price { get; set; }

        public int rating { get; set; }

        public string foodType { get; set; }

        public string description { get; set; }
        public int deliveryTime { get; set; }
        public string image {  get; set; }
    }
}