﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ItemDetail
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string AddedBy { get; set; }
    }

    public class ItemGetAll
    //public class Item
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string AddedBy { get; set; }
    }
}
