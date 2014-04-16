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
                .ForMember(c => c.CanHire, opt => opt.ResolveUsing(m => !m.Position.IsFilled()))
                .ForMember(c => c.CanSetStatus, opt => opt.ResolveUsing(m => !m.Position.IsFilled()))
                .ForMember(c => c.CanPass, opt => opt.ResolveUsing(m => !m.Position.IsFilled() && m.Status != "Passed"))
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
            ;

            CreateMap<Position, PositionDetails>()
                .ForMember(output => output.CanAddCandidate, opt => opt.ResolveUsing(input => !input.IsFilled()))
                .ForMember(output => output.CanClose, opt => opt.ResolveUsing(input => !(input.IsClosed() || input.IsFilled())))
                .AfterMap((position, positionDetails) =>
                    {
                        foreach (var candidate in positionDetails.Candidates)
                        {
                            if (!position.IsFilled())
                            {
                                //candidate.CanPass = candidate.Status != "Passed";
                                //candidate.CanHire = true;
                                //candidate.CanSetStatus = true;
                            }
                        }

                        //positionDetails.CanAddCandidate = !position.IsFilled();
                        //positionDetails.CanClose = !(position.IsClosed() || position.IsFilled());

                    })
                ;

        }
    }
}