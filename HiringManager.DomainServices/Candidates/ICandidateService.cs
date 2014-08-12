using HiringManager.DomainServices.Positions;

namespace HiringManager.DomainServices.Candidates
{
    public interface ICandidateService
    {
        CandidateDetails Get(int id);
        ValidatedResponse Save(SaveCandidateRequest request);
        QueryResponse<CandidateSummary> Query(QueryCandidatesRequest request);
        DocumentDetails Upload(UploadDocumentRequest request);
        ValidatedResponse Delete(int documentId);

        ValidatedResponse AddNote(AddNoteRequest request);
        ValidatedResponse EditNote(EditNoteRequest request);
        ValidatedResponse DeleteNote(int noteId);
    }
}
