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
				.ForMember(x => x.AssignedTo, o => o.Ignore())
				.ForMember(x => x.Attachments, o => o.Ignore());

			CreateMap<Ticket, TicketDTO>()
				.ForMember(x => x.TicketIdView, o => o.MapFrom(x => "T" + x.TicketId.ToString().PadLeft(5, '0')))
				.ForMember(x => x.Attachments, o => o.Ignore())
				.ForMember(x => x.RaisedBy, o => o.MapFrom(x => x.User.Name))
				.ForMember(x => x.AssignedTo, o =>
				{
					o.PreCondition(x => x.AssignedToId != null);
					o.MapFrom(x => x.AssignedTo.Name);
				})
				.ForMember(x => x.AttachmentViews, o => o.MapFrom(x => x.Attachments));

			CreateMap<CategoryDTO, Category>().ReverseMap();
			CreateMap<PriorityDTO, Priority>().ReverseMap();
			CreateMap<ProductDTO, Product>().ReverseMap();
			CreateMap<StatusDTO, Status>().ReverseMap();
			CreateMap<AttachmentDTO, Attachment>().ReverseMap();
            CreateMap<DiscussionAttachmentDTO, DiscussionAttachment>().ReverseMap();
            CreateMap<UserDTO, User>().ReverseMap();

			CreateMap<DiscussionDTO, Discussion>()
                .ForMember(x => x.Attachments, o => o.Ignore());

            CreateMap<Discussion, DiscussionDTO>()
				.ForMember(x => x.Name, o => o.MapFrom(x => x.User.Name))
				.ForMember(x => x.ImagePath, o => o.MapFrom(x => x.User.ImagePath))
                .ForMember(x => x.Attachments, o => o.Ignore())
                .ForMember(x => x.AttachmentViews, o => o.MapFrom(x => x.Attachments));

        }

	}
}
