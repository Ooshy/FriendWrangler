using FriendWrangler.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendWrangler.Core.Services
{
    public interface ISettings
    {
        User User { get; set; }
        void Save();
    }
}
