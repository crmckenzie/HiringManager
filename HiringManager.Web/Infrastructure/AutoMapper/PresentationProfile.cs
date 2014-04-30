using System.Collections.Generic;
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

            CreateMap<AddCandidateViewModel, AddCandidateRequest>()
                .ForMember(output => output.CandidateName, opt => opt.MapFrom(input => input.Name))
                .ForMember(output => output.ContactInfo,
                    opt => opt.ResolveUsing(input =>
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
                        }))
                ;

            CreateMap<CandidateStatusDetails, CandidateStatusViewModel>()
                ;

            CreateMap<CandidateStatusDetails, PositionCandidateViewModel>()
                ;

            CreateMap<ContactInfoDetails, ContactInfoViewModel>()
                ;
        }
    }
}