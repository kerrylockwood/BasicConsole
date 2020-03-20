using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    // GET
    public class MediaGet
    {
        public int MediaId { get; set; }
        public string Title { get; set; }
        public string MediaType { get; set; }
        public string Description { get; set; }
        public object AddedBy { get; set; }
    }

    // GET
    public class MediaShort
    //public class Medium
    {
        public int MediaId { get; set; }
        public string Title { get; set; }
        public string MediaType { get; set; }
        public string AddedBy { get; set; }
    }

    //public class MediaResults<T>
    //{
    //    public int Count { get; set; }
    //    public List<T> Results { get; set; }
    //}
}
