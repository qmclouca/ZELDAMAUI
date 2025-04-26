using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IDatabaseService
    {
        Task Init();
        Task SavePlayerAsync(PlayerData player);
        Task<PlayerData> GetLastPlayerAsync();
    }
}
