namespace AopDemo.Model.Entities {
    using System;

    /// <summary>
    /// A simple Book entity
    /// </summary>
    public class Book {
        /// <summary>
        /// Unique Id that identifies the book
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The title of the book
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The author of the book
        /// </summary>
        public Author Author { get; set; }

        /// <summary>
        /// When the book was published
        /// </summary>
        public DateTime DatePublished { get; set; }
    }
}
