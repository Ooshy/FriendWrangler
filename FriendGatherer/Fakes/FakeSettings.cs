using FriendWrangler.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendWrangler.Core.Fakes
{
    public class FakeSettings : ISettings
    {
        public Models.User User { get; set; }

        public void Save()
        {
           
        }
        public FakeSettings()
        {

        }
    }
}
