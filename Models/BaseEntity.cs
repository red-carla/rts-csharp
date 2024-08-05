using System.ComponentModel.DataAnnotations;
namespace RTS.Models;

public class BaseEntity
{
    [Key]
    public int Id { get; set; }
}