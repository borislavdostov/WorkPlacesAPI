namespace WorkPlaces.Data.Repositories
{
    public interface IWorkPlacesRepository
    {
        bool Exists(int workPlaceId);
    }
}
