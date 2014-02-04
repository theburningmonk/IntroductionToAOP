namespace AopDemo.Model.Entities {
    using System;

    /// <summary>
    /// A simple Author entity
    /// </summary>
    public class Author {
        /// <summary>
        /// Unique Id that identifies the author
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The first name of the author
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the author
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The nick name the author uses
        /// </summary>
        public string NickName { get; set; }
    }
}
