using FizzWare.NBuilder;
using HiringManager.EntityModel;

namespace IntegrationTestHelpers
{
    public class NBuilderConfiguration
    {
        public NBuilderConfiguration DisableIdPropertyNamers()
        {
            BuilderSetup.DisablePropertyNamingFor<Position, int?>(model => model.PositionId);

            BuilderSetup.DisablePropertyNamingFor<Candidate, int?>(model => model.CandidateId);
            BuilderSetup.DisablePropertyNamingFor<Candidate, int?>(model => model.SourceId);

            BuilderSetup.DisablePropertyNamingFor<CandidateStatus, int?>(model => model.CandidateStatusId);

            BuilderSetup.DisablePropertyNamingFor<Manager, int?>(model => model.ManagerId);

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