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

			CreateMap<Ticket, TicketDTO>()
				.ForMember(x => x.Attachments, o => o.Ignore())
				.ForMember(x => x.AttachmentViews, o => o.MapFrom(x => x.Attachments));

			CreateMap<CategoryDTO, Category>().ReverseMap();
			CreateMap<PriorityDTO, Priority>().ReverseMap();
			CreateMap<ProductDTO, Product>().ReverseMap();
			CreateMap<StatusDTO, Status>().ReverseMap();
			CreateMap<AttachmentDTO, Attachment>().ReverseMap();
		}

	}
}
