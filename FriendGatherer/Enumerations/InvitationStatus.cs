using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendWrangler.Core.Enumerations
{
    /// <summary>
    /// Constains the status of the message
    /// </summary>
    public enum InvitationStatus
    {
        NotYetSent,
        Pending,
        Yes,
        No,
        NoResponse,
        Unknown
    }
}
