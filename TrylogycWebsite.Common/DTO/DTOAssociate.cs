using CommonTrylogycWebsite.DTO.Interfaces;

namespace CommonTrylogycWebsite.DTO
{
    public class DTOAssociate : IDTOAssociate
    {
        public int Id { get;set; }
        public int ConnectionId { get;set; }
        public string CGP { get;set; }
        public string Name { get;set; }
        public string Address { get;set; }
        public string City { get;set; }
        public string Document { get;set; }
    }
}
