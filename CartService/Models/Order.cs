namespace CartService.Models
{
    public class Order
    {
        public int CustId { get; set; }
        public int ProdId { get; set; }
        public int Qty { get; set; }

        public Order(int custId, int prodId, int qty)
        {
            CustId = custId;
            ProdId = prodId;
            Qty = qty;
        }
    }
}
