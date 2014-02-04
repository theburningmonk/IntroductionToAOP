namespace AopDemo.Model {
    using System;
    using Entities;

    /// <summary>
    /// Defines the operations for a simple repository
    /// </summary>
    public interface IRepository {
        /// <summary>
        /// Retrieves a book by Id
        /// </summary>
        Book GetBookById(Guid id);

        /// <summary>
        /// Retrieves an author by Id
        /// </summary>
        Author GetAuthorById(Guid id);
    }
}