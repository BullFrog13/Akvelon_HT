using System.ComponentModel.DataAnnotations;

namespace UserService.DAL.Entities
{
    public class BaseType
    {
        [Key]
        public int Id { get; set; }
    }
}