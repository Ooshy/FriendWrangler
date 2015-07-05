using FriendWrangler.Core.Models;

namespace FriendWrangler.Core.Classes
{
    public class InvitationValidator
    {
        public Invitation Invitation { get; set; }
        private InvitationValidator() { }
        public InvitationValidator(Invitation invitation)
        {
            Invitation = invitation;
        }

        public bool IsValid()
        {
            return true;
        }
    }
}
