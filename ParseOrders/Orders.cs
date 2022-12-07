using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseOrders
{
    internal class Orders
    {
        private readonly List<Order> _orders;

        public Orders()
        {
            _orders = new List<Order>();
        }

        public void ParseFile(string fileName)
        {
            using StreamReader stream = new StreamReader(fileName);
            while (!stream.EndOfStream)
            {
                var order = Order.Create(stream);
                _orders.Add(order);
            }
        }

        public List<Order> OrderList => _orders;
    }
}
