using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ItemList
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public object AddedBy { get; set; }
    }

    public class MediumList
    {
        public int MediaId { get; set; }
        public string Title { get; set; }
        public string MediaType { get; set; }
    }

    public class CharacterItemMedia
    {
        public int CharId { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public List<ItemList> Items { get; set; }
        public List<MediumList> Media { get; set; }
        public string AddedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    public class CharacterItemMediaResults<T>
    {
        public int Count { get; set; }
        public List<T> Results { get; set; }
    }
}
