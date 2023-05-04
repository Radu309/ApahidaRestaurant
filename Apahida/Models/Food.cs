namespace Apahida.Models
{
    public class Food
    {
        private int id;
        private String name;
        private float price;
        private int stock;
        public virtual ICollection<OrderFood> Orders { get; set; }

        public Food() {
            this.Orders = new HashSet<OrderFood>();
        }
        public Food(int id, string name, float price, int stock)
        {
            this.Orders = new HashSet<OrderFood>();
            this.Id = id;
            this.Name = name;
            this.Price = price;
            this.Stock = stock;
        }

        public string Name { get => name; set => name = value; }
        public float Price { get => price; set => price = value; }
        public int Stock { get => stock; set => stock = value; }
        public int Id { get => id; set => id = value; }
    }
}
