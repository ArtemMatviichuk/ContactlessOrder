using System;

namespace ContactlessOrder.DAL.Entities.ApplicationSettings
{
    public class DictionaryItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
