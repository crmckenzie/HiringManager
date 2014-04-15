using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using AutoMapper;
using HiringManager.DomainServices;
using HiringManager.EntityModel;
using HiringManager.EntityModel.Specifications;

namespace HiringManager.Web.Infrastructure.AutoMapper
{
    public class DomainProfile : Profile
    {
        protected override void Configure()
        {
            base.Configure();

            ConfigurePositions();
            ConfigureCandidateStatus();
        }

        private void ConfigureCandidateStatus()
        {
            CreateMap<ContactInfo, ContactInfoDetails>()
                ;
            CreateMap<CandidateStatus, CandidateStatusDetails>()
                .ForMember(c => c.ContactInfo, opt => opt.MapFrom(m => m.Candidate.ContactInfo))
                ;
        }

        private void ConfigurePositions()
        {
            CreateMap<CreatePositionRequest, Position>()
                .ForMember(m => m.CreatedById, opt => opt.MapFrom(req => req.HiringManagerId))
                .ForMember(m => m.Status, opt => opt.UseValue("Open"))
                .ForMember(m => m.PositionId, opt => opt.Ignore())
                .ForMember(m => m.FilledById, opt => opt.Ignore())
                .ForMember(m => m.FilledBy, opt => opt.Ignore())
                .ForMember(m => m.CreatedBy, opt => opt.Ignore())
                .ForMember(m => m.Candidates, opt => opt.Ignore())
                .ForMember(m => m.FilledDate, opt => opt.Ignore())
                ;

            CreateMap<QueryPositionSummariesRequest, PositionSpecification>()
                .ForMember(dest => dest.Negate, opt => opt.Ignore())
                ;

            CreateMap<Position, PositionSummary>()
                .ForMember(dest => dest.CandidatesAwaitingReview, opt => opt.Ignore())
                .AfterMap((position, positionSummary) =>
                          {
                              if (positionSummary.Status == "Filled")
                                  return;

                              var passedCandidates =
                                  position.Candidates.Where(row => row.Status == "Passed" || row.Status == "Hired");
                              var inReview = position.Candidates.Except(passedCandidates);
                              positionSummary.CandidatesAwaitingReview = inReview.Count();
                          })
                ;
            //.ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull))

            ;

        }
    }
}