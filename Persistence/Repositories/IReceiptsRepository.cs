using System.Collections.Generic;
using Persistence.Models.ReadModels;
using Persistence.Models.WriteModels;

namespace Persistence.Repositories
{
    public interface IReceiptsRepository
    {
        IEnumerable<TripReadModel> GetAll();

        void Save(TripWriteModel receipt);
    }
}
