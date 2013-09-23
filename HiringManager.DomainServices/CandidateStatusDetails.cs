﻿using System.Collections.Generic;

namespace HiringManager.DomainServices
{
    public class CandidateStatusDetails
    {
        public CandidateStatusDetails()
        {
            this.ContactInfo = new List<ContactInfoDetails>();
        }

        public int CandidateStatusId { get; set; }
        public int CandidateId { get; set; }

        public int PositionId { get; set; }
        public string PositionTitle { get; set; }

        public string CandidateName { get; set; }
        public string Status { get; set; }

        public IList<ContactInfoDetails> ContactInfo { get; set; }
    }
}