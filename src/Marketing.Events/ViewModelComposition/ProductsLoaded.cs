﻿namespace Marketing.Events.ViewModelComposition
{
    using System.Collections.Generic;

    public class ProductsLoaded
    {
        public IDictionary<dynamic, dynamic> OrdersDictionary { get; set; }
    }
}