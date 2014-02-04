namespace AopDemo.Model {
    using System;

    /// <summary>
    /// Defines the operations of a simple DAL
    /// </summary>
    public interface IDataAccessLayer {
        /// <summary>
        /// Gets an object of type T 
        /// </summary>
        T Get<T>(Guid id);
    }
}