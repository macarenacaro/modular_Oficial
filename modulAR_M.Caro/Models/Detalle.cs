﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace modulAR_M.Caro.Models
{
    public class Detalle
    {
        public int Id { get; set; }

        [Display(Name = "Núm.Pedido")]
        public int PedidoId { get; set; }
        [Display(Name = "Id.Producto")]
        public int ProyectoId { get; set; }
        public int Cantidad { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Precio { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        
        public virtual Pedido? Pedido { get; set; }
        public virtual Proyecto? Proyecto { get; set; }
    }
}
