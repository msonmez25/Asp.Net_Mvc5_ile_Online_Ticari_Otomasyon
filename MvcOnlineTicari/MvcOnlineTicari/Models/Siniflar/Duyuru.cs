using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace MvcOnlineTicari.Models.Siniflar
{
    public class Duyuru
    {
        [Key]
        public int DuyuruID { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string Kategori { get; set; }


        [Column(TypeName = "Varchar")]
        [StringLength(2000)]
        public string icerik { get; set; }


        [Column(TypeName = "Date")]
        public DateTime Tarih { get; set; }
    }
}