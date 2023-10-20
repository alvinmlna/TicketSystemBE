using Core.Entities;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;

namespace BusinessLogic.Services
{
    public class DiscussionService : IDiscussionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DiscussionService(
            IUnitOfWork unitOfWork
            )
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Add(Discussion discussion)
        {
            discussion.DateSending = DateTime.Now;

            _unitOfWork.Repository<Discussion>().Add( discussion );
            return await _unitOfWork.SaveChangesReturnBool();
        }

        public async Task<bool> Delete(int discussionId)
        {
            var toDelete = await _unitOfWork.Repository<Discussion>().GetByIdAsync( discussionId );
            if ( toDelete == null ) { return false; }

            _unitOfWork.Repository<Discussion>().Delete( toDelete );
            return await _unitOfWork.SaveChangesReturnBool();
        }

        public async Task<bool> Edit(Discussion discussion)
        {
            var toUpdate = await _unitOfWork.Repository<Discussion>().GetByIdAsync(discussion.DiscussionId);
            if (toUpdate == null) { return false; }

            toUpdate.Message = discussion.Message;
            _unitOfWork.Repository<Discussion>().Update( toUpdate );
            return await _unitOfWork.SaveChangesReturnBool();
        }

        public async Task<Discussion> GetDiscussionById(int Id)
        {
            return await _unitOfWork.Repository<Discussion>().GetByIdAsync(Id);
        }

        public async Task<IReadOnlyList<Discussion>> GetDiscussionByTicketId(int ticketId)
        {
            return await _unitOfWork.DiscussionRepository.GetDiscussionByTicketId(ticketId);
        }
    }
}
