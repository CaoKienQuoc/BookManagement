using Assigment2.PRN231.BookStoreAPI.Repositories.Entity;
using PRN231_Group.Assigment.API.Repo.Interface;
using System.Linq.Expressions;
namespace PRN231_Group.Assigment.API.Repo.Implement
{
    public  class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly Assigment2Prn231Context context;
        private IGenericRepository<Author> authorRepository;
        private IGenericRepository<Book> bookRepository;
        private IGenericRepository<BookAuthor> bookAuthorRepository;
        private IGenericRepository<Publisher> publisherRepository;
        private IGenericRepository<Role> roleRepository;
        private IGenericRepository<User> userRepository;

        public UnitOfWork(Assigment2Prn231Context context)
        {
            this.context = context;
        }


        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;




        public IGenericRepository<Author> AuthorRepository
        {
            get
            {
                return authorRepository ??= new GenericRepository<Author>(context);
            }
        }

        public IGenericRepository<Book> BookRepository
        {
            get
            {
                return bookRepository ??= new GenericRepository<Book>(context);
            }
        }

        public IGenericRepository<BookAuthor> BookAuthorRepository
        {
            get
            {
                return bookAuthorRepository ??= new GenericRepository<BookAuthor>(context);
            }
        }

        public IGenericRepository<Publisher> PublisherRepository
        {
            get
            {
                return publisherRepository ??= new GenericRepository<Publisher>(context);
            }
        }

        public IGenericRepository<Role> RoleRepository
        {
            get
            {
                return roleRepository ??= new GenericRepository<Role>(context);
            }
        }

        public IGenericRepository<User> UserRepository
        {
            get
            {
                return userRepository ??= new GenericRepository<User>(context);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> And<T>(
            this Expression<Func<T, bool>> left,
            Expression<Func<T, bool>> right)
        {
            if (left == null)
                return right;
            var invokedExpr = Expression.Invoke(right, left.Parameters);
            return Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(left.Body, invokedExpr), left.Parameters);
        }
    }
}
