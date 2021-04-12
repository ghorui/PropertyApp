using Properties.Common;
using Properties.Model;
using Properties.Repository;
using System.Threading.Tasks;

namespace Properties.Service
{
    public interface IInteractionPostService 
    {
        public Task<bool> GetResponseAsync(PropertyInfo input);
    }

    public class InteractionPostService : IInteractionPostService
    {
        private readonly IInteractionPostRepository _interactionPostRepository;

        public InteractionPostService(IInteractionPostRepository interactionPostRepository)
        {
            _interactionPostRepository = interactionPostRepository;
        }

        public async Task<bool> GetResponseAsync(PropertyInfo input)
        {
            if (input == null)
            {
                return false;
            }

            return _interactionPostRepository.SaveDataToDb(input);
        }
    }
}
