using FarmEase.Domain.Entities;
using FarmEase.Infrastructure.Data;
using FarmEase.Infrastructure.Repository.Interface;

namespace FarmEase.Infrastructure.Repository.Implementation
{
    public class ApplicationUserRepository : Repository<ApplicationUser> , IApplicationUserRepository
    {

        private readonly ApplicationDBContext _db;
        public ApplicationUserRepository(ApplicationDBContext db) :base(db)
        {
            _db = db;
        }

    }
}
