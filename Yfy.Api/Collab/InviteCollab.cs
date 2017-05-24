namespace Yfy.Api.Collab
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    internal class InviteCollabArg
    {
        [JsonProperty("folder_id")]
        public long FolderId { get; set; }

        [JsonProperty("invited_user")]
        public InvitedUser InvitedUser { get; set; }

        [JsonProperty("invitation_message")]
        public string InvitationMessage { get; set; }

        public InviteCollabArg(long folderId, long invitedUserId, CollabRole role, string invitationMessage = null)
        {
            this.FolderId = folderId;
            this.InvitedUser = new InvitedUser(invitedUserId, role);
            this.InvitationMessage = invitationMessage;
        }
    }

    internal class InvitedUser
    {
        [JsonProperty("id")]
        public long Id { get; private set; }

        [JsonProperty("role")]
        [JsonConverter(typeof(StringEnumConverter))]
        public CollabRole Role { get; private set; }

        public InvitedUser(long id, CollabRole role)
        {
            this.Id = id;
            this.Role = role;
        }
    }
}
