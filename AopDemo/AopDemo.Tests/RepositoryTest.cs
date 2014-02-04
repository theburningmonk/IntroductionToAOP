namespace AopDemo.Model.Tests {
    using System;
    using Entities;
    using log4net.Config;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class RepositoryTest {
        private readonly Mock<IDataAccessLayer> _mockDal = new Mock<IDataAccessLayer>();
        private Author _author;
        private Book _book;
        private IDataAccessLayer _dal;

        [TestFixtureSetUp]
        public void SetupTestFixture() {
            _author = new Author {
                                     Id = Guid.NewGuid(),
                                     FirstName = "Yan",
                                     LastName = "Cui",
                                     NickName = "theburningmonk"
                                 };
            _book = new Book {
                                 Id = Guid.NewGuid(),
                                 Author = _author,
                                 Title = "AOP is awesome",
                                 DatePublished = DateTime.Now,
                             };

            // set up the mock dal to always return the configured book and author
            _mockDal.Setup(dal => dal.Get<Book>(It.IsAny<Guid>())).Returns(_book);
            _mockDal.Setup(dal => dal.Get<Author>(It.IsAny<Guid>())).Returns(_author);
            _dal = _mockDal.Object;

            // Set up a simple configuration that logs on the console.
            BasicConfigurator.Configure();
        }

        #region Non-AOP Repository

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void RepositoryTestGetBookByIdWithDefaultGuid() {
            new Repository(_dal).GetBookById(default(Guid));
        }

        [Test]
        public void RepositoryTestGetBookByIdSuccess() {
            var book = new Repository(_dal).GetBookById(Guid.NewGuid());
            Assert.AreEqual(_book, book);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void RepositoryTestGetAuthoryIdWithDefaultGuid() {
            new Repository(_dal).GetAuthorById(default(Guid));
        }

        [Test]
        public void RepositoryTestGetAuthorByIdSuccess() {
            var author = new Repository(_dal).GetAuthorById(Guid.NewGuid());
            Assert.AreEqual(_author, author);
        }

        #endregion

        #region AOP Repository

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void AopRepositoryTestGetBookByIdWithDefaultGuid() {
            new AopRepository(_dal).GetBookById(default(Guid));
        }

        [Test]
        public void AopRepositoryTestGetBookByIdSuccess() {
            var book = new AopRepository(_dal).GetBookById(Guid.NewGuid());
            Assert.AreEqual(_book, book);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void AopRepositoryTestGetAuthorByIdWithDefaultGuid() {
            new AopRepository(_dal).GetAuthorById(default(Guid));
        }

        [Test]
        public void AopRepositoryTestGetAuthorByIdSuccess() {
            var author = new AopRepository(_dal).GetAuthorById(Guid.NewGuid());
            Assert.AreEqual(_author, author);
        }

        #endregion
    }
}