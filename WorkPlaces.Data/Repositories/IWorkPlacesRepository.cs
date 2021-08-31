using System;

namespace WorkPlaces.Data.Repositories
{
    public interface IWorkPlacesRepository : IDisposable
    {
        bool WorkPlaceExists(int workPlaceId);
    }
}
