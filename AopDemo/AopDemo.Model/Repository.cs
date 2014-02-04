namespace AopDemo.Model {
    using System;
    using Entities;
    using log4net;

    /// <summary>
    /// Simple repository
    /// </summary>
    public class Repository : IRepository {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Repository));

        public Repository(IDataAccessLayer dal) {
            Dal = dal;
        }

        protected IDataAccessLayer Dal { get; private set; }

        public Book GetBookById(Guid id) {
            return Dal.Get<Book>(id);
        }

        public Author GetAuthorById(Guid id) {
            Logger.DebugFormat("Executing GetAuthorById with Id [{0}]", id);

            if (id == default(Guid)) {
                Logger.ErrorFormat("Id cannot be default Guid.");
                throw new ArgumentException("Id cannot be default Guid", "id");
            }

            try {
                var author = Dal.Get<Author>(id);

                Logger.DebugFormat("Retrieved author for Id [{0}]", id);

                return author;
            }
            catch (Exception ex) {
                Logger.Error(
                    string.Format("Exception caught whilst executing GetAuthorById with Id [{0}]", id),
                    ex);
                throw;
            }
        }
    }
}