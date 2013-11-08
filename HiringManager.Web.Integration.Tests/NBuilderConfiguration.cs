using FizzWare.NBuilder;
using HiringManager.EntityModel;

namespace HiringManager.Web.Integration.Tests
{
    public class NBuilderConfiguration
    {
        public NBuilderConfiguration DisableIdPropertyNamers()
        {
            BuilderSetup.DisablePropertyNamingFor<Position, int?>(model => model.PositionId);

            BuilderSetup.DisablePropertyNamingFor<Candidate, int?>(model => model.CandidateId);

            BuilderSetup.DisablePropertyNamingFor<CandidateStatus, int?>(model => model.CandidateStatusId);
            BuilderSetup.DisablePropertyNamingFor<Document, int?>(model => model.DocumentId);

            BuilderSetup.DisablePropertyNamingFor<Manager, int?>(model => model.ManagerId);

            BuilderSetup.DisablePropertyNamingFor<Message, int?>(model => model.MessageId);
            return this;
        }

        private NBuilderConfiguration DisablePropertyNamesForShortFields()
        {
            return this;
        }

        public NBuilderConfiguration Reset()
        {
            BuilderSetup.ResetToDefaults();
            return this;
        }

        public NBuilderConfiguration IntegrationTestConfiguration()
        {
            return Reset()
                .DisableIdPropertyNamers()
                .DisablePropertyNamesForShortFields()
                .DisableAuditColumns()
                ;
        }

        public NBuilderConfiguration UnitTestConfiguration()
        {
            return Reset()
                .DisablePropertyNamesForShortFields()
                .DisableAuditColumns()
                ;
        }

        public NBuilderConfiguration DisableAuditColumns()
        {
            return this;
        }

    }
}