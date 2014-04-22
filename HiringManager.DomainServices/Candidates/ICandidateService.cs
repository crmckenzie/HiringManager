namespace HiringManager.DomainServices.Candidates
{
    public interface ICandidateService
    {
        ValidatedResponse Save(SaveCandidateRequest request);
    }
}
