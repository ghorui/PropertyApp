using Properties.Common;
using Properties.Model;
using Properties.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Properties.Service
{
    public interface IInteractionGetService 
    {
        public Task<List<PropertyInfo>> GetResponseAsync();
    }

    public class InteractionGetService : IInteractionGetService
    {
        private readonly IInteractionGetRepository _interactionRepository;

        public InteractionGetService(IInteractionGetRepository interactionRepository)
        {
            _interactionRepository = interactionRepository;
        }

         public async Task<List<PropertyInfo>> GetResponseAsync()
        {
            return _interactionRepository.GetResponse();
        }
    }
}
