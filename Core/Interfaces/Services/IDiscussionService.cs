using Core.Entities;

namespace Core.Interfaces.Services
{
    public interface IDiscussionService
    {
        Task<bool> Add(Discussion discussion);
        Task<bool> Edit(Discussion discussion);
        Task<bool> Delete(int discussionId);
        Task<IReadOnlyList<Discussion>> GetDiscussionByTicketId(int ticketId);
        Task<Discussion> GetDiscussionById(int Id);
    }
}
