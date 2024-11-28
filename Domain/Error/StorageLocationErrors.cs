namespace Domain.Error
{
    public static class StorageLocationErrors
    {
        public static readonly Error SameLocationName = new("StorageLocation.SameLocationName", "Error! Storage location is already existing");
    }
}
