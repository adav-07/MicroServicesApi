namespace CustomerServices.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Customer(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
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
