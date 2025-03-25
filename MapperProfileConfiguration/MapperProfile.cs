using api.leads.Data.Models;
using api.leads.DTO.Response;
using AutoMapper;
using System.Diagnostics.CodeAnalysis;

namespace api.leads.MapperProfileConfiguration
{
    [ExcludeFromCodeCoverage]
	public class MapperProfile : Profile
	{
		public MapperProfile()
		{
			CreateMap<LeadType, LeadTypeGetResponse>();
			CreateMap<DuplicateLead, DuplicateLeadsGetResponse>();
			CreateMap<Campaign, CampaignGetResponse>();
			CreateMap<CampaignId, CampaignIdGetResponse>();
			CreateMap<Charges, ChargesGetResponse>();
			CreateMap<PossibleLeads, PossibleLeadsGetResponse>();
			CreateMap<PurchasedLeads, PurchasedLeadsGetResponse>();
			CreateMap<PurchaseLeads, PurchaseLeadsGetResponse>();
			CreateMap<CampaignFields, CampaignFieldsGetResponse>();
			CreateMap<Person, PersonGetResponse>();
			CreateMap<Call, CallGetResponse>();
			CreateMap<Forms, FormsGetResponse>();
			CreateMap<Groups, GroupsGetResponse>();
			CreateMap<LeadsToMatch, LeadsToMatchGetResponse>();
		}
	}
}
