using HiringManager.DomainServices;
using HiringManager.EntityModel;

namespace HiringManager.Mappers.Domain
{
    public class PositionDetailsMapper : IMapper<Position, PositionDetails>
    {
        public PositionDetails Map(Position input)
        {
            var result = AutoMapper.Mapper.DynamicMap<Position, PositionDetails>(input);

            for (var i = 0; i < result.Candidates.Count; i++)
            {
                var candidate = result.Candidates[i];
                var source = input.Candidates[i];
                candidate.CandidateName = source.Candidate.Name;

                if (!input.IsFilled())
                {
                    candidate.CanPass = candidate.Status != "Passed";
                    candidate.CanHire = true;
                    candidate.CanSetStatus = true;
                }

                for (var j = 0; j < source.Candidate.ContactInfo.Count; j++)
                {
                    var contactInfoSource = source.Candidate.ContactInfo[j];
                    var contactInfo = AutoMapper.Mapper.DynamicMap<ContactInfo, ContactInfoDetails>(contactInfoSource);
                    candidate.ContactInfo.Add(contactInfo);
                }
            }

            result.CanAddCandidate = !input.IsFilled();
            result.CanClose = !(input.IsClosed() || input.IsFilled());


            return result;
        }
    }
}