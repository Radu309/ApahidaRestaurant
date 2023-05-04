namespace Apahida.Models
{
    public class Order
    {
        private int id;
        private float totalPrice;
        private DateTime date;
        private int hour;
        private String status ;
        public virtual ICollection<OrderFood> Foods { get; set; }
        public Order()
        {
            this.Foods = new HashSet<OrderFood>();
            this.Hour = DateTime.Now.Hour;
            this.Date = DateTime.Now.Date;
            this.TotalPrice = 0;
            this.Status = "New Order";
        }
        public Order(int id, float totalPrice)
        {
            this.Foods = new HashSet<OrderFood>();
            this.Id = id;
            this.TotalPrice = totalPrice;
            this.Hour = DateTime.Now.Hour;
            this.Date = DateTime.Now.Date;
            this.Status = "New Order";
        }
        public int Id { get => id; set => id = value; }
        public float TotalPrice { get => totalPrice; set => totalPrice = value; }
        public DateTime Date { get => date; set => date = value; }
        public int Hour { get => hour; set => hour = value; }
        public string Status { get => status; set => status = value; }

    }
}
