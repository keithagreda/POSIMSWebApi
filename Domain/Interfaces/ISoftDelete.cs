namespace Domain.Interfaces
{
    public interface ISoftDelete
    {
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletionTime { get; set; }

        public void Undo()
        {
            IsDeleted = false;
            DeletionTime = null;
        }
    }
}
