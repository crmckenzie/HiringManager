using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using HiringManager.DomainServices;
using HiringManager.Web.ViewModels;
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