using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Apahida.Models
{
    public class OrderFood
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int FoodId { get; set; }
        public Order Order { get; set; }
        public Food Food { get; set; }

        public OrderFood(int orderId, int foodId)
        {
            this.OrderId = orderId;
            this.FoodId = foodId;
        }

    }
}
