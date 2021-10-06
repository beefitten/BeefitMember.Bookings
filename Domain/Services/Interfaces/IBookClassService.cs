using System.Threading.Tasks;
using Domain.Events;

namespace Domain.Services.Interfaces
{
    public interface IBookClassService
    {
        Task BookClass(BookClassEvent evt);
    }
}