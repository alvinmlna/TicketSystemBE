using Core.Entities;

namespace Core.Interfaces.Services
{
    public interface IDiscussionService
    {
        Task<Discussion?> Add(Discussion discussion);
        Task<bool> Edit(Discussion discussion);
        Task<bool> Delete(int discussionId);
        Task<IReadOnlyList<Discussion>> GetDiscussionByTicketId(int ticketId);
        Task<Discussion> GetDiscussionById(int Id);
        Task<bool> UploadFile(List<DiscussionAttachment> attachments, int discussionId);
    }
}
