using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace Librarian.DB
{
    /// <summary>
    /// Пользователи системы
    /// </summary>
    [Table(DBTableNames.Users)]
    public class User_Item
    {
       [Key]
       [Required]
       public Guid ID { get; set; }

       [Required]
       public DateTime CreationDate { get; set; }

       [Required]
       [Column(TypeName = DBConstants.NormalNameType_Expression)]
       public string Initials { get; set; }

       [Required]
       [Column(TypeName = DBConstants.NormalNameType_Expression)]
       public string LastName { get; set; }

       [Required]
       [Column(TypeName = DBConstants.NormalNameType_Expression)]
       public string FirstName { get; set; }

       [Required]
       [Column(TypeName = DBConstants.NormalNameType_Expression)]
       public string SecondName { get; set; }

       [Required]
       public bool IsAdministrator { get; set; }

       [Required]
       public bool IsActiveUser { get; set; }

       public string ASPIdentity { get; set; }
    }
}
