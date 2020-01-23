using System;

namespace Tweetbook.Contracts.V1.Responses
{
    public class CreateTagResponse
    {
        public string CreatorId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Name { get; set; }
    }
}