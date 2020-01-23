using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tweetbook.Contracts.V1.Requests
{
    public class CreatePostRequest
    {
        public string Name { get; set; }
        public string[] Tags { get; set; }
        
        public CreatePostRequest()
        {
            Tags = new string[0];    
        }
    }
}
