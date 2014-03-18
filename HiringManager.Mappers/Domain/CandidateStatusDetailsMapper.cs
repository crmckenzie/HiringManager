using HiringManager.DomainServices;
using HiringManager.EntityModel;

namespace HiringManager.Mappers.Domain
{
    public class CandidateStatusDetailsMapper : IMapper<CandidateStatus, CandidateStatusDetails>
    {
        public CandidateStatusDetails Map(CandidateStatus input)
        {
            var result = AutoMapper.Mapper.DynamicMap<CandidateStatus, CandidateStatusDetails>(input);
            result.CandidateName = input.Candidate.Name;

            foreach (var contactInfo in input.Candidate.ContactInfo)
            {
                result.ContactInfo.Add(new ContactInfoDetails()
                                       {
                                           ContactInfoId = contactInfo.ContactInfoId.Value,
                                           Type = contactInfo.Type,
                                           Value = contactInfo.Value,
                                       });
            }

            return result;
        }
    }
}
