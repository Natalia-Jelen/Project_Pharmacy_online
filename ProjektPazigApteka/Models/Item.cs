using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjektPazigApteka.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace ProjektPazigApteka.Models
{
    //[Bind(Exclude = "ItemId")]
    public class Item
    {
        [ScaffoldColumn(false)]
        public int ItemId { get; set; }
        
        [DisplayName("Category")]
        public int CategoryId { get; set; }
        [DisplayName("Producer")]
        public int ProducerId { get; set; }
        [Required(ErrorMessage = "Item title is required!!!")]
        [StringLength(160)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Price is required!!!")]
        [Range(0.01,500,ErrorMessage ="price Must be between 0,1 and 500")]
        public decimal Price { get; set; }
        [DisplayName("Item Art Url")]
        [StringLength(1024)]
        public string ItemArtUrl { get; set; }

        public virtual Category Category { get; set; }
        public virtual Producer Producer { get; set; }

        public virtual List<OrderDetail> OrderDetails { get; set; }
        
    }
}