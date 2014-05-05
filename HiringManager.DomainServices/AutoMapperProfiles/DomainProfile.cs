using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Reflection.Emit;
using AutoMapper;
using HiringManager.DomainServices.Candidates;
using HiringManager.DomainServices.Impl;
using HiringManager.DomainServices.Positions;
using HiringManager.DomainServices.Sources;
using HiringManager.EntityModel;
using HiringManager.EntityModel.Specifications;

namespace HiringManager.DomainServices.AutoMapperProfiles
{
    public class DomainProfile : Profile
    {
        protected override void Configure()
        {
            base.Configure();

            ConfigurePositions();
            ConfigureCandidates();
            ConfigureSources();
        }

        private void ConfigureSources()
        {
            CreateMap<Source, SourceSummary>()
                .ForMember(output => output.SourceId, opt => opt.MapFrom(input => input.SourceId.Value))
                ;
        }

        private void ConfigureCandidates()
        {
            CreateMap<Candidate, CandidateDetails>()
                ;

            CreateMap<Candidate, CandidateSummary>()
                .ForMember(output => output.CandidateId, opt => opt.MapFrom(input => input.CandidateId.Value))
                .ForMember(output => output.Source, opt => opt.MapFrom(input => input.Source.Name))
                ;

            CreateMap<SaveCandidateRequest, Candidate>()
                .ForMember(output => output.AppliedTo, opt => opt.Ignore())
                .ForMember(output => output.Source, opt => opt.Ignore())
                .ForMember(output => output.Documents, opt => opt.Ignore())
                ;

            CreateMap<ContactInfo, ContactInfoDetails>()
                ;

            CreateMap<ContactInfoDetails, ContactInfo>()
                .ForMember(output => output.Candidate, opt => opt.Ignore())
                .ForMember(output => output.CandidateId, opt => opt.Ignore())
                .ForMember(output => output.Manager, opt => opt.Ignore())
                .ForMember(output => output.ManagerId, opt => opt.Ignore())
                ;

            CreateMap<NewCandidateRequest, CandidateStatus>()
                .ForMember(output => output.Status, opt => opt.UseValue("Resume Received"))
                .ForMember(output => output.Candidate, opt => opt.ResolveUsing(MapCandidate))
                .ForMember(output => output.CandidateId, opt => opt.Ignore())
                .ForMember(output => output.CandidateStatusId, opt => opt.Ignore())
                .ForMember(output => output.Position, opt => opt.Ignore())
                ;

            CreateMap<AddCandidateRequest, CandidateStatus>()
                .ForMember(output => output.Status, opt => opt.UseValue("Resume Received"))
                .ForMember(output => output.CandidateId, opt => opt.MapFrom(input => input.CandidateId))
                .ForMember(output => output.Candidate, opt => opt.Ignore())
                .ForMember(output => output.CandidateStatusId, opt => opt.Ignore())
                .ForMember(output => output.Position, opt => opt.Ignore())
            ;

            CreateMap<CandidateStatus, CandidateStatusDetails>()
                .ForMember(output => output.SourceId, opt => opt.MapFrom(input => input.Candidate.Source.SourceId))
                .ForMember(output => output.Source, opt => opt.MapFrom(input => input.Candidate.Source.Name))
                .ForMember(c => c.ContactInfo, opt => opt.MapFrom(m => m.Candidate.ContactInfo))
                .ForMember(c => c.CanHire, opt => opt.ResolveUsing(m => !m.Position.IsFilled()))
                .ForMember(c => c.CanSetStatus, opt => opt.ResolveUsing(m => !m.Position.IsFilled()))
                .ForMember(c => c.CanPass, opt => opt.ResolveUsing(m => !m.Position.IsFilled() && m.Status != "Passed"))
                ;
        }

        private static Candidate MapCandidate(NewCandidateRequest input)
        {

            var candidate = new Candidate()
                            {
                                CandidateId = null,
                                SourceId = input.SourceId,
                                Name = input.CandidateName,
                                ContactInfo = Mapper.Map<ContactInfo[]>(input.ContactInfo),
                            };

            foreach (var contactInfo in candidate.ContactInfo)
            {
                contactInfo.Candidate = candidate;
            }

            candidate.Documents = input.Documents.Select(row => new Document()
                                                                {
                                                                    DisplayName = row.Key
                                                                }).ToList()
                ;

            return candidate;
        }

        private void ConfigurePositions()
        {
            CreateMap<CreatePositionRequest, Position>()
                .ForMember(m => m.CreatedById, opt => opt.MapFrom(req => req.HiringManagerId))
                .ForMember(m => m.Openings,
                    opt => opt.ResolveUsing(input =>
                                            {
                                                var results =
                                                    Enumerable.Range(0, input.Openings)
                                                        .Select(i => new Opening
                                                                     {
                                                                         Status = "Pending",
                                                                     }).ToList();
                                                return results;
                                            }))
                .ForMember(m => m.Status, opt => opt.UseValue("Open"))
                .ForMember(m => m.PositionId, opt => opt.Ignore())
                .ForMember(m => m.CreatedBy, opt => opt.Ignore())
                .ForMember(m => m.Candidates, opt => opt.Ignore())
                ;

            CreateMap<QueryPositionSummariesRequest, PositionSpecification>()
                .ForMember(dest => dest.Negate, opt => opt.Ignore())
                ;

            CreateMap<Position, PositionSummary>()
                .ForMember(output => output.CandidatesAwaitingReview, opt => opt.Ignore())
                .ForMember(output => output.Openings, opt => opt.MapFrom(input => input.Openings.Count))
                .ForMember(output => output.OpeningsFilled,
                    opt => opt.MapFrom(input => input.Openings.Count(row => row.IsFilled())))
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
                .ForMember(output => output.Openings, opt => opt.MapFrom(input => input.Openings.Count))
                .ForMember(output => output.OpeningsFilled,
                    opt => opt.MapFrom(input => input.Openings.Count(row => row.IsFilled())))
                .ForMember(output => output.CanClose,
                    opt => opt.ResolveUsing(input => !(input.IsClosed() || input.IsFilled())))
                ;
        }
    }
}