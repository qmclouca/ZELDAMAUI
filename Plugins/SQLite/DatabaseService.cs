using Domain.Entities;
using Domain.Interfaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugins.SQLite
{
    public class DatabaseService : IDatabaseService
    {
        private SQLiteAsyncConnection _db;

        public async Task Init()
        {
            if (_db is not null)
                return;
            var dbPath = Path.Combine(
                Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData)
                , "zelda.db");
            _db = new SQLiteAsyncConnection(dbPath);
            await _db.CreateTableAsync<PlayerData>();
        }

        public async Task<PlayerData> GetLastPlayerAsync()
        {
            await Init();
            try
            {
                return await _db.Table<PlayerData>()
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return new PlayerData();
            
        }

        public async Task SavePlayerAsync(PlayerData player)
        {
            await Init();
            if (player.Id != 0)
            {
                await _db.UpdateAsync(player);
            }
            else
            {
                await _db.InsertAsync(player);
            }
        }
    }
}
