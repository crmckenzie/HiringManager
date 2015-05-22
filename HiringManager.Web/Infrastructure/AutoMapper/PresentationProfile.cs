using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using HiringManager.DomainServices;
using HiringManager.DomainServices.Candidates;
using HiringManager.DomainServices.Positions;
using HiringManager.Web.ViewModels;
using HiringManager.Web.ViewModels.Candidates;
using HiringManager.Web.ViewModels.Positions;

namespace HiringManager.Web.Infrastructure.AutoMapper
{
    public class PresentationProfile : Profile
    {
        protected override void Configure()
        {
            base.Configure();

            ConfigureCandidateModels();
            ConfigurePositions();
        }

        private void ConfigurePositions()
        {
            CreateMap<CreatePositionViewModel, CreatePositionRequest>()
                .ForMember(output => output.HiringManagerId, opt => opt.Ignore())
                ;

            CreateMap<PositionDetails, ClosePositionViewModel>()
                .ForMember(output => output.PositionTitle, opt => opt.MapFrom(input => input.Title))
                ;

            CreateMap<PositionDetails, PositionCandidatesViewModel>()
                ;

            CreateMap<QueryResponse<PositionSummary>, IndexViewModel<PositionSummaryIndexItem>>()
                ;

            CreateMap<PositionSummary, PositionSummaryIndexItem>()
                ;

        }

        private void ConfigureCandidateModels()
        {
            CreateMap<DocumentDetails, SelectListItem>()
                .ForMember(sli => sli.Text, opt => opt.MapFrom(m => m.Title))
                .ForMember(sli => sli.Value, opt => opt.MapFrom(m => m.DocumentId.ToString()))
                .ForMember(sli => sli.Selected, opt => opt.UseValue(false))
                ;

            CreateMap<ContactInfoDetails, ContactInfoViewModel>()
                ;
            CreateMap<ContactInfoViewModel, ContactInfoDetails>()
                ;

            CreateMap<EditCandidateViewModel, SaveCandidateRequest>()
                ;

            CreateMap<CandidateDetails, EditCandidateViewModel>()
                .ForMember(output => output.SourceName, opt => opt.Ignore())
                .ForMember(output => output.Sources, opt => opt.Ignore())
                ;

            CreateMap<CandidateDetails, CandidateDetailsViewModel>()
                ;

            CreateMap<NewCandidateViewModel, NewCandidateRequest>()
                .ForMember(output => output.CandidateName, opt => opt.MapFrom(input => input.Name))
                .ForMember(output => output.ContactInfo, opt => opt.ResolveUsing(MapContactInfo))
                .ForMember(output => output.Documents,
                    opt => opt.ResolveUsing(input => input.Documents
                        .Where(row => row != null)
                        .ToDictionary(e => e.FileName, e => e.InputStream)))
                ;

            CreateMap<AddCandidateViewModel, AddCandidateRequest>()
                ;

            CreateMap<CandidateStatusDetails, CandidateStatusViewModel>()
                ;

            CreateMap<CandidateStatusDetails, PositionCandidateViewModel>()
                ;

            CreateMap<ContactInfoDetails, ContactInfoViewModel>()
                ;

            CreateMap<HiringManager.DomainServices.Candidates.NoteDetails, NoteViewModel>();
        }

        private static object MapContactInfo(NewCandidateViewModel input)
        {
            var results = new List<ContactInfoDetails>
                          {
                              new ContactInfoDetails()
                              {
                                  Type = "Email",
                                  Value = input.EmailAddress
                              },
                              new ContactInfoDetails()
                              {
                                  Type = "Phone",
                                  Value = input.PhoneNumber
                              }
                          };

            return results;
        }
    }
}