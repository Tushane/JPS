using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JPS.Data
{
    public class PREMISE_DETAILS
    {
        [Key]
        [Column(TypeName = "AS CONCAT(SUB_ID, CONVERT(NVARCHAR(90), ROW_ID)) PERSISTED")]
        public string ID { set; get; }

        [Column(TypeName = "INT IDENTITY(200, 10)")]
        [Required]
        public int ROW_ID { set; get; }

        [Column(TypeName = "NVARCHAR(10) DEFAULT 'PREPJPS'")]
        public string SUB_ID { set; get; }

        [Column(TypeName = "NVARCHAR(MAX)")]
        public string LOCATION_ADDRESS { set; get; }
    }
}
