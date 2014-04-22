using System;

namespace HiringManager.DomainServices.Positions
{
    public class CreatePositionRequest
    {
        public int HiringManagerId { get; set; }
        public DateTime? OpenDate { get; set; }
        public string Title { get; set; }
        public int Openings { get; set; }
    }
}