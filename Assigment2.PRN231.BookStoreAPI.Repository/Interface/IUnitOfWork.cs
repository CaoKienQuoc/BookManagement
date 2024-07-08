using Assigment2.PRN231.BookStoreAPI.Repositories.Entity;

namespace PRN231_Group.Assigment.API.Repo.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Author> AuthorRepository { get; }
        IGenericRepository<Book> BookRepository { get; }
        IGenericRepository<BookAuthor> BookAuthorRepository { get; }
        IGenericRepository<Publisher> PublisherRepository { get; }
        IGenericRepository<Role> RoleRepository { get; }
        IGenericRepository<User> UserRepository { get; }

        void Save();
    }
}
