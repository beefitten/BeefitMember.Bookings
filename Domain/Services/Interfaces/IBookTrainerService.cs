using System.Threading.Tasks;
using Domain.Events;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Services.Interfaces
{
    public interface IBookTrainerService
    {
        Task BookTrainer(BookTrainerEvent evt);
    }
}