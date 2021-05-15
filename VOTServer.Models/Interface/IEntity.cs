namespace VOTServer.Models.Interface
{
    public interface IEntity
    {
        public long Id { get; }
        public bool? IsDelete { get; set; }
    }
}
