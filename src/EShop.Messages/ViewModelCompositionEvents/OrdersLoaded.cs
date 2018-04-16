namespace EShop.Messages.ViewModelCompositionEvents
{
    using System.Collections.Generic;
    public class OrdersLoaded
    {
        public IDictionary<dynamic, dynamic> OrdersDictionary { get; set; }
    }
}
