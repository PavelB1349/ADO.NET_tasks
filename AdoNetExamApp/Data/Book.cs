namespace AdoNetExamApp.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Book")]
    public partial class Book
    {
        public int Id { get; set; }

        [StringLength(15)]
        public string Title { get; set; }

        public int? AuthorId { get; set; }

        public int? CategoryId { get; set; }

        public int? Pages { get; set; }

        public int? Cost { get; set; }

        public virtual Author Author { get; set; }

        public virtual Category Category { get; set; }
    }
}
