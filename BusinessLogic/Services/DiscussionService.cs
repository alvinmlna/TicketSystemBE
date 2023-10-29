using Core.Entities;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;
using System.Net.Sockets;

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

        public async Task<Discussion?> Add(Discussion discussion)
        {
            discussion.DateSending = DateTime.Now;

            _unitOfWork.Repository<Discussion>().Add( discussion );
            int result = await _unitOfWork.SaveChanges();
            if (result > 0)
                return discussion;

            return null;
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

        public async Task<bool> UploadFile(List<DiscussionAttachment> attachments, int discussionId)
        {
            var discussion = await _unitOfWork.Repository<Discussion>().GetByIdAsync(discussionId);
            if (discussion == null) { return false; }

            foreach (var attachment in attachments)
            {
                attachment.DateAdded = DateTime.Now;
                discussion.Attachments.Add(attachment);
            }

            _unitOfWork.Repository<Discussion>().Update(discussion);
            return await _unitOfWork.SaveChangesReturnBool();
        }
    }
}
