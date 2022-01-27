using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreCifradoBBDD.Models
{
    [Table("USERS")]
    public class Usuario
    {
        [Key]
        [Column("IDUSUARIO")]
        public int IdUsuario { get; set; }
        [Column("NOMBRE")]
        public String Nombre { get; set; }
        [Column("EMAIL")]
        public String Email { get; set; }
        [Column("SALT")]
        public String Salt { get; set; }
        [Column("IMAGEN")]
        public String Imagen { get; set; }
        //LOS TIPOS BLOB, CLOB O VARBINARY
        //SON CONVERTIDOS EN LOS CONTEXT AUTOMATICAMENTE A byte[]
        [Column("PASS")]
        public byte[] Password { get; set; }
    }
}
