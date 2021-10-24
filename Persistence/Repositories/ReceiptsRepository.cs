using System.Collections.Generic;
using Persistence.Models.ReadModels;
using Persistence.Models.WriteModels;

namespace Persistence.Repositories
{
    public class ReceiptsRepository : IReceiptsRepository
    {
        private const string FileName = "receipts.json";
        private readonly IFileClient _fileClient;

        public ReceiptsRepository(IFileClient fileClient)
        {
            _fileClient = fileClient;
        }

        public IEnumerable<TripReadModel> GetAll()
        {
            return _fileClient.ReadAll<TripReadModel>(FileName);
        }

        public void Save(TripWriteModel receipt)
        {
            _fileClient.Append(FileName, receipt);
        }
    }
}
