namespace AutomatServiceTest.Domain.Models
{
    /// <summary>
    /// Склад
    /// </summary>
    public class Storage
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<StorageProduct> StorageProducts { get; set; }
    }
}
