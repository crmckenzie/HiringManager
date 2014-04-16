using AutoMapper;
using HiringManager.EntityModel;
using HiringManager.EntityModel.Specifications;
using System.Linq;

namespace HiringManager.DomainServices.AutoMapperProfiles
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
                ;

        }
    }
}