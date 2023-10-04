using API.DTO;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles() 
		{
			CreateMap<TicketDTO, Ticket>()
				.ForMember(x => x.Attachments, o => o.Ignore());
		}

	}
}
