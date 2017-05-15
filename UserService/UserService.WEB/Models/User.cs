using System.Runtime.Serialization;

namespace UserService.WEB.Models
{
    [DataContract]
    public class User
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string NickName { get; set; }

        [DataMember]
        public string FullName { get; set; }
    }
}