using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Vaccination.Domain.Contracts
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
    }
}
