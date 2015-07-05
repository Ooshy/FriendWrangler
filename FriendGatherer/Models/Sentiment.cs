using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendWrangler.Core.Models
{

    public class Sentiment
    {
        public Probability Probability { get; set; }
        public string Label { get; set; }
    }

    public class Probability
    {
        public float Negative { get; set; }
        public float Neutral { get; set; }
        public float Positive { get; set; }
    }



}
