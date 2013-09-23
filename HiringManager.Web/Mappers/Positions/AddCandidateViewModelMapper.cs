using AutoMapper;
using HiringManager.DomainServices;
using HiringManager.Web.Models.Positions;

namespace HiringManager.Web.Mappers.Positions
{
    public class AddCandidateViewModelMapper : IMapper<AddCandidateViewModel, AddCandidateRequest>
    {
        public AddCandidateRequest Map(AddCandidateViewModel input)
        {
            var result = Mapper.DynamicMap<AddCandidateViewModel, AddCandidateRequest>(input);
            result.CandidateName = input.Name;

            result.ContactInfo.Add(new ContactInfoDetails()
                                   {
                                       Type = "Email",
                                       Value = input.EmailAddress
                                   });

            result.ContactInfo.Add(new ContactInfoDetails()
                                   {
                                       Type = "Phone",
                                       Value = input.PhoneNumber
                                   });

            return result;
        }
    }
}